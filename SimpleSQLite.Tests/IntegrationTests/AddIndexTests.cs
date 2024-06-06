namespace SimpleSQLite.Tests.IntegrationTests;

using Kildetoft.SimpleSQLite.Exceptions;
using Kildetoft.SimpleSQLite.IoC;
using SimpleSQLite.Samples.Entities;
using SimpleSQLite.Samples.Indexes;
using SimpleSQLite.Tests.TestHelpers.InvalidIndexes;

[TestFixture]
internal class AddIndexTests : BaseIntegrationTests
{
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
}
