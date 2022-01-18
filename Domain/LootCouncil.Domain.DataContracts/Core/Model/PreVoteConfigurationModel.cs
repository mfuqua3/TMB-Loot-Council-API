using LootCouncil.Domain.DataContracts.Core.Request;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class PreVoteConfigurationModel
    {
        public int? Id { get; set; }
        public ExpirationConfigurationModel ExpirationConfiguration { get; set; }
        public ItemSelectionConfigurationModel ItemSelectionConfiguration { get; set; }
        public VoterSelectionConfigurationModel VoterSelectionConfiguration { get; set; }
        public ConflictOfInterestConfigurationModel ConflictOfInterestConfiguration { get; set; }
        public TransparencyConfigurationModel TransparencyConfiguration { get; set; }
    }
}