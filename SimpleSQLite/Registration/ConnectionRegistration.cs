using Kildetoft.SimpleSQLite.Exceptions;
using Kildetoft.SimpleSQLite.ReflectionHelpers;
using Microsoft.Extensions.DependencyInjection;
using SQLite;
using System.Linq.Expressions;
using System.Reflection;

namespace Kildetoft.SimpleSQLite.IoC;

internal class ConnectionRegistration : IConnectionRegistration
{
    internal ConnectionRegistration(IServiceCollection serviceCollection)
    {
        ServiceCollection = serviceCollection;
    }

    public IServiceCollection ServiceCollection { get; private set; }

    public IConnectionRegistration AddTablesFromAssemblyContaining<T>()
    {
        var types = SQLiteEntities.FromAssemblyContaining<T>();
        return AddTables(types);
    }

    public IConnectionRegistration AddIndexesFromAssemblyContaining<T>()
    {
        var types = SQLiteIndexes.FromAssemblyContaining<T>();
        return AddIndexes(types);
    }

    public IConnectionRegistration AddAllFromAssemblyContaining<T>()
    {
        AddTablesFromAssemblyContaining<T>();
        AddIndexesFromAssemblyContaining<T>();
        return this;
    }

    public IConnectionRegistration AddTable<T>() where T: IEntity, new()
    {
        return AddTable(typeof(T));
    }

    public IConnectionRegistration AddTable(Type entityType)
    {
        return AddTables([entityType]);
    }

    public IConnectionRegistration AddTables(IEnumerable<Type> entityTypes)
    {
        if (entityTypes.FirstOrDefault(t => !t.IsUsableEntity()) is Type unusableType)
        {
            throw new UnsupportedEntityImplementationException($"Type {unusableType.Name} is not valid for use as a SimpleSQLite entity");
        }
        DatabaseConnectionFactory.AddTables(entityTypes);
        return this;
    }

    public IConnectionRegistration AddIndex<T>() where T : IIndex, new()
    {
        return AddIndex(typeof(T));
    }

    public IConnectionRegistration AddIndex(Type indexType)
    {
        return AddIndexes([indexType]);
    }

    public IConnectionRegistration AddIndexes(IEnumerable<Type> indexTypes)
    {
        foreach (var indexType in indexTypes)
        {
            if (!SQLiteIndexes.IsIndex(indexType))
            {
                throw new UnsupportedIndexImplementationException($"The supplied type {indexType.Name} does not implement IIndex<T> for an IEntity T");
            }
            var tableName = GetTableNameFromGenericIndexType(indexType);
            var attributeName = GetAttributeNameFromIndexType(indexType);
            var unique = GetUniqueFromIndexType(indexType);
            DatabaseConnectionFactory.AddIndex(tableName, attributeName, unique);
        }
        return this;
    }

    private bool GetUniqueFromIndexType(Type indexType)
    {
        if (Activator.CreateInstance(indexType) is IIndex index)
        {
            return index.Unique;
        }
        throw new UnsupportedIndexImplementationException($"The supplied type {indexType.Name} could not be created as an IIndex");
    }

    private string GetTableNameFromGenericIndexType(Type indexType)
    {
        var entitytype = indexType.GetGenericArguments().First();
        var tableAttribute = entitytype.GetCustomAttribute<TableAttribute>();

        return tableAttribute!.Name;
    }

    private string GetAttributeNameFromIndexType(Type indexType)
    {
        var indexObject = Activator.CreateInstance(indexType);
        var indexDefinitionProperty = indexType.GetProperty(IIndex.IndexDefinitionName);
        if (indexDefinitionProperty?.GetValue(indexObject) is LambdaExpression expression)
        {
            return GetAttributeNameFromLambdaExpression(expression);
        }
        throw new UnsupportedIndexImplementationException($"The field {IIndex.IndexDefinitionName} on the type {indexType.Name} is not a LambdaExpression");
    }

    private string GetAttributeNameFromLambdaExpression(LambdaExpression expression)
    {
        if (expression.Body is MemberExpression memberExpression && memberExpression.Member is PropertyInfo propertyInfo)
        {
            return propertyInfo.Name;
        }
        throw new UnsupportedIndexImplementationException($"The LambdaExpression {expression} does not represent accessing a property on the entity");
    }
}
