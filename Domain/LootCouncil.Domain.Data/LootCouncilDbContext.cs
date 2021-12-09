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
        public DbSet<DiscordServerIdentity> DiscordServers { get; set; }
        public DbSet<DiscordServerMember> DiscordServerMembers { get; set; }
        public DbSet<GuildUser> GuildUsers { get; set; }
        public DbSet<GuildRole> GuildRoles { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<ItemFilter> ItemFilters { get; set; }
        public DbSet<CharacterItemFilter> CharacterItemFilters { get; set; }
        public DbSet<CharacterItem> CharacterItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Import> Imports { get; set; }
        
        public DbSet<PreVote> PreVotes { get; set; }
        public DbSet<PreVoteConfiguration> PreVoteConfigurations { get; set; }
        public DbSet<ConflictOfInterestConfiguration> ConflictOfInterestConfigurations { get; set; }
        public DbSet<ExpirationConfiguration> ExpirationConfigurations { get; set; }
        public DbSet<ItemSelectionConfiguration> ItemSelectionConfigurations { get; set; }
        public DbSet<TransparencyConfiguration> TransparencyConfigurations { get; set; }
        public DbSet<VoterSelectionConfiguration> VoterSelectionConfigurations { get; set; }
        public DbSet<VoteVisibility> VoteVisibilities { get; set; }
        
        public DbSet<PreVoteItem> PreVoteItems { get; set; }
        public DbSet<PreVoteItemAssignment> PreVoteItemAssignments { get; set; }
        public DbSet<PreVoteVoter> PreVoteVoters { get; set; }
        public DbSet<PreVoteItemObjection> PreVoteItemObjections { get; set; }
        public DbSet<PreVoteItemObjectionResponse> PreVoteItemObjectionResponses { get; set; }
        public DbSet<PreVoteItemComment> PreVoteItemComments { get; set; }
        public DbSet<PreVoteItemVote> PreVoteItemVotes { get; set; }
        public DbSet<PreVoteCharacter> PreVoteCharacters { get; set; }
        public DbSet<PreVoteCharacterConsideration> PreVoteCharacterConsiderations { get; set; }

        public DbSet<GuildRoleVoterConfiguration> GuildRoleVoterConfigurations { get; set; }
        public DbSet<GuildUserVoterConfiguration> GuildUserVoterConfigurations { get; set; }
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