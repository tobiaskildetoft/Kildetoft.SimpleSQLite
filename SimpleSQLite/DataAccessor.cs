using Kildetoft.SimpleSQLite.IoC;
using Kildetoft.SimpleSQLite.QueryHelpers;
using SQLite;

namespace Kildetoft.SimpleSQLite;

internal class DataAccessor : IDataAccessor
{
    private readonly ISQLiteConnection _db;

    public DataAccessor()
    {
        _db = DatabaseConnectionFactory.GetConnection();
    }

    public T Create<T>(T entity) where T : IEntity, new()
    {
        _db.Insert(entity);
        return entity;
    }

    public T? GetById<T>(int id) where T: IEntity, new()
    {
        return _db.Table<T>().FirstOrDefault(x => x.Id == id);
    }

    public void Update<T>(T entity) where T : IEntity, new()
    { 
        _db.Update(entity);
    }

    public void Delete<T>(int id) where T : IEntity, new()
    {
        _db.Delete<T>(id);
    }

    public IList<T> Get<T>(IAllSpecification<T> specification) where T : IEntity, new()
    {
        var query = _db.Table<T>().ApplySpecification(specification);
        return query.ToList();
    }

    public T Get<T>(IFirstSpecification<T> specification) where T : IEntity, new()
    {
        var query = _db.Table<T>().ApplySpecification(specification);
        return query.First();
    }

    public T? Get<T>(IFirstOrDefaultSpecification<T> specification) where T : IEntity, new()
    {
        var query = _db.Table<T>().ApplySpecification(specification);
        return query.FirstOrDefault();
    }
}
