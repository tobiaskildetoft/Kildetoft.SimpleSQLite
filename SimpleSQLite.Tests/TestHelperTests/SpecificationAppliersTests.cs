using Kildetoft.SimpleSQLite.TestHelpers;
using SimpleSQLite.Samples.Entities;
using SimpleSQLite.Samples.Specifications;
using SimpleSQLite.Tests.TestHelpers;

namespace SimpleSQLite.Tests.TestHelperTests;

[TestFixture]
internal class SpecificationAppliersTests
{
    [Test]
    public void ApplyAllSpecification_ReturnsSameAsDataAccessor()
    {
        // Arrange
        var specification = new SecondAndThirdLargest();
        var entities = SampleEntityCreator.GetSampleEntities();
        IEnumerable<string> expectedResult;
        using (var dataAccessMock = new DataAccessMock<SampleEntity>(entities))
        {
            expectedResult = dataAccessMock.DataAccessor.Get(specification).Select(x => x.UniqueName);
        }

        // Act
        var actualResult = entities.ApplySpecification(specification).Select(x => x.UniqueName);

        // Assert
        Assert.That(actualResult, Is.EquivalentTo(expectedResult));
    }

    [Test]
    [TestCase("UniqueName1")]
    [TestCase("UniqueName123")]
    [TestCase("DoesNotExist")]
    public void ApplyFirstOrDefaultSpecification_ReturnsSameAsDataAccessor(string uniqueNameSought)
    {
        // Arrange
        var specification = new ByUniqueName(uniqueNameSought);
        var entities = SampleEntityCreator.GetSampleEntities();
        string? expectedResult;
        using (var dataAccessMock = new DataAccessMock<SampleEntity>(entities))
        {
            expectedResult = dataAccessMock.DataAccessor.Get(specification)?.UniqueName;
        }

        // Act
        var actualResult = entities.ApplySpecification(specification)?.UniqueName;

        // Assert
        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }

    [Test]
    public void ApplyFirstSpecification_ReturnsSameAsDataAccessor()
    {
        // Arrange
        var specification = new Smallest();
        var entities = SampleEntityCreator.GetSampleEntities();
        string expectedResult;
        using (var dataAccessMock = new DataAccessMock<SampleEntity>(entities))
        {
            expectedResult = dataAccessMock.DataAccessor.Get(specification).UniqueName;
        }

        // Act
        var actualResult = entities.ApplySpecification(specification).UniqueName;

        // Assert
        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
}
