using LootCouncil.Domain.Entities;

namespace LootCouncil.Domain.Data.Seeds
{
    public class DefaultItemSelectionConfigurationSeed : DataSeedConfiguration<ItemSelectionConfiguration>
    {
        protected override ItemSelectionConfiguration[] Data => new[]
        {
            new ItemSelectionConfiguration
            {
                Id = 1,
                SelectAll = true
            }
        };
    }
}