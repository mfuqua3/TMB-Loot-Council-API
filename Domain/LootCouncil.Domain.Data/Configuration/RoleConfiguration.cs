using LootCouncil.Utility.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LootCouncil.Domain.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        private readonly IdentityRole[] _roles = {
            new()
            {
                Name = AuthorizationConstants.Roles.Basic,
                NormalizedName = AuthorizationConstants.Roles.Basic.ToUpper()
            },
            new()
            {
                Name = AuthorizationConstants.Roles.Developer,
                NormalizedName = AuthorizationConstants.Roles.Developer.ToUpper()
            },
            new()
            {
                Name = AuthorizationConstants.Roles.Admin,
                NormalizedName = AuthorizationConstants.Roles.Admin.ToUpper()
            },
        };

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(_roles);
        }
    }
}