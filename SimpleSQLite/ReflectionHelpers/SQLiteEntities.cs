using System.Reflection;

namespace Kildetoft.SimpleSQLite.ReflectionHelpers;

internal static class SQLiteEntities
{
    internal static IEnumerable<Type> FromAssemblyContaining<T>()
    {
        var assembly = typeof(T).Assembly;
        return FromAssembly(assembly);
    }

    internal static IEnumerable<Type> FromAssembly(Assembly assembly)
    {
        return assembly.ExportedTypes.Where(t => t.IsAssignableTo(typeof(IEntity)));
    }
}
