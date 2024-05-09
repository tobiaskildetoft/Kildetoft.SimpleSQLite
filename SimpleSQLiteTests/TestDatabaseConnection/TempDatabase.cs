using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSQLite.Tests.TestDatabaseConnection
{
    public class TempDatabase : IDisposable
    { 
        private readonly string _databaseName;
        public ISQLiteAsyncConnection Connection { get; private set; }
        public TempDatabase() 
        {
            _databaseName = $"{Guid.NewGuid()}.db";
            Connection = new SQLiteAsyncConnection(_databaseName);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            File.Delete(_databaseName);
        }
    }
}
