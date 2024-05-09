using Kildetoft.SimpleSQLite;
using SimpleSQLite.Samples.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSQLite.Samples.Indexes
{
    public class NameIndex : IIndex<SampleEntity>
    {
        public bool Unique => false;
        public Expression<Func<SampleEntity, object>> IndexDefinition => x => x.Name;
    }
}
