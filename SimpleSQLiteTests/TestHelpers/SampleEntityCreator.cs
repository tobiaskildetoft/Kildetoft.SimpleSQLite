using SimpleSQLite.Samples.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSQLite.Tests.TestHelpers
{
    internal static class SampleEntityCreator
    {
        internal static IEnumerable<SampleEntity> GetSampleEntities()
        {
            var entities = new List<SampleEntity>();
            entities.Add(new SampleEntity { Name = "Name1", UniqueName = "UniqueName1", SomeDate = new DateTime(1999, 1, 1), SomeNullableDate = new DateTime(1999, 1, 1), SomeInt = 1 });
            entities.Add(new SampleEntity { Name = "Name1", UniqueName = "UniqueName2", SomeDate = new DateTime(1999, 1, 1), SomeNullableDate = new DateTime(1999, 1, 1), SomeInt = 2 });
            entities.Add(new SampleEntity { Name = "Name2", UniqueName = "UniqueName3", SomeDate = new DateTime(1999, 1, 1), SomeNullableDate = new DateTime(1999, 1, 1), SomeInt = 2 });
            entities.Add(new SampleEntity { Name = "Name2", UniqueName = "UniqueName4", SomeDate = new DateTime(1999, 1, 1), SomeNullableDate = new DateTime(1999, 1, 1), SomeInt = 3 });
            entities.Add(new SampleEntity { Name = "Name3", UniqueName = "UniqueName5", SomeDate = new DateTime(1999, 1, 1), SomeNullableDate = new DateTime(1999, 1, 1), SomeInt = 4 });
            entities.Add(new SampleEntity { Name = "Name3", UniqueName = "UniqueName6", SomeDate = new DateTime(1999, 1, 1), SomeNullableDate = new DateTime(1999, 1, 1), SomeInt = 5 });
            entities.Add(new SampleEntity { Name = "Name4", UniqueName = "UniqueName7", SomeDate = new DateTime(1999, 1, 1), SomeNullableDate = new DateTime(1999, 1, 1), SomeInt = 6 });
            return entities;
        }
    }
}
