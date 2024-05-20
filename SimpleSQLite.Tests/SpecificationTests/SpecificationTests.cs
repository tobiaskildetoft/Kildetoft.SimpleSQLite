using Kildetoft.SimpleSQLite.TestHelpers;
using SimpleSQLite.Samples.Entities;
using SimpleSQLite.Samples.Specifications;
using SimpleSQLite.Tests.TestHelpers;

namespace SimpleSQLite.Tests.SpecificationTests;

[TestFixture]
internal class SpecificationTests
{
    [Test]
    public async Task ByUniqueName_ReturnsObjectIfExists()
    {
        // Arrange
        var entities = SampleEntityCreator.GetSampleEntities().ToList();
        var uniqueNameSought = Guid.NewGuid().ToString();
        var nameOfSoughtEntity = Guid.NewGuid().ToString();
        entities.Add(new SampleEntity { UniqueName = uniqueNameSought, Name = nameOfSoughtEntity });

        var specification = new ByUniqueName(uniqueNameSought);

        // Act
        SampleEntity? resultAsync = null;
        SampleEntity? result = null;
        using (var mockAccess = new DataAccessMock<SampleEntity>(entities))
        {
            resultAsync = await mockAccess.AsyncDataAccessor.GetAsync(specification);
            result = mockAccess.DataAccessor.Get(specification);
        }

        // Assert
        Assert.That(resultAsync, Is.Not.Null);
        Assert.That(resultAsync.Name, Is.EqualTo(nameOfSoughtEntity));
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(resultAsync.Name));
    }

    [Test]
    public async Task ByUniqueName_ReturnsNullIfNotExist()
    {
        // Arrange
        var entities = SampleEntityCreator.GetSampleEntities().ToList();
        var uniqueNameSought = Guid.NewGuid().ToString();

        var specification = new ByUniqueName(uniqueNameSought);

        // Act
        SampleEntity? resultAsync = null;
        SampleEntity? result = null;
        using (var mockAccess = new DataAccessMock<SampleEntity>(entities))
        {
            resultAsync = await mockAccess.AsyncDataAccessor.GetAsync(specification);
            result = mockAccess.DataAccessor?.Get(specification);
        }

        // Assert
        Assert.That(resultAsync, Is.Null);
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task SecondAndThirdLargest_ReturnsTwoIfTheyExist()
    {
        // Arrange
        var entities = SampleEntityCreator.GetSampleEntities().ToList();
        var maxValue = entities.Max(x => x.SomeInt);
        var ThirdLargestName = "ThirdLargest";
        var SecondLargestName = "SecondLargest";
        var LargestName = "Largest";
        entities.Add(new SampleEntity { SomeInt = maxValue+1, Name = ThirdLargestName });
        entities.Add(new SampleEntity { SomeInt = maxValue + 2, Name = SecondLargestName });
        entities.Add(new SampleEntity { SomeInt = maxValue + 3, Name = LargestName });

        var specification = new SecondAndThirdLargest();

        // Act
        IEnumerable<SampleEntity>? resultAsync = null;
        IEnumerable<SampleEntity>? result = null;
        using (var mockAccess = new DataAccessMock<SampleEntity>(entities))
        {
            resultAsync = await mockAccess.AsyncDataAccessor.GetAsync(specification);
            result = mockAccess.DataAccessor.Get(specification);
        }

        // Assert
        Assert.That(resultAsync, Is.Not.Null);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result.Count(), Is.EqualTo(result.Count()));
        Assert.That(result.First().Name, Is.EqualTo(SecondLargestName));
        Assert.That(result.First().Name, Is.EqualTo(result.First().Name));
        Assert.That(result.Skip(1).First().Name, Is.EqualTo(ThirdLargestName));        
        Assert.That(result.Skip(1).First().Name, Is.EqualTo(result.Skip(1).First().Name));
    }

    [Test]
    public async Task SecondAndThirdLargest_ReturnsSecondLargestIfOnlyTwo()
    {
        // Arrange
        var entities = new List<SampleEntity>();
        var SecondLargestName = "SecondLargest";
        var LargestName = "Largest";
        entities.Add(new SampleEntity { SomeInt = 2, Name = SecondLargestName });
        entities.Add(new SampleEntity { SomeInt = 3, Name = LargestName });

        var specification = new SecondAndThirdLargest();

        // Act
        IEnumerable<SampleEntity>? resultAsync = null;
        IEnumerable<SampleEntity>? result = null;
        using (var mockAccess = new DataAccessMock<SampleEntity>(entities))
        {
            resultAsync = await mockAccess.AsyncDataAccessor.GetAsync(specification);
            result = mockAccess.DataAccessor.Get(specification);
        }

        // Assert
        Assert.That(resultAsync, Is.Not.Null);
        Assert.That(result, Is.Not.Null);
        Assert.That(resultAsync.Count(), Is.EqualTo(1));
        Assert.That(result.Count(), Is.EqualTo(resultAsync.Count()));
        Assert.That(resultAsync.First().Name, Is.EqualTo(SecondLargestName));
        Assert.That(result.First().Name, Is.EqualTo(resultAsync.First().Name));
    }

    [Test]
    public async Task SecondAndThirdLargest_ReturnsEmptyIfOnlyOne()
    {
        // Arrange
        var entities = new List<SampleEntity>();
        var LargestName = "Largest";
        entities.Add(new SampleEntity { SomeInt = 3, Name = LargestName });

        var specification = new SecondAndThirdLargest();

        // Act
        IEnumerable<SampleEntity>? resultAsync = null;
        IEnumerable<SampleEntity>? result = null;
        using (var mockAccess = new DataAccessMock<SampleEntity>(entities))
        {
            resultAsync = await mockAccess.AsyncDataAccessor.GetAsync(specification);
            result = mockAccess.DataAccessor.Get(specification);
        }

        // Assert
        Assert.That(resultAsync, Is.Not.Null);
        Assert.That(result, Is.Not.Null);
        Assert.That(resultAsync.Count(), Is.EqualTo(0));
        Assert.That(result.Count(), Is.EqualTo(resultAsync.Count()));
    }
}
