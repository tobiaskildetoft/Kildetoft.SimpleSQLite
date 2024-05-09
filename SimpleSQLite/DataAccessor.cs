using Kildetoft.SimpleSQLite.IoC;
using Kildetoft.SimpleSQLite.QueryHelpers;
using SQLite;

namespace Kildetoft.SimpleSQLite;

internal class DataAccessor : IDataAccessor
{
    private readonly ISQLiteAsyncConnection _db;

    public DataAccessor()
    {
        _db = DatabaseConnectionFactory.GetConnection();
    }

    public async Task<T> Create<T>(T entity) where T : IEntity, new()
    {
        await _db.InsertAsync(entity);
        return entity;
    }

    public async Task<T?> GetById<T>(int id) where T: IEntity, new()
    {
        return await _db.Table<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Update<T>(T entity) where T : IEntity, new()
    { 
        await _db.UpdateAsync(entity);
    }

    public async Task Delete<T>(int id) where T : IEntity, new()
    {
        await _db.DeleteAsync<T>(id);
    }

    public async Task<IList<T>> Get<T>(IAllSpecification<T> specification) where T : IEntity, new()
    {
        var query = _db.Table<T>().ApplySpecification(specification);
        return await query.ToListAsync();
    }

    public async Task<T> Get<T>(IFirstSpecification<T> specification) where T : IEntity, new()
    {
        var query = _db.Table<T>().ApplySpecification(specification);
        return await query.FirstAsync();
    }

    public async Task<T?> Get<T>(IFirstOrDefaultSpecification<T> specification) where T : IEntity, new()
    {
        var query = _db.Table<T>().ApplySpecification(specification);
        return await query.FirstOrDefaultAsync();
    }
}
