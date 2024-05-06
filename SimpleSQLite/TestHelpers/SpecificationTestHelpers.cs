namespace Kildetoft.SimpleSQLite.TestHelpers;

public static class SpecificationTestHelpers
{
    public static bool IsValidSQLiteSpecification<T>(ISpecification<T> specification) where T : IEntity
    {
        // TODO: Check that the IsSatisfied expression only uses those things it can for use in SQLite
        throw new NotImplementedException();
    }
}
