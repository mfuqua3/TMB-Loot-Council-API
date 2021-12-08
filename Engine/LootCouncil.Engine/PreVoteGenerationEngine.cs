using System;
using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Response;

namespace LootCouncil.Engine
{
    public class PreVoteGenerationEngine : IPreVoteGenerationEngine
    {
        public async Task<PreVoteResponse> GeneratePreVote(int configurationId)
        {
            throw new NotImplementedException();
        }
    }
}