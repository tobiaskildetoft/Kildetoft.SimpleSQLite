namespace Kildetoft.SimpleSQLite;

public interface ITakeSpecification<T> : IAllSpecification<T> where T : IEntity
{
    int NumberToTake { get; }
}
