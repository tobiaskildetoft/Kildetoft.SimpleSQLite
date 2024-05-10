# Kildetoft.SimpleSQLite

This package provides a wrapper for an async SQLite connection, which exposes a minimal interface for erforming CRUD operations, plus querying via specifications.

## Setup
Create the classes that represent the desired datamodels, and have them implement the exposed IEntity interface.
These classes must have the Table attribure from sqlite-net, as well as the PrimaryKey attribute on the Id field.

For each index desired, create a generic implementation of the IIndex<T> interface, where T is the IEntity representing the table for the index. Add the necessary choice of whether the index should be unique, as well as a LambdaExpression as the IndexDefinition that defines which property the index should be created on.

The main interface for interacting with the data will be the IDataAccessor. This is registered in an IServiceCollection by using the supplied extension method AddSimpleSQLite, providing it with the connection string for the database file to be used.
This will return an IConnectionRegistration, on which it is possible to add tables and indexes. The simplest way is via the methods AddTablesFromAssemblyContaining<T>, AddIndexesFromAssemblyContaining<T>, and AddAllFromAssemblyContaining<T>. These will find all classes in the assemblies containing the specified type, and add either tables, indexes or both, to the database based on what it finds.

## CRUD operations
The IDataAccessor interface exposes the basic CRUD operations, via the Create, GetById, Update, and Delete methods.

## Querying data
IDataAccessor also exposes the Get method, which is used for querying the data. It takes an ISpecification, which determines what it returns.

The return type is based on whether the ISpecification is:
- an IAllSpecification (return all results), 
- an IFirstSpecification (return first result, throwing an exception if no results are found), 
- or an IFirstOrDefaultSpecification (returning the first result if it exists, and null otherwise)

The specifications can also be used to define:
- which data to return, using the IWhereSpecification
- How to sort the data, using the IOrderSpecification
- Whether to skip some of the data, using the ISkipSpecification
- How many items to take, using the ITakeSpecification (which extends the IAllSpecification)

## Samples and testing
See the SimpleSQLite.SampleClasses project for examples of creating Entities and Specifications.
See the SimpleSQLite.Tests project for examples of how to test Specifications using the provided DataAccessMock

