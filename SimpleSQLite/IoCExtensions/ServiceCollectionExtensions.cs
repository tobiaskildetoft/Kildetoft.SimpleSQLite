using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DataAccess.DataContracts;
using TimeTracker.DataAccess.Registration;

namespace TimeTracker.DataAccess.IoCExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static ConnectionRegistration AddSimpleSQLite(this IServiceCollection collection, string connectionString)
        {
            // TODO: How to handle logging in the classes here?
            // TODO: Possible to use this database for logging with the users chosen logging?
            DatabaseConnectionFactory.Initialize(connectionString);
            collection.AddSingleton<IDataAccessor, DataAccessor>();
            return new ConnectionRegistration();
        }
    }
}
