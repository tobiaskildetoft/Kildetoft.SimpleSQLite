namespace Kildetoft.SimpleSQLite;

public interface ISelectAllSpecification<T, S> : ISelectSpecification<T, S>, IAllSpecification<T> where T : IEntity
{
}
