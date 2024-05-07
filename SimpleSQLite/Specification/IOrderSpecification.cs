using System.Linq.Expressions;

namespace Kildetoft.SimpleSQLite;

public interface IOrderSpecification<T, S> : ISpecification<T> where T : IEntity where S : IComparable
{
    Expression<Func<T, S>> OrderExpression { get; }
    OrderType OrderType { get; }
}
