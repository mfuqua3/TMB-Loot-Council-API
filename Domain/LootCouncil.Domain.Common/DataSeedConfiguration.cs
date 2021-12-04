using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LootCouncil.Domain
{
    public abstract class DataSeedConfiguration<T> : IEntityTypeConfiguration<T> where T: class
    {
        protected abstract T[] Data { get; }
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasData(Data);
        }
    }
}