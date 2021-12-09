using LootCouncil.Domain.Entities;

namespace LootCouncil.Domain.Data.Seeds
{
    public class DefaultVoteVisibilitySeed : DataSeedConfiguration<VoteVisibility>
    {
        protected override VoteVisibility[] Data => new[]
        {
            new VoteVisibility
            {
                Id = 1,
                AllowGuild = false,
                AllowAllEligibleVoters = true,
                VoteSubmissionRequirement = 1
            }
        };
    }
}