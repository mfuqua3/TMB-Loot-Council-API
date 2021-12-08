using System;
using System.Threading.Tasks;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Engine;

namespace LootCouncil.Service.Core
{
    public class PreVoteService : IPreVoteService
    {
        private readonly IPreVoteConfigurationEngine _preVoteConfigurationEngine;
        private readonly LootCouncilDbContext _dbContext;

        public PreVoteService(IPreVoteConfigurationEngine preVoteConfigurationEngine, LootCouncilDbContext dbContext)
        {
            _preVoteConfigurationEngine = preVoteConfigurationEngine;
            _dbContext = dbContext;
        }
        public async Task<PreVoteResponse> CreatePreVote(CreatePreVoteRequest request)
        {
            throw new NotImplementedException();
        }
    }
}