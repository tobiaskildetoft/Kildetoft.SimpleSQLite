namespace Kildetoft.SimpleSQLite;

/// <summary>
/// Base interface for specifications
/// Should not be directly implemented. Instead use the various extensions
/// </summary>
public interface ISpecification<T> where T : IEntity, new()
{
}
