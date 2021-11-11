using System;
using System.Linq.Expressions;
using System.Reflection;
using LootCouncil.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LootCouncil.Domain.Extensions
{
    public static class SoftDeleteQueryExtension
    {
        public static void AddSoftDeleteQueryFilter(this IMutableEntityType entityType)
        {
            var methodToCall = typeof(SoftDeleteQueryExtension)
                .GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
                ?.MakeGenericMethod(entityType.ClrType);

            var filter = methodToCall?.Invoke(null, new object[] { });
            entityType.SetQueryFilter((LambdaExpression)filter);
            entityType.AddIndex(entityType.FindProperty(nameof(ISoftDelete.IsDeleted)));
        }

        internal static LambdaExpression GetSoftDeleteFilter<TEntity>() where TEntity : class, ISoftDelete
        {
            Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }
    }
}