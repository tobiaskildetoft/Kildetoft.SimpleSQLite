namespace Kildetoft.SimpleSQLite;

public interface ISelectFirstSpecification<T, S> : ISelectSpecification<T, S>, IFirstSpecification<T> where T : IEntity
{
}
