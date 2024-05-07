using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kildetoft.SimpleSQLite;

public interface IWhereSpecification<T> : ISpecification<T> where T : IEntity
{
    Expression<Func<T, bool>> WhereExpression { get; }
}
