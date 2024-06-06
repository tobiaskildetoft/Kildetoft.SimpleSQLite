using Kildetoft.SimpleSQLite;
using Kildetoft.SimpleSQLite.TestHelpers;
using SimpleSQLite.Samples.Entities;
using SimpleSQLite.Samples.Indexes;
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
        using (var mockAccess = new DataAccessMock<SampleEntity>(entities, GetIndexes()))
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
        // var entities = SampleEntityCreator.GetSampleEntities().ToList();
        var uniqueNameSought = Guid.NewGuid().ToString();

        var specification = new ByUniqueName(uniqueNameSought);
        // Act
        // SampleEntity? resultAsync = null;
        SampleEntity? result = null;
        using (var mockAccess = new DataAccessMock<SampleEntity>(null, GetIndexes()))
        {
            // resultAsync = await mockAccess.AsyncDataAccessor.GetAsync(specification);
            result = mockAccess.DataAccessor?.Get(specification);
        }

        // Assert
        // Assert.That(resultAsync, Is.Null);
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
        entities.Add(new SampleEntity { SomeInt = maxValue+1, Name = ThirdLargestName, UniqueName = "ThirdLargestUniqueName" });
        entities.Add(new SampleEntity { SomeInt = maxValue + 2, Name = SecondLargestName, UniqueName = "SecondLargestUniqueName" });
        entities.Add(new SampleEntity { SomeInt = maxValue + 3, Name = LargestName, UniqueName = "LargestUniqueName" });

        var specification = new SecondAndThirdLargest();

        // Act
        IEnumerable<SampleEntity>? resultAsync = null;
        IEnumerable<SampleEntity>? result = null;
        using (var mockAccess = new DataAccessMock<SampleEntity>(entities, GetIndexes()))
        {
            resultAsync = await mockAccess.AsyncDataAccessor.GetAsync(specification);
            result = mockAccess.DataAccessor.Get(specification);
        }

        // Assert
        Assert.That(resultAsync, Is.Not.Null);
        Assert.That(result, Is.Not.Null);
        Assert.That(resultAsync.Count(), Is.EqualTo(2));
        Assert.That(result.Count(), Is.EqualTo(resultAsync.Count()));
        Assert.That(resultAsync.First().Name, Is.EqualTo(SecondLargestName));
        Assert.That(result.First().Name, Is.EqualTo(resultAsync.First().Name));
        Assert.That(resultAsync.Skip(1).First().Name, Is.EqualTo(ThirdLargestName));        
        Assert.That(result.Skip(1).First().Name, Is.EqualTo(resultAsync.Skip(1).First().Name));
    }

    [Test]
    public async Task SecondAndThirdLargest_ReturnsSecondLargestIfOnlyTwo()
    {
        // Arrange
        var entities = new List<SampleEntity>();
        var SecondLargestName = "SecondLargest";
        var LargestName = "Largest";
        entities.Add(new SampleEntity { SomeInt = 2, Name = SecondLargestName, UniqueName = "SecondLargestUniqueName" });
        entities.Add(new SampleEntity { SomeInt = 3, Name = LargestName, UniqueName = "LargestUniqueName" });

        var specification = new SecondAndThirdLargest();

        // Act
        IEnumerable<SampleEntity>? resultAsync = null;
        IEnumerable<SampleEntity>? result = null;
        using (var mockAccess = new DataAccessMock<SampleEntity>(entities, GetIndexes()))
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
        using (var mockAccess = new DataAccessMock<SampleEntity>(entities, GetIndexes()))
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

    [Test]
    public async Task Smallest_ReturnsSmallestIfNonEmpty()
    {
        // Arrange
        var entities = SampleEntityCreator.GetSampleEntities().ToList();
        var minValue = entities.Min(x => x.SomeInt);
        var smallestName = "Smallest";
        entities.Add(new SampleEntity { SomeInt = minValue - 1, Name = smallestName, UniqueName = "SmallestUniqueName" });

        var specification = new Smallest();

        // Act
        SampleEntity? resultAsync = null;
        SampleEntity? result = null;
        using (var mockAccess = new DataAccessMock<SampleEntity>(entities, GetIndexes()))
        {
            resultAsync = await mockAccess.AsyncDataAccessor.GetAsync(specification);
            result = mockAccess.DataAccessor.Get(specification);
        }

        // Assert
        Assert.That(resultAsync, Is.Not.Null);
        Assert.That(result, Is.Not.Null);
        Assert.That(resultAsync.Name, Is.EqualTo(smallestName));
        Assert.That(result.Name, Is.EqualTo(resultAsync.Name));
    }

    [Test]
    public void Smallest_ThrowsIfEmpty()
    {
        // Arrange
        var entities = new List<SampleEntity>();

        var specification = new Smallest();

        // Act
        // Assert
        using (var mockAccess = new DataAccessMock<SampleEntity>(entities, GetIndexes()))
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await mockAccess.AsyncDataAccessor.GetAsync(specification));
            Assert.Throws<InvalidOperationException>(() => mockAccess.DataAccessor.Get(specification));
        }
    }

    private IEnumerable<IIndex<SampleEntity>> GetIndexes()
    {
        return new List<IIndex<SampleEntity>>
        {
            new NameIndex(),
            new UniqueNameIndex(),
        };
    }
}
