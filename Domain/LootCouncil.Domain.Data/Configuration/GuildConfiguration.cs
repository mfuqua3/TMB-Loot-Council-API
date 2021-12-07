using LootCouncil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LootCouncil.Domain.Data.Configuration
{
    public class GuildConfiguration : IEntityTypeConfiguration<Guild>
    {
        public void Configure(EntityTypeBuilder<Guild> builder)
        {
            builder.HasOne(e => e.Configuration)
                .WithOne(e => e.Guild)
                .HasForeignKey<Guild>(e => e.ConfigurationId)
                .IsRequired(false);
        }
    }
}