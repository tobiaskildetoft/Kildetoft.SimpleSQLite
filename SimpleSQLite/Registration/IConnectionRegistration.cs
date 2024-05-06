namespace Kildetoft.SimpleSQLite.IoC;

public interface IConnectionRegistration
{
    IConnectionRegistration AddTables(IEnumerable<Type> entityTypes, bool allowUnusableTypes = false);
    IConnectionRegistration AddTable(Type entityType, bool allowUnusableTypes = false);
}
