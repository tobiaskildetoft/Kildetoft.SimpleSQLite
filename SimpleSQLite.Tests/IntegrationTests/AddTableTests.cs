using Kildetoft.SimpleSQLite.Exceptions;
using SimpleSQLite.Samples.Entities;
using SimpleSQLite.Tests.TestHelpers.InvalidEntities;
using Kildetoft.SimpleSQLite.IoC;

namespace SimpleSQLite.Tests.IntegrationTests;

[TestFixture]
internal class AddTableTests : BaseIntegrationTests
{
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
