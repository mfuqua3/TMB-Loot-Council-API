using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Model;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;

namespace LootCouncil.Service.Core
{
    public interface IPreVoteService
    {
        Task<PreVoteSummary> CreatePreVote(CreatePreVoteRequest request);
        Task<PreVoteConfigurationModel> GetConfiguration(int id);
        Task<PreVoteConfigurationModel> GetLatestConfiguration(int guildId);
        
    }
}