using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LootCouncil.Domain;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LootCouncil.Engine
{
    public class PreVoteGenerationEngine : IPreVoteGenerationEngine
    {
        private readonly LootCouncilDbContext _dbContext;

        public PreVoteGenerationEngine(LootCouncilDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PreVoteResponse> GeneratePreVote(int configurationId, int guildId)
        {
            var configuration = await GetConfiguration(configurationId);
            throw new NotImplementedException();
        }

        private async Task AggregateWishlistData(int guildId)
        {
            throw new NotImplementedException();
            // var y = await _dbContext.CharacterItems
            //     .AsQueryable()
            //     .Where(x=>x.Character.GuildId == guildId)
            //     .GroupBy(x=>x.ItemId)

        }

        private async Task<PreVoteConfiguration> GetConfiguration(int configurationId)
        {
            var config = await _dbContext.PreVoteConfigurations
                .Include(x => x.ExpirationConfiguration)
                .Include(x => x.TransparencyConfiguration)
                .ThenInclude(x => x.VoteVisibility)
                .Include(x => x.VoterSelectionConfiguration)
                .ThenInclude(x => x.VoterConfigurations)
                .Include(x => x.VoterSelectionConfiguration)
                .ThenInclude(x => x.EligibleRoleConfigurations)
                .Include(x => x.ConflictOfInterestConfiguration)
                .Include(x => x.ItemSelectionConfiguration)
                .SingleOrDefaultAsync(x=>x.Id == configurationId);
            if (config == null)
                throw new KeyNotFoundException();
            return config;
        }
    }
}