using System.Linq.Expressions;

namespace Kildetoft.SimpleSQLite;

/// <summary>
/// Base interface for defining indexes to use with SimpleSQLite
/// </summary>
public interface IIndex
{
    internal const string IndexDefinitionName = "IndexDefinition";
    bool Unique { get; }
}

public interface IIndex<T> : IIndex where T : IEntity, new()
{
    /// <summary>
    /// Expression defining the property the index should be defined on
    /// For example x => x.Name
    /// Must be a LambdaExpression similar to above
    /// </summary>
    Expression<Func<T,object>> IndexDefinition { get; }
}
