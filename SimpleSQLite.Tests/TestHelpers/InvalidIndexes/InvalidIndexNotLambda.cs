using Kildetoft.SimpleSQLite;
using SimpleSQLite.Samples.Entities;
using System.Linq.Expressions;


namespace SimpleSQLite.Tests.TestHelpers.InvalidIndexes;

internal class InvalidIndexNotLambda : IIndex<SampleEntity>
{
    public bool Unique => false;
    public Expression<Func<SampleEntity, object>> IndexDefinition => null;
}
