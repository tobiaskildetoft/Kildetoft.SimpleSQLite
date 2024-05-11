using System.Linq.Expressions;

namespace Kildetoft.SimpleSQLite;

/// <summary>
/// Implement this interface to speficy an ordering to be applied to the results prior to return
/// </summary>
public interface IOrderSpecification<T> : ISpecification<T> where T : IEntity, new()
{
    /// <summary>
    /// Supply a LambdaExpression to be used for ordering the results
    /// For example, x => x.Name
    /// </summary>
    Expression<Func<T, IComparable>> OrderExpression { get; }

    /// <summary>
    /// Specify whether to order Ascending or Descending
    /// </summary>
    OrderType OrderType { get; }
}
