using Kildetoft.SimpleSQLite.IoC;
using SQLite;

namespace Kildetoft.SimpleSQLite.TestHelpers;

/// <summary>
/// Mock setup of a database for use in testing
/// Will use a database with a random filename and delete the file on Dispose or when GC cleans it up
/// </summary>
public class DataAccessMock<T> : IDisposable where T : IEntity, new()
{
    private readonly string _databaseName;
    private readonly ISQLiteAsyncConnection _connection;
    private bool _disposed;
    public IDataAccessor DataAccessor { get; private set; }
    /// <summary>
    /// Initialize a connection with a table for entities of type T, containing all the supplied entities initially
    /// </summary>
    public DataAccessMock(IEnumerable<T> entities)
    {
        _databaseName = $"{Guid.NewGuid()}.db";
        DatabaseConnectionFactory.Initialize(_databaseName);
        DatabaseConnectionFactory.AddTables([typeof(T)]);

        DataAccessor = new DataAccessor();
        _connection = DatabaseConnectionFactory.GetConnection();

        foreach (var entity in entities)
        {
            DataAccessor.Create(entity).GetAwaiter().GetResult();
        }

        // TODO: Add support for indexes to be able to also test these
    }

    ~DataAccessMock() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
        _connection.CloseAsync().GetAwaiter().GetResult();
        File.Delete(_databaseName);
        _disposed = true;
    }
}
