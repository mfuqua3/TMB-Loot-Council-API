using System.Reflection;
using LootCouncil.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LootCouncil.Domain.Data
{
    public class LootCouncilDbContext : IdentityDbContext<LootCouncilUser>
    {
        public DbSet<DiscordIdentity> DiscordIdentities { get; set; }
        public LootCouncilDbContext(DbContextOptions options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}