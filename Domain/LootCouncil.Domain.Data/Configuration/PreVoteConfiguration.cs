using LootCouncil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LootCouncil.Domain.Data.Configuration
{
    public class PreVoteConfiguration : IEntityTypeConfiguration<PreVote>
    {
        public void Configure(EntityTypeBuilder<PreVote> builder)
        {
            builder.Property(x => x.ExpirationConfigurationId).HasDefaultValue(1);
            builder.Property(x => x.TransparencyConfigurationId).HasDefaultValue(1);
            builder.Property(x => x.VoterSelectionConfigurationId).HasDefaultValue(1);
            builder.Property(x => x.ItemSelectionConfigurationId).HasDefaultValue(1);
            builder.Property(x => x.ConflictOfInterestConfigurationId).HasDefaultValue(1);
        }
    }
}