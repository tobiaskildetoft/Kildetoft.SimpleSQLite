namespace Kildetoft.SimpleSQLite;

public interface IDataAccessor
{
    Task<T?> GetById<T>(int id) where T : IEntity, new();
    Task<IList<T>> GetAllWhere<T>(ISpecification<T> specification) where T : IEntity, new();
    Task<T> GetFirstOrDefaultWhere<T>(ISpecification<T> specification) where T : IEntity, new();
    Task<T> GetFirstWhere<T>(ISpecification<T> specification) where T : IEntity, new();

    Task<T> Create<T>(T entity) where T : IEntity, new();
    Task Update<T>(T entity) where T : IEntity, new();
    Task Delete<T>(int id) where T : IEntity, new();
}
