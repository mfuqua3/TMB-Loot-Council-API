using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LootCouncil.Domain;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.Core.Model;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.Entities;
using LootCouncil.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LootCouncil.Engine
{
    public class PreVoteGenerationEngine : IPreVoteGenerationEngine
    {
        private readonly LootCouncilDbContext _dbContext;
        private readonly IConfigurationProvider _configurationProvider;

        public PreVoteGenerationEngine(LootCouncilDbContext dbContext, IConfigurationProvider configurationProvider)
        {
            _dbContext = dbContext;
            _configurationProvider = configurationProvider;
        }

        public async Task<int> GeneratePreVote(int configurationId, int guildId)
        {
            var configuration = await GetConfiguration(configurationId);
            var characterItems = await AggregateCandidateItemData(guildId);
            var groupedItems = GroupItems(characterItems);
            var voterAggregate = await CreateVoterAggregate(configuration.VoterSelectionConfigurationId, guildId);
            return await CreatePreVote(groupedItems, voterAggregate, configuration, guildId);
        }

        private async Task<int> CreatePreVote(List<PreVoteItem> groupedItems, VoterAggregate voterAggregate,
            PreVoteConfiguration configuration, int guildId)
        {
            var preVote = new PreVote()
            {
                GuildId = guildId,
                PreVoteConfiguration = configuration,
                Voters = new List<PreVoteVoter>(),
                Items = groupedItems
            };
            voterAggregate.EligibleVoters = voterAggregate.EligibleVoters.OrderBy(x => x.Fixed).ToList();
            var fixedCount = voterAggregate.EligibleVoters.Count(x => x.Fixed);
            voterAggregate.EligibleVoters = RandomizeVoters(voterAggregate.EligibleVoters, fixedCount);
            foreach (var preVoteItem in groupedItems)
            {
                await AssignVoters(voterAggregate, preVoteItem, fixedCount,
                    configuration.ConflictOfInterestConfiguration, preVote.Voters);
            }

            await _dbContext.PreVotes.AddAsync(preVote);
            await _dbContext.SaveChangesAsync();
            return preVote.Id;
        }

        private List<EligibleVoterInfo> RandomizeVoters(List<EligibleVoterInfo> voters, int skipCount)
        {
            var skipped = voters.Take(skipCount).ToList();
            var randomizedRemainder = voters.Skip(skipCount).ToList().Shuffle();
            skipped.AddRange(randomizedRemainder);
            return skipped;
        }

        private async Task AssignVoters(
            VoterAggregate voterAggregate, 
            PreVoteItem item, 
            int fixedVoteCount,
            ConflictOfInterestConfiguration conflictOfInterestConfiguration,
            List<PreVoteVoter> voterPool)
        {
            var eligibleVoters = voterAggregate.Randomize
                ? RandomizeVoters(voterAggregate.EligibleVoters, fixedVoteCount)
                : voterAggregate.EligibleVoters;
            var characterIds = item.EligibleCharacters.Select(x => x.CharacterId).ToList();
            item.VoterAssignments = new List<PreVoteItemAssignment>();
            foreach (var eligibleVoter in eligibleVoters)
            {
                if (item.VoterAssignments.Count >= voterAggregate.MaxPerItem)
                    break;
                var canVote = conflictOfInterestConfiguration.AllowVoting ||
                              !await _dbContext.GuildUsers
                                  .Where(x => x.Id == eligibleVoter.GuildUserId)
                                  .SelectMany(x => x.Characters)
                                  .Select(x => x.Id)
                                  .AnyAsync(x => characterIds.Contains(x));
                if(!canVote)
                    continue;
                var voter = voterPool.FirstOrDefault(x => x.GuildUserId == eligibleVoter.GuildUserId);
                if (voter == null)
                {
                    voter = new PreVoteVoter
                    {
                        GuildUserId = eligibleVoter.GuildUserId
                    };
                    voterPool.Add(voter);
                }
                item.VoterAssignments.Add(new PreVoteItemAssignment
                {
                    PreVoteVoter = voter
                });
            }
        }

        private async Task<VoterAggregate> CreateVoterAggregate(int voterConfigId, int guildId)
        {
            var voterConfig = await _dbContext.VoterSelectionConfigurations
                .Include(x => x.VoterConfigurations)
                .Include(x => x.EligibleRoleConfigurations)
                .SingleAsync(x => x.Id == voterConfigId);
            var usersInEligibleRoles = await _dbContext.GuildUsers
                .Where(x => x.GuildId == guildId)
                .Where(x => voterConfig.EligibleRoleConfigurations.Select(cfg=>cfg.GuildRoleId).Contains(x.RoleId))
                .Select(x => x.Id)
                .ToListAsync();
            var toAdd = voterConfig.VoterConfigurations.Where(x => x.Eligible).Select(x => x.GuildUserId)
                .Except(usersInEligibleRoles);
            var toRemove = usersInEligibleRoles.Where(x =>
                voterConfig.VoterConfigurations.Any(cfg => !cfg.Eligible && cfg.GuildUserId == x));
            usersInEligibleRoles.AddRange(toAdd);
            foreach (var user in toRemove)
            {
                usersInEligibleRoles.Remove(user);
            }

            return new VoterAggregate()
            {
                MaxPerItem = voterConfig.MaximumVotersPerItem,
                Randomize = voterConfig.Randomize,
                EligibleVoters = usersInEligibleRoles.Select(x => new EligibleVoterInfo
                {
                    Fixed = voterConfig.VoterConfigurations.Any(cfg => cfg.GuildUserId == x && cfg.Fixed),
                    GuildUserId = x
                }).ToList()
            };
        }

        private List<PreVoteItem> GroupItems(List<CharacterItem> characterItems)
        {
            return characterItems
                .GroupBy(x => x.ItemId)
                .Select(x => new PreVoteItem()
                {
                    ItemId = x.Key,
                    EligibleCharacters = x.Select(ci => new PreVoteCharacter
                    {
                        CharacterId = ci.CharacterId,
                        CharacterConsiderations = ci.Type == DataConstants.ItemTypes.Wishlist
                            ? new List<PreVoteCharacterConsideration>
                            {
                                new PreVoteCharacterConsideration
                                {
                                    Type = DataConstants.ItemTypes.Wishlist,
                                    Order = ci.Order.GetValueOrDefault()
                                }
                            }
                            : default
                    }).ToList()
                }).ToList();
        }

        private async Task<List<CharacterItem>> AggregateCandidateItemData(int guildId)
        {
            var itemGroups = await _dbContext.CharacterItems
                .AsQueryable()
                .Where(x => x.Character.GuildId == guildId)
                .Where(x => x.Type == DataConstants.ItemTypes.Wishlist)
                .GroupBy(x => x.CharacterId)
                .Select(x =>
                    x.Where(item =>
                        !_dbContext.CharacterItems
                            .Where(set => set.CharacterId == item.CharacterId && item.ItemId == set.ItemId)
                            .Any(set => set.Type == DataConstants.ItemTypes.Received)).ToList()
                ).ToListAsync();
            return itemGroups.SelectMany(x => x).ToList();
        }

        private async Task<PreVoteConfiguration> GetConfiguration(int configurationId)
        {
            var config = await _dbContext.PreVoteConfigurations.FindAsync(configurationId);
            if (config == null)
                throw new KeyNotFoundException();
            return config;
        }

        private class VoterAggregate
        {
            public bool Randomize { get; set; }
            public int MaxPerItem { get; set; }
            public List<EligibleVoterInfo> EligibleVoters { get; set; }
        }

        private class EligibleVoterInfo
        {
            public int GuildUserId { get; set; }
            public bool Fixed { get; set; }
        }
    }
}