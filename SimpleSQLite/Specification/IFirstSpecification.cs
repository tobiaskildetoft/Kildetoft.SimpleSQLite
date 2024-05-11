namespace Kildetoft.SimpleSQLite;

/// <summary>
/// Implement this interface to signal that the first result should be returned, or an exception if no such result exists
/// </summary>
public interface IFirstSpecification<T> : ISpecification<T> where T : IEntity, new()
{
}
