using System.Linq.Expressions;

namespace Kildetoft.SimpleSQLite;

/// <summary>
/// Implement this interface to signal that only certain items should be returned
/// </summary>
public interface IWhereSpecification<T> : ISpecification<T> where T : IEntity, new()
{
    /// <summary>
    /// Supply a LambdaExpression indicating which items to include
    /// For example x => x.Name == "SomeName"
    /// </summary>
    Expression<Func<T, bool>> WhereExpression { get; }
}
