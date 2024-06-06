using Kildetoft.SimpleSQLite.Exceptions;
using Kildetoft.SimpleSQLite.IoC;
using SQLite;
using System.Linq.Expressions;
using System.Reflection;

namespace Kildetoft.SimpleSQLite.ReflectionHelpers;

internal static class SQLiteIndexes
{
    internal static IEnumerable<Type> FromAssemblyContaining<T>()
    {
        var assembly = typeof(T).Assembly;
        return FromAssembly(assembly);
    }

    private static IEnumerable<Type> FromAssembly(Assembly assembly)
    {
        return assembly.ExportedTypes.Where(IsIndex);
    }

    private static bool IsIndex(Type type)
    {
        return
        type.GetConstructor(Type.EmptyTypes) != null &&
        GetEntityType(type)?.IsUsableEntity() == true;
    }

    private static Type? GetEntityType(Type type)
    {
        var indexInterface = type.GetInterfaces().FirstOrDefault(x =>
        x.IsGenericType &&
        x.GetGenericTypeDefinition() == typeof(IIndex<>));
        return indexInterface?.GetGenericArguments()[0];
    }

    internal static IndexDefinition GetIndexDefinition(Type indexType)
    {
        if (!IsIndex(indexType))
        {
            throw new UnsupportedIndexImplementationException($"The supplied type {indexType.Name} does not implement IIndex<T> for an IEntity T");
        }

        return new IndexDefinition
        {
            TableName = GetTableNameFromGenericIndexType(indexType),
            AttributeName = GetAttributeNameFromIndexType(indexType),
            Unique = GetUniqueFromIndexType(indexType)
        };
    }

    private static bool GetUniqueFromIndexType(Type indexType)
    {
        return (Activator.CreateInstance(indexType) as IIndex)!.Unique;
    }

    private static string GetTableNameFromGenericIndexType(Type indexType)
    {
        var entitytype = GetEntityType(indexType);
        var tableAttribute = entitytype!.GetCustomAttribute<TableAttribute>();

        return tableAttribute!.Name;
    }

    private static string GetAttributeNameFromIndexType(Type indexType)
    {
        var indexObject = Activator.CreateInstance(indexType);
        var indexDefinitionProperty = indexType.GetProperty(IIndex.IndexDefinitionName);
        if (indexDefinitionProperty?.GetValue(indexObject) is LambdaExpression expression)
        {
            return GetAttributeNameFromLambdaExpression(expression);
        }
        throw new UnsupportedIndexImplementationException($"The field {IIndex.IndexDefinitionName} on the type {indexType.Name} is not a LambdaExpression");
    }

    private static string GetAttributeNameFromLambdaExpression(LambdaExpression expression)
    {
        if (expression.Body is MemberExpression memberExpression && memberExpression.Member is PropertyInfo propertyInfo)
        {
            return propertyInfo.Name;
        }
        throw new UnsupportedIndexImplementationException($"The LambdaExpression {expression} does not represent accessing a property on the entity");
    }
}
