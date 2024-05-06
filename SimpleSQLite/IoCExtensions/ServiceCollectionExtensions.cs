using Microsoft.Extensions.DependencyInjection;

namespace Kildetoft.SimpleSQLite.IoC;

public static class ServiceCollectionExtensions
{
    public static IConnectionRegistration AddSimpleSQLite(this IServiceCollection collection, string connectionString)
    {
        // TODO: How to handle logging in the classes here?
        // TODO: Possible to use this database for logging with the users chosen logging?
        DatabaseConnectionFactory.Initialize(connectionString);
        collection.AddSingleton<IDataAccessor, DataAccessor>();
        return new ConnectionRegistration();
    }
}
