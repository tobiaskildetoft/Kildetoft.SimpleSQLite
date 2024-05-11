namespace Kildetoft.SimpleSQLite;

/// <summary>
/// Implement this interface to signal that the first result should be returned if exists, and null otherwise
/// </summary>
public interface IFirstOrDefaultSpecification<T> : ISpecification<T> where T : IEntity, new()
{
}
