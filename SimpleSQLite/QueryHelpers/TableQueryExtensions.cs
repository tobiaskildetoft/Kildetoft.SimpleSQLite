using SQLite;

namespace Kildetoft.SimpleSQLite.QueryHelpers;

internal static class TableQueryExtensions
{
    internal static TableQuery<T> Where<T>(this TableQuery<T> query, IWhereSpecification<T> whereSpecification) where T : IEntity, new()
    {
        return query.Where(whereSpecification.WhereExpression);
    }

    internal static TableQuery<T> OrderBy<T>(this TableQuery<T> query, IOrderSpecification<T> orderSpecification) where T : IEntity, new()
    {
        if (orderSpecification.OrderType == OrderType.Ascending)
        {
            return query.OrderBy(orderSpecification.OrderExpression);
        }
        return query.OrderByDescending(orderSpecification.OrderExpression);
    }

    internal static TableQuery<T> Take<T>(this TableQuery<T> query, ITakeSpecification<T> takeSpecification) where T : IEntity, new()
    {
        return query.Take(takeSpecification.NumberToTake);
    }

    internal static TableQuery<T> Skip<T>(this TableQuery<T> query, ISkipSpecification<T> skipSpecification) where T : IEntity, new()
    {
        return query.Skip(skipSpecification.NumberToSkip);
    }

    internal static TableQuery<T> ApplySpecification<T>(this TableQuery<T> query, ISpecification<T> specification) where T : IEntity, new()
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
