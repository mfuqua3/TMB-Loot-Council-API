using System.Collections.Generic;
using LootCouncil.Domain.Entities;
using LootCouncil.Utility.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LootCouncil.Domain.Data.Configuration
{
    public class GuildRoleConfiguration : IEntityTypeConfiguration<GuildRole>
    {
        public void Configure(EntityTypeBuilder<GuildRole> builder)
        {
            builder.HasData(new List<GuildRole>
            {
                new()
                {
                    Id = 1,
                    Name = AuthorizationConstants.GuildRoles.Basic
                },
                new()
                {
                    Id = 2,
                    Name = AuthorizationConstants.GuildRoles.Admin
                },
            });
        }
    }
}