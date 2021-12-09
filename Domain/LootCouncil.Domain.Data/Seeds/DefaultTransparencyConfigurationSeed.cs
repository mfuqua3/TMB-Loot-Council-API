using LootCouncil.Domain.Entities;

namespace LootCouncil.Domain.Data.Seeds
{
    public class DefaultTransparencyConfigurationSeed : DataSeedConfiguration<TransparencyConfiguration>
    {
        protected override TransparencyConfiguration[] Data => new[]
        {
            new TransparencyConfiguration()
            {
                Id = 1,
                VoteVisibilityId = 1,
            }
        };
    }
}