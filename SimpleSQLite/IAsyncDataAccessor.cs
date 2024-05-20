namespace Kildetoft.SimpleSQLite;

/// <summary>
/// Main interface for asynchronously interacting with data
/// Should be bound using the extension for IServiceCollection
/// </summary>
public interface IAsyncDataAccessor
{
    /// <summary>
    /// Get an item with the specified Id if one exists, and null otherwise
    /// </summary>
    Task<T?> GetByIdAsync<T>(int id) where T : IEntity, new();

    /// <summary>
    /// Create the chosen item
    /// Returns the created item, including the Id if this gets set by the database
    /// </summary>
    Task<T> CreateAsync<T>(T entity) where T : IEntity, new();

    /// <summary>
    /// Updates the specified item, identified by Id
    /// </summary>
    Task UpdateAsync<T>(T entity) where T : IEntity, new();

    /// <summary>
    /// Deletes the item with the specified Id
    /// </summary>
    Task DeleteAsync<T>(int id) where T : IEntity, new();

    /// <summary>
    /// Get all items defined by the given ISpecification
    /// </summary>
    Task<IList<T>> GetAsync<T>(IAllSpecification<T> specification) where T : IEntity, new();

    /// <summary>
    /// Get the first item defined by the given specification
    /// Throws an exception if no such item exists
    /// </summary>
    Task<T> GetAsync<T>(IFirstSpecification<T> specification) where T : IEntity, new();

    /// <summary>
    /// Get the first item defined by the given specification
    /// Returns null if no such item exists
    /// </summary>
    Task<T?> GetAsync<T>(IFirstOrDefaultSpecification<T> specification) where T : IEntity, new();
}
