namespace Kildetoft.SimpleSQLite;

/// <summary>
/// Implement this interface to signal that all results should be returned
/// </summary>
public interface IAllSpecification<T> : ISpecification<T> where T : IEntity, new()
{
}
