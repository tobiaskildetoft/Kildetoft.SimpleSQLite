namespace Kildetoft.SimpleSQLite;

/// <summary>
/// Implement this interface to signal that a certain number of items in the result should be skipped
/// </summary>
public interface ISkipSpecification<T> : ISpecification<T> where T : IEntity, new()
{
    /// <summary>
    /// Supply the number of items to be skipped
    /// </summary>
    int NumberToSkip { get; }
}
