using Kildetoft.SimpleSQLite.Exceptions;
using SQLite;

namespace Kildetoft.SimpleSQLite.IoC;

internal static class DatabaseConnectionFactory
{
    private static SQLiteAsyncConnection? _connection;
    private static SQLiteConnection? _initializationConnection;

    internal static SQLiteAsyncConnection GetConnection()
    {
        _initializationConnection?.Close();
        _initializationConnection = null;
        return _connection ?? throw new NotInitializedException("The connection was accessed before being initialized");
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
            throw new InvalidOperationException("Cannot add tables before initializing the connection or after starting to use the connection");
        }
        foreach (var type in entityTypes)
        {
            _initializationConnection.CreateTable(type);
        }
    }

    internal static void AddIndex(string tableName, string attributeName, bool unique)
    {
        if (_initializationConnection == null)
        {
            throw new InvalidOperationException("Cannot add indexes before initializing the connection or after starting to use the connection");
        }
        _initializationConnection.CreateIndex(tableName, attributeName, unique);
    }
}
