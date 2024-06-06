using Kildetoft.SimpleSQLite;
using Kildetoft.SimpleSQLite.IoC;

namespace SimpleSQLite.Tests.IntegrationTests;

[TestFixture]
internal class RemoveSimpleSQLiteTests : BaseIntegrationTests
{
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
}
