using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DataAccess.Registration
{
    // TODO: make public interface and internal implementation
    public class ConnectionRegistration
    {
        internal ConnectionRegistration() { }

        public ConnectionRegistration AddTables(IEnumerable<Type> entityTypes, bool allowUnusableTypes = false)
        {
            if (!allowUnusableTypes && entityTypes.FirstOrDefault(t => !t.IsUsableEntity()) is Type unusableTypeType)
            {
                throw new InvalidOperationException($"Type {unusableTypeType.Name} is not valid for use as a SimpleSQLite entity");
                // TODO: Link to documentation?
            }
            DatabaseConnectionFactory.AddTables(entityTypes.Where(t => t.IsUsableEntity()));
            return this;
        }

        public ConnectionRegistration AddTable(Type entityType, bool allowUnusableTypes = false)
        {
            return AddTables(new List<Type> { entityType }, allowUnusableTypes);
        }

        // TODO: Option to add indexes
        // TODO: Possible to add indexes based on specifications?
    }
}
