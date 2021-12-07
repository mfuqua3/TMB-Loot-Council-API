using LootCouncil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LootCouncil.Domain.Data.Configuration
{
    public class CharacterItemConfiguration : IEntityTypeConfiguration<CharacterItem>
    {
        public void Configure(EntityTypeBuilder<CharacterItem> builder)
        {
            builder.HasMany(c => c.CharacterItemFilters)
                .WithOne(x => x.CharacterItem)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}