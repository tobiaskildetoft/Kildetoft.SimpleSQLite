namespace Kildetoft.SimpleSQLite;

/// <summary>
/// Base interface for all database entities
/// Must have a public parameterless constructor
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Must also be annotated with [PrimaryKey] from SQLite-Net
    /// </summary>
    int Id { get; }
}
