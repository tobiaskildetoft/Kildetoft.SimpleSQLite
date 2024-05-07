namespace Kildetoft.SimpleSQLite;

public interface ITakeSpecification<T> : ISpecification<T> where T : IEntity
{
    int NumberToTake { get; }
}
