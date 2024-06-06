using Kildetoft.SimpleSQLite;
using SimpleSQLite.Samples.Entities;
using System.Linq.Expressions;

namespace SimpleSQLite.Tests.TestHelpers.InvalidIndexes;

internal class InvalidIndexNotMemberExpression : IIndex<SampleEntity>
{
    public bool Unique => true;
    public Expression<Func<SampleEntity, object>> IndexDefinition => x => x;
}
