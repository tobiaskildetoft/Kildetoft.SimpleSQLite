using SQLite;

namespace SimpleSQLite.Tests.TestHelpers.InvalidEntities;

[Table("InvalidEntityNotIEntity")]
internal class InvalidEntityNotIEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UniqueName { get; set; } = string.Empty;
    public DateTime SomeDate { get; set; }
    public DateTime? SomeNullableDate { get; set; }
    public int SomeInt { get; set; }
}
