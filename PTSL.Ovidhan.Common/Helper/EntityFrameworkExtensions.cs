using System.Linq.Expressions;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace PTSL.Ovidhan.Common.Helper;

public static class EntityFrameworkExtensions
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition ? query.Where(predicate) : query;
    }

    public static IIncludableQueryable<TEntity, TProperty> IncludeIf<TEntity, TProperty>(this IQueryable<TEntity> query, bool condition, Expression<Func<TEntity, TProperty>> navigationProperty) where TEntity : class
    {
        return condition ? query.Include(navigationProperty) : (IIncludableQueryable<TEntity, TProperty>)query;
    }
}
