using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DataAccess.DataContracts;

namespace TimeTracker.DataAccess.EntityHelpers
{
    public static class SQLiteEntities
    {
        public static IEnumerable<Type> FromAssemblyContaining<T>() where T : IEntity, new()
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
}
