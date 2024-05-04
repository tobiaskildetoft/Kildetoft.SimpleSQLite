using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DataAccess.DataContracts;

namespace TimeTracker.DataAccess.Registration
{
    internal static class EntityValidator
    {
        internal static bool IsUsableEntity(this Type entityType)
        {
            // TODO: Could it be possible to skip all this and instead add the attributes
            // TODO: Maybe completely remove the dependency on anything external for the entities
            if (!entityType.IsAssignableFrom(typeof(IEntity)))
            {
                return false;
            }
            if (entityType.GetCustomAttribute<TableAttribute>() == null)
            {
                return false;
            }
            if (entityType.GetProperty("Id")?.GetCustomAttribute<PrimaryKeyAttribute>() == null)
            {
                return false;
            }
            return true;
        }
    }
}
