using Kildetoft.SimpleSQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSQLite.Samples.Entities
{
    public class SampleEntity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UniqueName { get; set; } = string.Empty;
        public DateTime SomeDate { get; set; }
        public DateTime? SomeNullableDate { get; set; }
        public int SomeInt { get; set; }
    }
}
