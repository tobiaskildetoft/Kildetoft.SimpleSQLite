using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kildetoft.SimpleSQLite.ReflectionHelpers
{
    public static class SQLiteIndexes
    {
        internal const string IndexDefinitionName = "IndexDefinition";
        public static IEnumerable<Type> FromAssemblyContaining<T>()
        {
            var assembly = typeof(T).Assembly;
            return FromAssembly(assembly);
        }

        public static IEnumerable<Type> FromAssembly(Assembly assembly)
        {
            return assembly.ExportedTypes.Where(t => IsIndex(t));
        }

        internal static bool IsIndex(Type type)
        {
            return type.GetInterfaces().Any(x =>
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == typeof(IIndex<>));
        }
    }
}
