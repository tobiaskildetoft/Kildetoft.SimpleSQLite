using Kildetoft.SimpleSQLite;
using SimpleSQLite.Samples.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSQLite.Samples.Specifications
{
    public class OrderedBySomeIntDescending : IOrderSpecification<SampleEntity>
    {
        public OrderType OrderType => OrderType.Descending;
        public Expression<Func<SampleEntity, IComparable>> OrderExpression => x => x.SomeInt;
    }
}
