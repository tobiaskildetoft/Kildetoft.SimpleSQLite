using Kildetoft.SimpleSQLite;
using Kildetoft.SimpleSQLite.Exceptions;
using Kildetoft.SimpleSQLite.IoC;
using Microsoft.Extensions.DependencyInjection;
using SimpleSQLite.Samples.Entities;
using SimpleSQLite.Samples.Indexes;
using SimpleSQLite.Tests.TestHelpers.InvalidEntities;
using SimpleSQLite.Tests.TestHelpers.InvalidIndexes;
using System.Data;
using System.IO;

namespace SimpleSQLite.Tests.IntegrationTests;

[TestFixture]
internal class IoCTests : BaseIntegrationTests
{
    [Test]
    public void AddSimpleSQLite_ValidConnectionString_RegistersIDataAccessor()
    {
        // Act
        _serviceCollection.AddSimpleSQLite(_validConnectionString);

        // Assert
        Assert.That(_serviceCollection.Any(x => x.ServiceType == typeof(IDataAccessor)));
    }

    [Test]
    public void AddSimpleSQLite_ValidConnectionString_RegistersIAsyncDataAccessor()
    {
        // Act
        _serviceCollection.AddSimpleSQLite(_validConnectionString);

        // Assert
        Assert.That(_serviceCollection.Any(x => x.ServiceType == typeof(IAsyncDataAccessor)));
    }

    [Test]
    public void AddSimpleSQLite_ValidConnectionString_ReturnsConnectionRegistrationWithOriginalServiceCollection()
    {
        // Act
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);

        // Assert
        Assert.That(connectionRegistration.ServiceCollection, Is.EqualTo(_serviceCollection));
    }

    [Test]
    public void RemoveSimpleSQLite_UndoesRegistrationOfIDataAccessorAndAllowsDeletionOfFile()
    {
        // Arrange
        _serviceCollection.AddSimpleSQLite(_validConnectionString);

        // Act
        _serviceCollection.RemoveSimpleSQLite();

        // Assert
        Assert.That(!_serviceCollection.Any(x => x.ServiceType == typeof(IDataAccessor)));
        Assert.DoesNotThrow(() => File.Delete(_validConnectionString));
    }

    [Test]
    public void RemoveSimpleSQLite_UndoesRegistrationOfIAsyncDataAccessorAndAllowsDeletionOfFile()
    {
        // Arrange
        _serviceCollection.AddSimpleSQLite(_validConnectionString);

        // Act
        _serviceCollection.RemoveSimpleSQLite();

        // Assert
        Assert.That(!_serviceCollection.Any(x => x.ServiceType == typeof(IAsyncDataAccessor)));
        Assert.DoesNotThrow(() => File.Delete(_validConnectionString));
    }

    [Test]
    public void AddSimpleSQLite_InvalidConnectionString_ThrowsInvalidConnectionStringException()
    {
        // Arrange
        var invalidConnectionString = Path.Combine(Guid.NewGuid().ToString(), _validConnectionString);

        // Act
        // Assert
        Assert.Throws<InvalidConnectionStringException>(() => _serviceCollection.AddSimpleSQLite(invalidConnectionString));
    }

    [Test]
    public void AddTablesFromAssemblyContaining_AssemblyContainsSampleEntity_SampleEntityTableExists()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);

        // Act
        connectionRegistration.AddTablesFromAssemblyContaining<SampleEntity>();
        _serviceCollection.RemoveSimpleSQLite();
        var databaseFile = File.ReadAllText(_validConnectionString);

        // Assert
        Assert.That(databaseFile, Does.Contain("SampleEntity"));
    }

    [Test]
    public void AddTableGeneric_SampleEntity_SampleEntityTableExists()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);

        // Act
        connectionRegistration.AddTable<SampleEntity>();
        _serviceCollection.RemoveSimpleSQLite();
        var databaseFile = File.ReadAllText(_validConnectionString);

        // Assert
        Assert.That(databaseFile, Does.Contain("SampleEntity"));
    }

    [Test]
    public void AddTable_InvalidEntityNotIEntity_ThrowsUnsupportedEntityImplementationException()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);

        // Act
        // Assert
        Assert.Throws<UnsupportedEntityImplementationException>(() => connectionRegistration.AddTable(typeof(InvalidEntityNotIEntity)));
    }

    [Test]
    public void AddTable_InvalidEntityNoTableAttribute_ThrowsUnsupportedEntityImplementationException()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);

        // Act
        // Assert
        Assert.Throws<UnsupportedEntityImplementationException>(() => connectionRegistration.AddTable(typeof(InvalidEntityNoTableAttribute)));
    }

    [Test]
    public void AddTable_InvalidEntityIdNotPrimaryKey_ThrowsUnsupportedEntityImplementationException()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);

        // Act
        // Assert
        Assert.Throws<UnsupportedEntityImplementationException>(() => connectionRegistration.AddTable(typeof(InvalidEntityIdNotPrimaryKey)));
    }

    [Test]
    public void AddTable_InvalidEntityNoDefaultConstructor_ThrowsUnsupportedEntityImplementationException()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);

        // Act
        // Assert
        Assert.Throws<UnsupportedEntityImplementationException>(() => connectionRegistration.AddTable(typeof(InvalidEntityNoDefaultConstructor)));
    }

    [Test]
    public void AddIndexesFromAssemblyContaining_AssemblyContainingUniqueNameIndexAndNameIndex_IndexesExistsInTable()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);
        connectionRegistration.AddTablesFromAssemblyContaining<SampleEntity>();

        // Act
        connectionRegistration.AddIndexesFromAssemblyContaining<UniqueNameIndex>();
        _serviceCollection.RemoveSimpleSQLite();
        var databaseFile = File.ReadAllText(_validConnectionString);

        // Assert
        Assert.That(databaseFile, Does.Contain("SampleEntity_UniqueName"));
        Assert.That(databaseFile, Does.Contain("SampleEntity_Name"));
    }

    [Test]
    public void AddIndexesGeneric_UniqueNameIndex_IndexesExistsInTable()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);
        connectionRegistration.AddTablesFromAssemblyContaining<SampleEntity>();

        // Act
        connectionRegistration.AddIndex<UniqueNameIndex>();
        _serviceCollection.RemoveSimpleSQLite();
        var databaseFile = File.ReadAllText(_validConnectionString);

        // Assert
        Assert.That(databaseFile, Does.Contain("SampleEntity_UniqueName"));
    }

    [Test]
    public void AddAllFromAssemblyContaining_AssemblyContainingVarious_AllGetAdded()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);
        connectionRegistration.AddAllFromAssemblyContaining<SampleEntity>();

        // Act
        _serviceCollection.RemoveSimpleSQLite();
        var databaseFile = File.ReadAllText(_validConnectionString);

        // Assert
        Assert.That(databaseFile, Does.Contain("SampleEntity"));
        Assert.That(databaseFile, Does.Contain("SampleEntity_UniqueName"));
        Assert.That(databaseFile, Does.Contain("SampleEntity_Name"));
    }

    [Test]
    public void AddIndex_InvalidIndex_ThrowsUnsupportedIndexImplementationException()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);
        connectionRegistration.AddTablesFromAssemblyContaining<SampleEntity>();

        // Act
        // Assert
        Assert.Throws<UnsupportedIndexImplementationException>(() => connectionRegistration.AddIndex(typeof(SampleEntity)));
    }

    [Test]
    public void AddIndex_InvalidIndexExpressionNotLambdaExpression_ThrowsUnsupportedIndexImplementationException()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);
        connectionRegistration.AddTablesFromAssemblyContaining<SampleEntity>();

        // Act
        // Assert
        Assert.Throws<UnsupportedIndexImplementationException>(() => connectionRegistration.AddIndex(typeof(InvalidIndexNotLambda)));
    }

    [Test]
    public void AddIndex_InvalidIndexExpressionNotMemberExpression_ThrowsUnsupportedIndexImplementationException()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);
        connectionRegistration.AddTablesFromAssemblyContaining<SampleEntity>();

        // Act
        // Assert
        Assert.Throws<UnsupportedIndexImplementationException>(() => connectionRegistration.AddIndex(typeof(InvalidIndexNotMemberExpression)));
    }

    [Test]
    public void AddIndex_AfterRemovingSimpleSQLite_ThrowsInvalidIndexException()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);
        _serviceCollection.RemoveSimpleSQLite();

        // Act
        // Assert
        Assert.Throws<InvalidOperationException>(() => connectionRegistration.AddIndex(typeof(NameIndex)));
    }

    [Test]
    public void AddTables_AfterRemovingSimplqSQLite_ThrowsInvalidOperationException()
    {
        // Arrange
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);
        _serviceCollection.RemoveSimpleSQLite();

        // Act
        // Assert
        Assert.Throws<InvalidOperationException>(() => connectionRegistration.AddTable(typeof(SampleEntity)));
    }
}