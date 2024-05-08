﻿using SQLite;
using System.Linq.Expressions;
using System.Reflection;

namespace Kildetoft.SimpleSQLite.IoC;

internal class ConnectionRegistration : IConnectionRegistration
{
    public IConnectionRegistration AddTables(IEnumerable<Type> entityTypes, bool allowUnusableTypes = false)
    {
        if (!allowUnusableTypes && entityTypes.FirstOrDefault(t => !t.IsUsableEntity()) is Type unusableTypeType)
        {
            throw new InvalidOperationException($"Type {unusableTypeType.Name} is not valid for use as a SimpleSQLite entity");
            // TODO: Link to documentation?
        }
        DatabaseConnectionFactory.AddTables(entityTypes.Where(t => t.IsUsableEntity()));
        return this;
    }

    public IConnectionRegistration AddTable(Type entityType, bool allowUnusableTypes = false)
    {
        return AddTables(new List<Type> { entityType }, allowUnusableTypes);
    }

    public IConnectionRegistration AddIndex(Type indexType)
    {
        // TODO: methods for adding multiple, and methods for adding from assemblycontaining
        if (Activator.CreateInstance(indexType) is IIndex<IEntity> index)
        {
            var tableName = GetTableNameFromGenericIndexType(indexType);
            var attributeName = GetAttributeNameFromIndex(index);
            DatabaseConnectionFactory.AddIndex(tableName, attributeName, index.Unique);
        }
        return this;
    }

    private string GetTableNameFromGenericIndexType(Type indexType)
    {
        // TODO: null handling and some try-catch
        var entitytype = indexType.GetGenericArguments().First();
        var tableAttribute = entitytype.GetCustomAttribute<TableAttribute>();
        return tableAttribute.Name;
    }

    private string GetAttributeNameFromIndex(IIndex<IEntity> index)
    {
        if (index.IndexDefinition.Body is MemberExpression memberExpression && memberExpression.Member is PropertyInfo propertyInfo)
        {
            return propertyInfo.Name;
        }
        throw new NotSupportedException();
        // TODO: Throw proper custom exception with proper info
    }

    // TODO: Option to add indexes
    // TODO: Possible to add indexes based on specifications? Seems doable, but probably not worth it, as it is better to be explicit about index creation

    // TODO: Method that adds all entities and indexes from an assembly based on a type from that assembly. What will be a good name for that?
}
