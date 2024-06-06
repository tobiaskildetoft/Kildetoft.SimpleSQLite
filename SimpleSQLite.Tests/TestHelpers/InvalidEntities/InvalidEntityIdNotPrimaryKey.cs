using Kildetoft.SimpleSQLite;
using SQLite;

namespace SimpleSQLite.Tests.TestHelpers.InvalidEntities;

[Table("InvalidEntityIdNotPrimaryKey")]
internal class InvalidEntityIdNotPrimaryKey : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UniqueName { get; set; } = string.Empty;
    public DateTime SomeDate { get; set; }
    public DateTime? SomeNullableDate { get; set; }
    [PrimaryKey, AutoIncrement]
    public int SomeInt { get; set; }
}
