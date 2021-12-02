using System;
using LootCouncil.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace LootCouncil.Domain.Extensions
{
    public static class DbContextExtensions
    {
        public static void ProcessCustomInterfaces(this DbContext dbContext)
        {
            var autoDetectChangesEnabled = dbContext.ChangeTracker.AutoDetectChangesEnabled;
            try
            {
                dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
                foreach (var entry in dbContext.ChangeTracker.Entries())
                {
                    #region Configure ISoftDelete Fields
                    if (entry.Entity is ISoftDelete deleted)
                    {
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                deleted.IsDeleted = false;
                                break;
                            case EntityState.Deleted:
                                entry.State = EntityState.Unchanged;
                                deleted.IsDeleted = true;
                                deleted.Deleted = DateTime.UtcNow;
                                break;
                        }
                    }
                    #endregion

                    #region Configure ITracked Fields
                    if (entry.Entity is ITracked tracked)
                    {
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                tracked.Created = DateTime.UtcNow;
                                break;
                            case EntityState.Modified:
                                tracked.Updated = DateTime.UtcNow;
                                break;
                        }
                    }
                    #endregion
                }
            }
            finally
            {
                dbContext.ChangeTracker.AutoDetectChangesEnabled = autoDetectChangesEnabled;
            }
        }
    }
}