using LootCouncil.Domain.Entities;

namespace LootCouncil.Domain.Data.Seeds
{
    public class DefaultPreVoteConfigurationSeed : DataSeedConfiguration<PreVoteConfiguration>
    {
        protected override PreVoteConfiguration[] Data => new[]
        {
            new PreVoteConfiguration()
            {
                Id = 1,
                ExpirationConfigurationId = 1,
                ItemSelectionConfigurationId = 1,
                VoterSelectionConfigurationId = 1,
                ConflictOfInterestConfigurationId = 1,
                TransparencyConfigurationId = 1
            }
        };
    }
}