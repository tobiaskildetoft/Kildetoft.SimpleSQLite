using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kildetoft.SimpleSQLite.QueryHelpers;

internal static class AsyncTableQueryExtensions
{
    internal static AsyncTableQuery<T> Where<T>(this AsyncTableQuery<T> query, IWhereSpecification<T> whereSpecification) where T : IEntity, new()
    {
        return query.Where(whereSpecification.WhereExpression);
    }

    internal static AsyncTableQuery<T> OrderBy<T>(this AsyncTableQuery<T> query, IOrderSpecification<T> orderSpecification) where T : IEntity, new()
    {
        if (orderSpecification.OrderType == OrderType.Ascending)
        {
            return query.OrderBy(orderSpecification.OrderExpression);
        }
        return query.OrderByDescending(orderSpecification.OrderExpression);
    }

    internal static AsyncTableQuery<T> Take<T>(this AsyncTableQuery<T> query, ITakeSpecification<T> takeSpecification) where T : IEntity, new()
    {
        return query.Take(takeSpecification.NumberToTake);
    }

    internal static AsyncTableQuery<T> Skip<T>(this AsyncTableQuery<T> query, ISkipSpecification<T> skipSpecification) where T : IEntity, new()
    {
        return query.Skip(skipSpecification.NumberToSkip);
    }

    internal static AsyncTableQuery<T> ApplySpecification<T>(this AsyncTableQuery<T> query, ISpecification<T> specification) where T : IEntity, new()
    {
        if (specification is IWhereSpecification<T> whereSpecification)
        {
            query = query.Where(whereSpecification);
        }
        if (specification is IOrderSpecification<T> orderSpecification)
        {
            query = query.OrderBy(orderSpecification);
        }
        if (specification is ISkipSpecification<T> skipSpecification)
        {
            query = query.Skip(skipSpecification);
        }
        if (specification is ITakeSpecification<T> takeSpecification)
        {
            query = query.Take(takeSpecification);
        }
        return query;
    }
}