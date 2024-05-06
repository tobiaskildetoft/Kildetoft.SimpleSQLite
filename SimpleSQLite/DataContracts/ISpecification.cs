using System.Linq.Expressions;

namespace Kildetoft.SimpleSQLite;

public interface ISpecification<T> where T : IEntity
{
    Expression<Func<T, bool>> IsSatisfied { get; }
}
