using System.Linq.Expressions;

namespace Kildetoft.SimpleSQLite;

public interface IOrderSpecification<T> : ISpecification<T> where T : IEntity
{
    Expression<Func<T, IComparable>> OrderExpression { get; }
    OrderType OrderType { get; }
}
