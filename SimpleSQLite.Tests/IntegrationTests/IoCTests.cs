using Kildetoft.SimpleSQLite;
using Kildetoft.SimpleSQLite.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleSQLite.Tests.IntegrationTests;

[TestFixture]
internal class IoCTests
{
    private IServiceCollection _serviceCollection;
    private const string _validConnectionString = "test.db";

    [SetUp]
    public void Setup()
    {
        _serviceCollection = new ServiceCollection();
    }

    [TearDown]
    public void TearDown()
    {
        _serviceCollection.RemoveSimpleSQLite(true);
    }

    [Test]
    public void AddSimpleSQLite_ValidConnectionString_RegistersIDataAccessor()
    {
        _serviceCollection.AddSimpleSQLite(_validConnectionString);
        Assert.That(_serviceCollection.Any(x => x.ServiceType == typeof(IDataAccessor)));
    }

    [Test]
    public void AddSimpleSQLite_ValidConnectionString_RegistersIAsyncDataAccessor()
    {
        _serviceCollection.AddSimpleSQLite(_validConnectionString);
        Assert.That(_serviceCollection.Any(x => x.ServiceType == typeof(IAsyncDataAccessor)));
    }

    [Test]
    public void AddSimpleSQLite_ValidConnectionString_ReturnsConnectionRegistrationWithOriginalServiceCollection()
    {
        var connectionRegistration = _serviceCollection.AddSimpleSQLite(_validConnectionString);
        Assert.That(connectionRegistration.ServiceCollection, Is.EqualTo(_serviceCollection));
    }

    [Test]
    public void RemoveSimpleSQLite_NoDatabaseDeletion_UndoesRegistrationOfIDataAccessor()
    {
        _serviceCollection.AddSimpleSQLite(_validConnectionString);
        _serviceCollection.RemoveSimpleSQLite();
        Assert.That(!_serviceCollection.Any(x => x.ServiceType == typeof(IDataAccessor)));
        Assert.That(File.Exists(_validConnectionString));
    }

    [Test]
    public void RemoveSimpleSQLite_NoDatabaseDeletion_UndoesRegistrationOfIAsyncDataAccessor()
    {
        _serviceCollection.AddSimpleSQLite(_validConnectionString);
        _serviceCollection.RemoveSimpleSQLite();
        Assert.That(!_serviceCollection.Any(x => x.ServiceType == typeof(IAsyncDataAccessor)));
        Assert.That(File.Exists(_validConnectionString));
    }

    [Test]
    public void RemoveSimpleSQLite_DatabaseDeletion_UndoesRegistrationOfIDataAccessorAndDeletesFile()
    {
        _serviceCollection.AddSimpleSQLite(_validConnectionString);
        _serviceCollection.RemoveSimpleSQLite(true);
        Assert.That(!_serviceCollection.Any(x => x.ServiceType == typeof(IDataAccessor)));
        Assert.That(!File.Exists(_validConnectionString));
    }

    [Test]
    public void RemoveSimpleSQLite_DatabaseDeletion_UndoesRegistrationOfIAsyncDataAccessorAndDeletesFile()
    {
        _serviceCollection.AddSimpleSQLite(_validConnectionString);
        _serviceCollection.RemoveSimpleSQLite(true);
        Assert.That(!_serviceCollection.Any(x => x.ServiceType == typeof(IAsyncDataAccessor)));
        Assert.That(!File.Exists(_validConnectionString));
    }
}
