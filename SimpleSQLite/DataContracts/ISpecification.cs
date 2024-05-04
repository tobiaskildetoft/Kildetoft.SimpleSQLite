using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DataAccess.DataContracts;

namespace TimeTracker.Specifications.Base
{
    public interface ISpecification<T> where T : IEntity
    {
        Expression<Func<T, bool>> IsSatisfied { get; }
    }
}
