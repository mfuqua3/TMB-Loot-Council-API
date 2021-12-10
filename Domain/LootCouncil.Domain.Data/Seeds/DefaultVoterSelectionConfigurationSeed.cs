using System.Collections.Generic;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Domain.Data.Seeds
{
    public class DefaultVoterSelectionConfigurationSeed : DataSeedConfiguration<VoterSelectionConfiguration>
    {
        protected override VoterSelectionConfiguration[] Data => new[]
        {
            new VoterSelectionConfiguration
            {
                Id = 1,
                MinimumVotersPerItem = 3,
                MaximumVotersPerItem = 5,
                Randomize = false,
            }
        };
    }
}