using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LootCouncil.Domain.Entities;
using LootCouncil.Domain.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LootCouncil.Domain.Data
{
    public class LootCouncilDbContext : IdentityDbContext<LootCouncilUser>
    {
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<GuildUser> GuildUsers { get; set; }
        public LootCouncilDbContext(DbContextOptions options): base(options)
        {
            
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ProcessCustomInterfaces();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override int SaveChanges()
        {
            this.ProcessCustomInterfaces();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            this.ProcessCustomInterfaces();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.ProcessCustomInterfaces();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.RestrictForeignKeyDelete();
            builder.ApplySoftDeleteQueryFilters();
            base.OnModelCreating(builder);
        }
    }
}