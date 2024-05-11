using Kildetoft.SimpleSQLite.IoC;
using System.Reflection;

namespace Kildetoft.SimpleSQLite.ReflectionHelpers;

internal static class SQLiteIndexes
{
    internal static IEnumerable<Type> FromAssemblyContaining<T>()
    {
        var assembly = typeof(T).Assembly;
        return FromAssembly(assembly);
    }

    internal static IEnumerable<Type> FromAssembly(Assembly assembly)
    {
        return assembly.ExportedTypes.Where(IsIndex);
    }

    internal static bool IsIndex(Type type)
    {
        return
        type.GetConstructor(Type.EmptyTypes) != null &&
        type.GetInterfaces().Any(x =>
        x.IsGenericType &&
        x.GetGenericTypeDefinition() == typeof(IIndex<>) &&
        x.GetGenericArguments().First().IsUsableEntity());
    }
}
