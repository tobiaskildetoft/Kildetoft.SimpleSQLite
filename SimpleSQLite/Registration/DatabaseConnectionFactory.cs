using SQLite;
using System.Linq.Expressions;
using System.Reflection;

namespace Kildetoft.SimpleSQLite.IoC;

internal static class DatabaseConnectionFactory
{
    private static SQLiteAsyncConnection? _connection;
    private static SQLiteConnection? _initializationConnection;

    internal static SQLiteAsyncConnection GetConnection()
    {
        _initializationConnection?.Close();
        return _connection ?? throw new NotImplementedException();
        // TODO: Custom exception here
    }

    internal static void Initialize(string connectionString)
    {
        _connection = new SQLiteAsyncConnection(connectionString);
        _initializationConnection = new SQLiteConnection(connectionString);
    }

    internal static void AddTables(IEnumerable<Type> entityTypes)
    {
        if (_initializationConnection == null)
        {
            throw new InvalidOperationException("Cannot add tables before initializing the connection");
        }
        foreach (var type in entityTypes)
        {
            _initializationConnection.CreateTable(type);
        }
    }

    internal static void AddIndex(string tableName, string attributeName, bool unique)
    {
        // TODO: Handle case where table has not been added before index
        if (_initializationConnection == null)
        {
            throw new InvalidOperationException("Cannot add indexes before initializing the connection");
        }
        _initializationConnection.CreateIndex(tableName, attributeName, unique);
    }
}
