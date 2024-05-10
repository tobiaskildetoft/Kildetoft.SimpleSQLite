using Kildetoft.SimpleSQLite.IoC;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kildetoft.SimpleSQLite.TestHelpers
{
    public class DataAccessMock<T> : IDisposable where T : IEntity, new()
    {
        private readonly string _databaseName;
        public IDataAccessor DataAccessor { get; private set; }
        public DataAccessMock(IEnumerable<T> entities)
        {
            // TODO: time this construction when done multiple times, and see if it could be an issue
            _databaseName = $"{Guid.NewGuid()}.db";
            DatabaseConnectionFactory.Initialize(_databaseName);
            DatabaseConnectionFactory.AddTables([typeof(T)]);


            DataAccessor = new DataAccessor();

            foreach (var entity in entities)
            {
                DataAccessor.Create(entity).GetAwaiter().GetResult();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            DatabaseConnectionFactory.GetConnection().CloseAsync().GetAwaiter().GetResult();
            File.Delete(_databaseName);
        }
    }
}
