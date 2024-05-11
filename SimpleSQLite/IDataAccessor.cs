namespace Kildetoft.SimpleSQLite;

/// <summary>
/// Main interface for interacting with data
/// Should be bound using the extension for IServiceCollection
/// </summary>
public interface IDataAccessor
{
    /// <summary>
    /// Get an item with the specified Id if one exists, and null otherwise
    /// </summary>
    Task<T?> GetById<T>(int id) where T : IEntity, new();

    /// <summary>
    /// Create the chosen item
    /// Returns the created item, including the Id if this gets set by the database
    /// </summary>
    Task<T> Create<T>(T entity) where T : IEntity, new();

    /// <summary>
    /// Updates the specified item, identified by Id
    /// </summary>
    Task Update<T>(T entity) where T : IEntity, new();

    /// <summary>
    /// Deletes the item with the specified Id
    /// </summary>
    Task Delete<T>(int id) where T : IEntity, new();

    /// <summary>
    /// Get all items defined by the given ISpecification
    /// </summary>
    Task<IList<T>> Get<T>(IAllSpecification<T> specification) where T : IEntity, new();

    /// <summary>
    /// Get the first item defined by the given specification
    /// Throws an exception if no such item exists
    /// </summary>
    Task<T> Get<T>(IFirstSpecification<T> specification) where T : IEntity, new();

    /// <summary>
    /// Get the first item defined by the given specification
    /// Returns null if no such item exists
    /// </summary>
    Task<T?> Get<T>(IFirstOrDefaultSpecification<T> specification) where T : IEntity, new();
}
