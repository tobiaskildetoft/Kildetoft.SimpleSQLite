namespace Kildetoft.SimpleSQLite;

public interface ISkipSpecification<T> : ISpecification<T> where T : IEntity
{
    int NumberToSkip { get; }
}
