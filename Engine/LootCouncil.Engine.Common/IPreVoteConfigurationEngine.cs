using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Model;
using LootCouncil.Domain.DataContracts.Core.Request;

namespace LootCouncil.Engine
{
    public interface IPreVoteConfigurationEngine
    {
        Task<int> AddOrGetConfiguration(CreatePreVoteRequest request);
    }
}