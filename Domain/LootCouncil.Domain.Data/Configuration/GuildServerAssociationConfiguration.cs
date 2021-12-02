using LootCouncil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LootCouncil.Domain.Data.Configuration
{
    // public class GuildServerAssociationConfiguration : IEntityTypeConfiguration<GuildServerAssociation>
    // {
    //     public void Configure(EntityTypeBuilder<GuildServerAssociation> builder)
    //     {
    //         builder.HasOne(x=>x.Guild)
    //             .WithOne(x=>x.ServerAssociation)
    //             .HasForeignKey<GuildServerAssociation>(x=>x.ServerAssociationId)
    //     }
    // }
}