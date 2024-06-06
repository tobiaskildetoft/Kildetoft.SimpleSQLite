using Kildetoft.SimpleSQLite.Exceptions;
using Kildetoft.SimpleSQLite.ReflectionHelpers;
using SQLite;
using System.Linq.Expressions;
using System.Reflection;

namespace Kildetoft.SimpleSQLite.IoC;

internal static class DatabaseConnectionFactory
{
    private static string? _connectionString;
    private static ISQLiteAsyncConnection? _asyncConnection;
    private static ISQLiteConnection? _connection;

    internal static ISQLiteAsyncConnection GetAsyncConnection()
    {
        return _asyncConnection ?? throw new NotInitializedException("The async connection was accessed before being initialized or after being closed");
    }

    internal static ISQLiteConnection GetConnection()
    {
        return _connection ?? throw new NotInitializedException("The connection was accessed before being initialized or after being closed");
    }

    internal static void Initialize(string connectionString)
    {
        try
        {
            _asyncConnection = new SQLiteAsyncConnection(connectionString);
            _connection = new SQLiteConnection(connectionString);
        }
        catch (SQLiteException e)
        {
            throw new InvalidConnectionStringException("SQLite was not able to create a connection using the provided connection string. See inner Exception for details", e);
        }
        _connectionString = connectionString;
    }

    internal static void AddTables(IEnumerable<Type> entityTypes)
    {
        if (_connection == null)
        {
            throw new InvalidOperationException("Cannot add tables before initializing the connection");
        }
        foreach (var type in entityTypes)
        {
            // TODO: What if a table already exists, but the model has been changed?
            // TODO: Find good way to handle this
            _connection.CreateTable(type);
        }
    }

    internal static void AddIndex(Type indexType)
    {
        var indexDefinition = SQLiteIndexes.GetIndexDefinition(indexType);

        AddIndex(indexDefinition.TableName!, indexDefinition.AttributeName!, indexDefinition.Unique);
    }

    private static void AddIndex(string tableName, string attributeName, bool unique)
    {
        if (_connection == null)
        {
            throw new InvalidOperationException("Cannot add indexes before initializing the connection or after starting to use the connection");
        }
        _connection.CreateIndex(tableName, attributeName, unique);
    }

    internal static void CloseConnections()
    {
        _connection?.Close();
        _connection = null;
    }
}
