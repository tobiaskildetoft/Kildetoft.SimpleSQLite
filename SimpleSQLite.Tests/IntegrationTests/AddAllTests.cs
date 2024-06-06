using Kildetoft.SimpleSQLite.IoC;
using SimpleSQLite.Samples.Entities;

namespace SimpleSQLite.Tests.IntegrationTests;

[TestFixture]
internal class AddAllTests : BaseIntegrationTests
{
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
}
