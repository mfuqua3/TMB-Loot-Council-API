using LootCouncil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LootCouncil.Domain.Data.Configuration
{
    public class GuildUserConfiguration : IEntityTypeConfiguration<GuildUser>
    {
        public void Configure(EntityTypeBuilder<GuildUser> builder)
        {
            builder.HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .IsRequired();
            builder.Property(x => x.RoleId)
                .HasDefaultValue(1);
        }
    }
}