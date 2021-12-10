using LootCouncil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LootCouncil.Domain.Data.Configuration
{
    public class PreVoteConfiguration : IEntityTypeConfiguration<PreVote>
    {
        public void Configure(EntityTypeBuilder<PreVote> builder)
        {
            builder.Property(x => x.PreVoteConfigurationId).HasDefaultValue(1);
        }
    }
}