using Kildetoft.SimpleSQLite.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleSQLite.Tests.IntegrationTests;

[TestFixture]
internal abstract class BaseIntegrationTests
{
    protected IServiceCollection _serviceCollection;
    protected const string _validConnectionString = "test.db";

    [SetUp]
    public void Setup()
    {
        _serviceCollection = new ServiceCollection();
    }

    [TearDown]
    public void TearDown()
    {
        _serviceCollection.RemoveSimpleSQLite();
        File.Delete(_validConnectionString);
    }
}
