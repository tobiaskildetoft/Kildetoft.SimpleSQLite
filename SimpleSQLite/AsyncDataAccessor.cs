using Kildetoft.SimpleSQLite.IoC;
using Kildetoft.SimpleSQLite.QueryHelpers;
using SQLite;

namespace Kildetoft.SimpleSQLite;

internal class AsyncDataAccessor : IAsyncDataAccessor
{
    private readonly ISQLiteAsyncConnection _db;

    public AsyncDataAccessor()
    {
        _db = DatabaseConnectionFactory.GetAsyncConnection();
    }

    public async Task<T> CreateAsync<T>(T entity) where T : IEntity, new()
    {
        await _db.InsertAsync(entity);
        return entity;
    }

    public async Task<T?> GetByIdAsync<T>(int id) where T : IEntity, new()
    {
        return await _db.Table<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync<T>(T entity) where T : IEntity, new()
    {
        await _db.UpdateAsync(entity);
    }

    public async Task DeleteAsync<T>(int id) where T : IEntity, new()
    {
        await _db.DeleteAsync<T>(id);
    }

    public async Task<IList<T>> GetAsync<T>(IAllSpecification<T> specification) where T : IEntity, new()
    {
        var query = _db.Table<T>().ApplySpecification(specification);
        return await query.ToListAsync();
    }

    public async Task<T> GetAsync<T>(IFirstSpecification<T> specification) where T : IEntity, new()
    {
        var query = _db.Table<T>().ApplySpecification(specification);
        return await query.FirstAsync();
    }

    public async Task<T?> GetAsync<T>(IFirstOrDefaultSpecification<T> specification) where T : IEntity, new()
    {
        var query = _db.Table<T>().ApplySpecification(specification);
        return await query.FirstOrDefaultAsync();
    }
}
