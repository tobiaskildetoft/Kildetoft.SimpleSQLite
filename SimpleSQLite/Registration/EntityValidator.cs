using SQLite;
using System.Reflection;

namespace Kildetoft.SimpleSQLite.IoC;

internal static class EntityValidator
{
    internal static bool IsUsableEntity(this Type entityType)
    {
        if (!entityType.IsAssignableTo(typeof(IEntity)))
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
        if (entityType.GetConstructor(Type.EmptyTypes) == null)
        {
            return false;
        }
        return true;
    }
}
