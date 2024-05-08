using System.Linq.Expressions;

namespace Kildetoft.SimpleSQLite;

public interface IIndex<T> where T : IEntity
{
    Expression<Func<T,object>> IndexDefinition { get; }
    bool Unique { get; }
}
