namespace Kildetoft.SimpleSQLite;

/// <summary>
/// Implement this interface to signal that a certain number of items should be returned
/// Will return all results if fewer that the number specified exist
/// </summary>
public interface ITakeSpecification<T> : IAllSpecification<T> where T : IEntity, new()
{
    /// <summary>
    /// Supply the number of items to return
    /// </summary>
    int NumberToTake { get; }
}
