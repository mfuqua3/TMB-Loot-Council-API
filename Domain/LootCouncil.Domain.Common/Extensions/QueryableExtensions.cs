using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LootCouncil.Domain.Extensions;

public static class QueryableExtensions
{
    public static async Task<T> SingleOrNotFoundAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
    {
        try
        {
            return await query.SingleAsync(predicate);
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException("The requested resource could not be found", ex);
        }
    }
    public static async Task<T> FirstOrNotFoundAsync<T>(this IQueryable<T> query)
    {
        try
        {
            return await query.FirstAsync();
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException("The requested resource could not be found", ex);
        }
    }
    public static async Task<T> FirstOrNotFoundAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
    {
        try
        {
            return await query.FirstAsync(predicate);
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException("The requested resource could not be found", ex);
        }
    }
}