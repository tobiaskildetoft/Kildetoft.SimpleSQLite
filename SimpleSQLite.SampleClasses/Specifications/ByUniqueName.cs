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
    public class ByUniqueName : IWhereSpecification<SampleEntity>, IFirstOrDefaultSpecification<SampleEntity>
    {
        private readonly string _uniqueName;

        public ByUniqueName(string uniqueName)
        {
            _uniqueName = uniqueName;
        }

        public Expression<Func<SampleEntity, bool>> WhereExpression => x => x.UniqueName == _uniqueName;
    }
}
