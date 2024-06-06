using Kildetoft.SimpleSQLite;
using Kildetoft.SimpleSQLite.Exceptions;
using Kildetoft.SimpleSQLite.IoC;

namespace SimpleSQLite.Tests.IntegrationTests;

[TestFixture]
internal class AddSimpleSQLiteTests : BaseIntegrationTests
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
    public void AddSimpleSQLite_InvalidConnectionString_ThrowsInvalidConnectionStringException()
    {
        // Arrange
        var invalidConnectionString = Path.Combine(Guid.NewGuid().ToString(), _validConnectionString);

        // Act
        // Assert
        Assert.Throws<InvalidConnectionStringException>(() => _serviceCollection.AddSimpleSQLite(invalidConnectionString));
    }
}
