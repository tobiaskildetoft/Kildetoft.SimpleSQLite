using System.Reflection;

namespace Kildetoft.SimpleSQLite.IoC;

public static class SQLiteEntities
{
    public static IEnumerable<Type> FromAssemblyContaining<T>()
    {
        var assembly = typeof(T).Assembly;
        return FromAssembly(assembly);
    }

    public static IEnumerable<Type> FromAssembly(Assembly assembly)
    {
        return assembly.ExportedTypes.Where(t => t.IsAssignableFrom(typeof(IEntity)));
    }

    public static IEnumerable<Type> FromAssemblyNamed(string assemblyName)
    {
        var assembly = Assembly.Load(assemblyName);
        return FromAssembly(assembly);
    }
}
