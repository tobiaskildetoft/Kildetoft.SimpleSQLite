namespace Kildetoft.SimpleSQLite;

public interface IDataAccessor
{
    Task<T?> GetById<T>(int id) where T : IEntity, new();

    Task<T> Create<T>(T entity) where T : IEntity, new();
    Task Update<T>(T entity) where T : IEntity, new();
    Task Delete<T>(int id) where T : IEntity, new();

    Task<IList<T>> Get<T>(IAllSpecification<T> specification) where T : IEntity, new();
    Task<T> Get<T>(IFirstSpecification<T> specification) where T : IEntity, new();
    Task<T?> Get<T>(IFirstOrDefaultSpecification<T> specification) where T : IEntity, new();
}
