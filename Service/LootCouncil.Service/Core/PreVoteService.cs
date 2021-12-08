using System;
using System.Threading.Tasks;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.Entities;
using LootCouncil.Engine;

namespace LootCouncil.Service.Core
{
    public class PreVoteService : IPreVoteService
    {
        private readonly IPreVoteConfigurationEngine _preVoteConfigurationEngine;
        private readonly IPreVoteGenerationEngine _preVoteGenerationEngine;

        public PreVoteService(
            IPreVoteConfigurationEngine preVoteConfigurationEngine, 
            IPreVoteGenerationEngine preVoteGenerationEngine)
        {
            _preVoteConfigurationEngine = preVoteConfigurationEngine;
            _preVoteGenerationEngine = preVoteGenerationEngine;
        }
        public async Task<PreVoteResponse> CreatePreVote(CreatePreVoteRequest request)
        {
            var configurationId = await _preVoteConfigurationEngine.AddOrGetConfiguration(request);
            var preVote = await _preVoteGenerationEngine.GeneratePreVote(configurationId);
            return preVote;
        }
    }
}