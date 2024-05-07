using System.Linq.Expressions;

namespace Kildetoft.SimpleSQLite;

public interface ISelectSpecification<T, S> : ISpecification<T> where T : IEntity
{
    Expression<Func<T, S>> SelectExpression { get; }
}
