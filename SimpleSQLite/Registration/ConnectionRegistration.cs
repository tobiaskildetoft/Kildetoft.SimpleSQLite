using Kildetoft.SimpleSQLite.Exceptions;
using Kildetoft.SimpleSQLite.ReflectionHelpers;
using Microsoft.Extensions.DependencyInjection;

namespace Kildetoft.SimpleSQLite.IoC;

internal class ConnectionRegistration : IConnectionRegistration
{
    internal ConnectionRegistration(IServiceCollection serviceCollection)
    {
        ServiceCollection = serviceCollection;
    }

    public IServiceCollection ServiceCollection { get; private set; }

    public IConnectionRegistration AddTablesFromAssemblyContaining<T>()
    {
        var types = SQLiteEntities.FromAssemblyContaining<T>();
        return AddTables(types);
    }

    public IConnectionRegistration AddIndexesFromAssemblyContaining<T>()
    {
        var types = SQLiteIndexes.FromAssemblyContaining<T>();
        return AddIndexes(types);
    }

    public IConnectionRegistration AddAllFromAssemblyContaining<T>()
    {
        AddTablesFromAssemblyContaining<T>();
        AddIndexesFromAssemblyContaining<T>();
        return this;
    }

    public IConnectionRegistration AddTable<T>() where T: IEntity, new()
    {
        return AddTable(typeof(T));
    }

    public IConnectionRegistration AddTable(Type entityType)
    {
        return AddTables([entityType]);
    }

    public IConnectionRegistration AddTables(IEnumerable<Type> entityTypes)
    {
        if (entityTypes.FirstOrDefault(t => !t.IsUsableEntity()) is Type unusableType)
        {
            throw new UnsupportedEntityImplementationException($"Type {unusableType.Name} is not valid for use as a SimpleSQLite entity");
        }
        DatabaseConnectionFactory.AddTables(entityTypes);
        return this;
    }

    public IConnectionRegistration AddIndex<T>() where T : IIndex, new()
    {
        return AddIndex(typeof(T));
    }

    public IConnectionRegistration AddIndex(Type indexType)
    {
        return AddIndexes([indexType]);
    }

    public IConnectionRegistration AddIndexes(IEnumerable<Type> indexTypes)
    {
        foreach (var indexType in indexTypes)
        {
            DatabaseConnectionFactory.AddIndex(indexType);
        }
        return this;
    }
}
