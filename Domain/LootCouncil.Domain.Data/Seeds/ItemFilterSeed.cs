using LootCouncil.Domain.Entities;

namespace LootCouncil.Domain.Data.Seeds
{
    public class ItemFilterSeed : DataSeedConfiguration<ItemFilter>
    {
        protected override ItemFilter[] Data => new[]
        {
            new ItemFilter()
            {
                Id = 1,
                Name = DataConstants.ItemFilters.Offspec
            }
        };
    }
}