using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Response;

namespace LootCouncil.Engine
{
    public interface IPreVoteGenerationEngine
    {
        Task<PreVoteResponse> GeneratePreVote(int configurationId);
    }
}