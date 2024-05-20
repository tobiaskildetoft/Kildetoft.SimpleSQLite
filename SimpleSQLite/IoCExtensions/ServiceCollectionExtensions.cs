using Microsoft.Extensions.DependencyInjection;

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
        return new ConnectionRegistration();
    }
}
