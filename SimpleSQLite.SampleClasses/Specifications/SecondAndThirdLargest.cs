using Kildetoft.SimpleSQLite;
using SimpleSQLite.Samples.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSQLite.Samples.Specifications
{
    public class SecondAndThirdLargest : OrderedBySomeIntDescending, ITakeSpecification<SampleEntity>, ISkipSpecification<SampleEntity>
    {
        public int NumberToSkip => 1;
        public int NumberToTake => 2;
    }
}
