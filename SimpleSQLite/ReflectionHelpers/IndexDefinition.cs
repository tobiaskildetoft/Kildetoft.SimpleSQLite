namespace Kildetoft.SimpleSQLite.ReflectionHelpers;

internal class IndexDefinition
{
    internal string? TableName { get; set; }
    internal string? AttributeName { get; set; }
    internal bool Unique { get; set; } 
}
