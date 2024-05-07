namespace Kildetoft.SimpleSQLite;

public interface ISelectFirstOrDefaultSpecification<T, S> : ISelectSpecification<T, S>, IFirstOrDefaultSpecification<T> where T : IEntity
{
}
