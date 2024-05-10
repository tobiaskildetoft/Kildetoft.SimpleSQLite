namespace Kildetoft.SimpleSQLite.IoC;

public interface IConnectionRegistration
{
    IConnectionRegistration AddTablesFromAssemblyContaining<T>();
    IConnectionRegistration AddIndexesFromAssemblyContaining<T>();
    IConnectionRegistration AddAllFromAssemblyContaining<T>();

    IConnectionRegistration AddTables(IEnumerable<Type> entityTypes, bool allowUnusableTypes = false);
    IConnectionRegistration AddTable(Type entityType, bool allowUnusableTypes = false);
}
