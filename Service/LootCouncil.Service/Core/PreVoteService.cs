using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.Core.Model;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.Extensions;
using LootCouncil.Engine;
using Microsoft.EntityFrameworkCore;

namespace LootCouncil.Service.Core
{
    public class PreVoteService : IPreVoteService
    {
        private readonly IPreVoteConfigurationEngine _preVoteConfigurationEngine;
        private readonly IPreVoteGenerationEngine _preVoteGenerationEngine;
        private readonly LootCouncilDbContext _dbContext;
        private readonly IConfigurationProvider _configurationProvider;

        public PreVoteService(
            IPreVoteConfigurationEngine preVoteConfigurationEngine, 
            IPreVoteGenerationEngine preVoteGenerationEngine,
            LootCouncilDbContext dbContext,
            IConfigurationProvider configurationProvider)
        {
            _preVoteConfigurationEngine = preVoteConfigurationEngine;
            _preVoteGenerationEngine = preVoteGenerationEngine;
            _dbContext = dbContext;
            _configurationProvider = configurationProvider;
        }
        public async Task<PreVoteSummary> CreatePreVote(CreatePreVoteRequest request)
        {
            var configurationId = await _preVoteConfigurationEngine.AddOrGetConfiguration(request);
            var preVoteId = await _preVoteGenerationEngine.GeneratePreVote(configurationId, request.GuildId);
            return await _dbContext.PreVotes
                .Where(x => x.Id == preVoteId)
                .ProjectTo<PreVoteSummary>(_configurationProvider)
                .SingleAsync();
        }

        public async Task<PreVoteConfigurationModel> GetConfiguration(int id) =>
            await _dbContext.PreVoteConfigurations
                .ProjectTo<PreVoteConfigurationModel>(_configurationProvider)
                .SingleOrNotFoundAsync(x => x.Id == id);

        public async Task<PreVoteConfigurationModel> GetLatestConfiguration(int guildId)
        {
            return await _dbContext.PreVotes
                .Where(x => x.GuildId == guildId)
                .OrderByDescending(x => x.Created)
                .Select(x => x.PreVoteConfiguration)
                .ProjectTo<PreVoteConfigurationModel>(_configurationProvider)
                .FirstOrNotFoundAsync();
        }
    }
}