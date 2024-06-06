using Kildetoft.SimpleSQLite;
using SimpleSQLite.Samples.Entities;
using System.Linq.Expressions;

namespace SimpleSQLite.Samples.Specifications;

public class OrderedBySomeIntDescending : IOrderSpecification<SampleEntity>
{
    public OrderType OrderType => OrderType.Descending;
    public Expression<Func<SampleEntity, IComparable>> OrderExpression => x => x.SomeInt;
}
