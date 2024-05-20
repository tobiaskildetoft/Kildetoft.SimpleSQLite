using Kildetoft.SimpleSQLite.Exceptions;
using SQLite;

namespace Kildetoft.SimpleSQLite.IoC;

internal static class DatabaseConnectionFactory
{
    private static ISQLiteAsyncConnection? _asyncConnection;
    private static ISQLiteConnection? _connection;

    internal static ISQLiteAsyncConnection GetAsyncConnection()
    {
        return _asyncConnection ?? throw new NotInitializedException("The async connection was accessed before being initialized");
    }

    internal static ISQLiteConnection GetConnection()
    {
        return _connection ?? throw new NotInitializedException("The connection was accessed before being initialized");
    }

    internal static void Initialize(string connectionString)
    {
        _asyncConnection = new SQLiteAsyncConnection(connectionString);
        _connection = new SQLiteConnection(connectionString);
    }

    internal static void AddTables(IEnumerable<Type> entityTypes)
    {
        if (_connection == null)
        {
            throw new InvalidOperationException("Cannot add tables before initializing the connection or after starting to use the connection");
        }
        foreach (var type in entityTypes)
        {
            _connection.CreateTable(type);
        }
    }

    internal static void AddIndex(string tableName, string attributeName, bool unique)
    {
        if (_connection == null)
        {
            throw new InvalidOperationException("Cannot add indexes before initializing the connection or after starting to use the connection");
        }
        _connection.CreateIndex(tableName, attributeName, unique);
    }
}
