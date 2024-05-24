using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Kildetoft.SimpleSQLite.IoC;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add an implementation of the IDataAccessor interface to the given IServicecollection
    /// The connectionstring should be the full path for the file to be used as the database
    /// The returned IConnectionRegistration can be used to register tables and indexes for the database
    /// </summary>
    public static IConnectionRegistration AddSimpleSQLite(this IServiceCollection collection, string connectionString)
    {
        // TODO: How to handle logging in the classes here?
        // TODO: Possible to use this database for logging with the users chosen logging?
        DatabaseConnectionFactory.Initialize(connectionString);
        collection.AddSingleton<IDataAccessor, DataAccessor>();
        collection.AddSingleton<IAsyncDataAccessor, AsyncDataAccessor>();
        return new ConnectionRegistration(collection);
    }

    /// <summary>
    /// Removes the registration of the interfaces for SimpleSQLite and closes the database connection
    /// Does not delete any database files
    /// </summary>
    /// <exception cref="ReadOnlyException">If the IServiceCollection is readonly</exception>
    public static IServiceCollection RemoveSimpleSQLite(this IServiceCollection collection, bool deleteDatabase = false)
    {
        if (collection.IsReadOnly)
        {
            throw new ReadOnlyException($"{nameof(collection)} is read only");
        }
        collection.RemoveDataAccessor();
        collection.RemoveAsyncDataAccessor();
        DatabaseConnectionFactory.CloseConnections(deleteDatabase);
        return collection;
    }

    private static void RemoveDataAccessor(this IServiceCollection collection)
    {
        if (collection.FirstOrDefault(x => x.ServiceType == typeof(IDataAccessor)) is { } registration)
        {
            collection.Remove(registration);
        }
    }

    private static void RemoveAsyncDataAccessor(this IServiceCollection collection)
    {
        if (collection.FirstOrDefault(x => x.ServiceType == typeof(IAsyncDataAccessor)) is { } registration)
        {
            collection.Remove(registration);
        }
    }
}
