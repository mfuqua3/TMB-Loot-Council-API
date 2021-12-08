using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Model;

namespace LootCouncil.Engine
{
    public interface IPreVoteConfigurationEngine
    {
        Task ConfigureVoterSelection(int preVoteId, VoterSelectionConfigurationModel configurationModel);
        Task ConfigureConflictOfInterest(int preVoteId, ConflictOfInterestConfigurationModel configurationModel);
        Task ConfigureTransparency(int preVoteId, TransparencyConfigurationModel configurationModel);
    }
}