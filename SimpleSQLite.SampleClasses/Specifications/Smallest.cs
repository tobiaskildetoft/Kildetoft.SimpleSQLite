using Kildetoft.SimpleSQLite;
using SimpleSQLite.Samples.Entities;

namespace SimpleSQLite.Samples.Specifications;

public class Smallest : OrderedBySomeIntAscending, IFirstSpecification<SampleEntity>
{
}
