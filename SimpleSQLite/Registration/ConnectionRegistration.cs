using Kildetoft.SimpleSQLite.ReflectionHelpers;
using SQLite;
using System.Linq.Expressions;
using System.Reflection;

namespace Kildetoft.SimpleSQLite.IoC;

internal class ConnectionRegistration : IConnectionRegistration
{
    public IConnectionRegistration AddTablesFromAssemblyContaining<T>()
    {
        var types = SQLiteEntities.FromAssemblyContaining<T>();
        return AddTables(types);
    }

    public IConnectionRegistration AddIndexFromAssemblyContaining<T>()
    {
        var types = SQLiteIndexes.FromAssemblyContaining<T>();
        return AddIndexes(types);
    }

    public IConnectionRegistration AddAllFromAssemblyContaining<T>()
    {
        AddTablesFromAssemblyContaining<T>();
        AddIndexFromAssemblyContaining<T>();
        return this;
    }

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
        return AddTables([entityType], allowUnusableTypes);
    }

    public IConnectionRegistration AddIndexes(IEnumerable<Type> indexTypes)
    {
        foreach (var indexType in indexTypes)
        {
            // TODO: Make more thorough check that makes sure the index is a usable type
            if (!SQLiteIndexes.IsIndex(indexType))
            {
                throw new NotSupportedException();
                // TODO: custom exception with more info
            }
            var tableName = GetTableNameFromGenericIndexType(indexType);
            var attributeName = GetAttributeNameFromIndexType(indexType);
            var unique = GetUniqueFromIndexType(indexType);
            DatabaseConnectionFactory.AddIndex(tableName, attributeName, unique);
        }
        return this;
    }

    public IConnectionRegistration AddIndex<T>() where T : IIndex<IEntity>
    {
        return AddIndex(typeof(T));
    }

    public IConnectionRegistration AddIndex(Type indexType)
    {
        return AddIndexes([indexType]);
    }

    private bool GetUniqueFromIndexType(Type indexType)
    {
        if (Activator.CreateInstance(indexType) is IIndex index)
        {
            return index.Unique;
        }
        throw new NotSupportedException();
        // TODO: Better exception (or maybe have this method assume it is an index based on it being private?
    }

    private string GetTableNameFromGenericIndexType(Type indexType)
    {
        // TODO: null handling and some try-catch
        var entitytype = indexType.GetGenericArguments().First();
        var tableAttribute = entitytype.GetCustomAttribute<TableAttribute>();
        return tableAttribute.Name;
    }

    private string GetAttributeNameFromIndexType(Type indexType)
    {
        var indexObject = Activator.CreateInstance(indexType);
        var indexDefinitionProperty = indexType.GetProperty(SQLiteIndexes.IndexDefinitionName);
        if (indexDefinitionProperty.GetValue(indexObject) is LambdaExpression expression)
        {
            return GetAttributeNameFromLambdaExpression(expression);
        }
        throw new NotSupportedException();
        // TODO: Better custom exception
    }

    private string GetAttributeNameFromLambdaExpression(LambdaExpression expression)
    {
        if (expression.Body is MemberExpression memberExpression && memberExpression.Member is PropertyInfo propertyInfo)
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
