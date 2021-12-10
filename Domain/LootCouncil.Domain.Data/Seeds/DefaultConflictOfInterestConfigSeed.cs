using LootCouncil.Domain.Entities;

namespace LootCouncil.Domain.Data.Seeds
{
    public class DefaultConflictOfInterestConfigSeed : DataSeedConfiguration<ConflictOfInterestConfiguration>
    {
        protected override ConflictOfInterestConfiguration[] Data => new[]
        {
            new ConflictOfInterestConfiguration()
            {
                Id = 1,
                AllowVoting = false,
                AllowSelfVoting = false,
                AllowCommenting = true,
                AllowObjecting = false
            }
        };
    }
}