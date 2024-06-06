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
    private readonly ISQLiteConnection _connection;
    private readonly ISQLiteAsyncConnection _asyncConnection;
    private bool _disposed;
    public IDataAccessor DataAccessor { get; private set; }
    public IAsyncDataAccessor AsyncDataAccessor { get; private set; }
    /// <summary>
    /// Initialize a connection with a table for entities of type T, containing all the supplied entities initially
    /// </summary>
    public DataAccessMock(IEnumerable<T>? entities = null, IEnumerable<IIndex<T>>? indexes = null)
    {
        try
        {
            _databaseName = $"{Guid.NewGuid()}.db";
            DatabaseConnectionFactory.Initialize(_databaseName);

            _connection = DatabaseConnectionFactory.GetConnection();
            _asyncConnection = DatabaseConnectionFactory.GetAsyncConnection();

            DatabaseConnectionFactory.AddTables([typeof(T)]);
            DataAccessor = new DataAccessor();
            AsyncDataAccessor = new AsyncDataAccessor();

            foreach (var index in indexes ?? Enumerable.Empty<IIndex<T>>())
            {
                DatabaseConnectionFactory.AddIndex(index.GetType());
            }

            foreach (var entity in entities ?? Enumerable.Empty<T>())
            {
                DataAccessor.Create(entity);
            }
        }
        catch
        {
            _connection?.Close();
            _asyncConnection?.CloseAsync();
            File.Delete(_databaseName!);

            throw;
        }
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
        _connection.Close();
        _asyncConnection.CloseAsync().GetAwaiter().GetResult();
        File.Delete(_databaseName);
        _disposed = true;
    }
}
