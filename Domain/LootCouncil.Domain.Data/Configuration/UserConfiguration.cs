using LootCouncil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LootCouncil.Domain.Data.Configuration
{
    public class UserConfiguration: IEntityTypeConfiguration<LootCouncilUser>
    {
        public void Configure(EntityTypeBuilder<LootCouncilUser> builder)
        {
            builder
                .HasOne(x => x.DiscordIdentity)
                .WithOne(x => x.User)
                .HasForeignKey<DiscordIdentity>(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}