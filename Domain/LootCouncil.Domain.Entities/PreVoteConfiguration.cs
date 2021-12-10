using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class PreVoteConfiguration : IUnique<int>
    {
        public int Id { get; set; }
        public int ExpirationConfigurationId { get; set; }
        public ExpirationConfiguration ExpirationConfiguration { get; set; }
        public int ItemSelectionConfigurationId { get; set; }
        public ItemSelectionConfiguration ItemSelectionConfiguration { get; set; }
        public int VoterSelectionConfigurationId { get; set; }
        public VoterSelectionConfiguration VoterSelectionConfiguration { get; set; }
        public int ConflictOfInterestConfigurationId { get; set; }
        public ConflictOfInterestConfiguration ConflictOfInterestConfiguration { get; set; }
        public int TransparencyConfigurationId { get; set; }
        public TransparencyConfiguration TransparencyConfiguration { get; set; }
    }
}