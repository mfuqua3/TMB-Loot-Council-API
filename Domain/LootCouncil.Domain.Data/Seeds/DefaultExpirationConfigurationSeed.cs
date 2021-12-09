using System;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Domain.Data.Seeds
{
    public class DefaultExpirationConfigurationSeed : DataSeedConfiguration<ExpirationConfiguration>
    {
        protected override ExpirationConfiguration[] Data => new[]
        {
            new ExpirationConfiguration()
            {
                Id = 1,
                ExpirationTime = DateTime.MaxValue,
                LockVotesTteMinutes = 0,
                LockCommentsTteMinutes = 0,
                LockObjectionsTteMinutes = 0
            }
        };
    }
}