namespace Kildetoft.SimpleSQLite.TestHelpers;

public static class SpecificationAppliers
{
    public static IEnumerable<T> ApplySpecification<T>(this IEnumerable<T> enumerable, IAllSpecification<T> specification) where T : IEntity, new()
    {
        var newItems = enumerable.ApplyGeneralSpecification(specification);
        return newItems;
    }

    public static T ApplySpecification<T>(this IEnumerable<T> enumerable, IFirstSpecification<T> specification) where T : IEntity, new()
    {
        var newItems = enumerable.ApplyGeneralSpecification(specification);
        return newItems.First();
    }

    public static T? ApplySpecification<T>(this IEnumerable<T> enumerable, IFirstOrDefaultSpecification<T> specification) where T : IEntity, new()
    {
        var newItems = enumerable.ApplyGeneralSpecification(specification);
        return newItems.FirstOrDefault();
    }

    private static IEnumerable<T> Where<T>(this IEnumerable<T> query, IWhereSpecification<T> whereSpecification) where T : IEntity, new()
    {
        return query.Where(whereSpecification.WhereExpression.Compile());
    }

    private static IEnumerable<T> OrderBy<T>(this IEnumerable<T> query, IOrderSpecification<T> orderSpecification) where T : IEntity, new()
    {
        if (orderSpecification.OrderType == OrderType.Ascending)
        {
            return query.OrderBy(orderSpecification.OrderExpression.Compile());
        }
        return query.OrderByDescending(orderSpecification.OrderExpression.Compile());
    }

    private static IEnumerable<T> Take<T>(this IEnumerable<T> query, ITakeSpecification<T> takeSpecification) where T : IEntity, new()
    {
        return query.Take(takeSpecification.NumberToTake);
    }

    private static IEnumerable<T> Skip<T>(this IEnumerable<T> query, ISkipSpecification<T> skipSpecification) where T : IEntity, new()
    {
        return query.Skip(skipSpecification.NumberToSkip);
    }

    private static IEnumerable<T> ApplyGeneralSpecification<T>(this IEnumerable<T> originalItems, ISpecification<T> specification) where T : IEntity, new()
    {
        var newItems = originalItems.Select(x => x);
        if (specification is IWhereSpecification<T> whereSpecification)
        {
            newItems = newItems.Where(whereSpecification);
        }
        if (specification is IOrderSpecification<T> orderSpecification)
        {
            newItems = newItems.OrderBy(orderSpecification);
        }
        if (specification is ISkipSpecification<T> skipSpecification)
        {
            newItems = newItems.Skip(skipSpecification);
        }
        if (specification is ITakeSpecification<T> takeSpecification)
        {
            newItems = newItems.Take(takeSpecification);
        }
        return newItems;
    }
}
