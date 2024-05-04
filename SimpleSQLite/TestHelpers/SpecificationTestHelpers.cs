using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DataAccess.DataContracts;
using TimeTracker.Specifications.Base;

namespace TimeTracker.DataAccess.TestHelpers
{
    public static class SpecificationTestHelpers
    {
        public static bool IsValidSQLiteSpecification<T>(ISpecification<T> specification) where T : IEntity
        {
            // TODO: Check that the IsSatisfied expression only uses those things it can for use in SQLite
            throw new NotImplementedException();
        }
    }
}
