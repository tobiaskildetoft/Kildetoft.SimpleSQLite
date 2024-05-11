namespace Kildetoft.SimpleSQLite.IoC;

public interface IConnectionRegistration
{
    // TODO: Add custom exceptions and reference these in summaries

    /// <summary>
    /// Add all tables defined by implementations of IEntity in the assembly containing the generic type
    /// If any implementations of IEntity are not usable for table definitions, an exception will be thrown
    /// Returns self for chaining
    /// </summary>
    /// <exception cref="UnsupportedEntityImplementationException">If any IEntity implementation is not usable</exception>
    IConnectionRegistration AddTablesFromAssemblyContaining<T>();

    /// <summary>
    /// Add all indexes defined by implementations of the generic version of IIndex in the assembly containing the generic type
    /// If any implementations of IIndex are not usable for index definitions, an exception will be thrown
    /// Returns self for chaining
    /// </summary>
    /// <exception cref="UnsupportedIndexImplementationException">If any IIndex implementation is not usable</exception>
    IConnectionRegistration AddIndexesFromAssemblyContaining<T>();

    /// <summary>
    /// Add all indexes and tables defined by implementations of IEntity and the generic version of IIndex in the assembly containing the generic type
    /// If any implementations are not usable, an exception will be thrown
    /// Returns self for chaining
    /// </summary>
    /// <exception cref="UnsupportedEntityImplementationException">If any IEntity implementation is not usable</exception>
    /// <exception cref="UnsupportedIndexImplementationException">If any IIndex implementation is not usable</exception>
    IConnectionRegistration AddAllFromAssemblyContaining<T>();


    /// <summary>
    /// Add a table defined by the generic type implementing IEntity
    /// If the type is not usable for defining a table, an exception will be thrown
    /// Returns self for chaining
    /// </summary>
    /// <exception cref="UnsupportedEntityImplementationException">If the IEntity implementation is not usable</exception>
    IConnectionRegistration AddTable<T>() where T : IEntity, new();

    /// <summary>
    /// Add a table defined by the specified type implementing IEntity
    /// If the type is not usable for defining a table, an exception will be thrown
    /// Returns self for chaining
    /// </summary>
    /// <exception cref="UnsupportedEntityImplementationException">If the IEntity implementation is not usable</exception>
    IConnectionRegistration AddTable(Type entityType);

    /// <summary>
    /// Add tables defined by the types specified implementing IEntity
    /// If any type is not usable for defining a table, an exception will be thrown
    /// Returns self for chaining
    /// </summary>
    /// <exception cref="UnsupportedEntityImplementationException">If any IEntity implementation is not usable</exception>
    IConnectionRegistration AddTables(IEnumerable<Type> entityTypes);


    /// <summary>
    /// Add an index defined by the generic type implementing the generic version of IIndex
    /// If the type is not usable for defining an index, an exception will be thrown
    /// Returns self for chaining
    /// </summary>
    /// <exception cref="UnsupportedIndexImplementationException">If the IIndex implementation is not usable</exception>
    IConnectionRegistration AddIndex<T>() where T : IIndex, new();

    /// <summary>
    /// Add an index defined by the specified type implementing the generic version of IIndex
    /// If the type is not usable for defining an index, an exception will be thrown
    /// Returns self for chaining
    /// </summary>
    /// <exception cref="UnsupportedIndexImplementationException">If the IIndex implementation is not usable</exception>
    IConnectionRegistration AddIndex(Type indexType);

    /// <summary>
    /// Add indexes defined by the specified types implementing the generic version of IIndex
    /// If any type is not usable for defining an index, an exception will be thrown
    /// Returns self for chaining
    /// </summary>
    /// <exception cref="UnsupportedIndexImplementationException">If any IIndex implementation is not usable</exception>
    IConnectionRegistration AddIndexes(IEnumerable<Type> indexTypes);
}
