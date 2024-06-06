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
    public class OrderedBySomeIntAscending : IOrderSpecification<SampleEntity>
    {
        public OrderType OrderType => OrderType.Ascending;
        public Expression<Func<SampleEntity, IComparable>> OrderExpression => x => x.SomeInt;
    }
}
