using Kildetoft.SimpleSQLite.TestHelpers;
using SimpleSQLite.Samples.Entities;

namespace SimpleSQLite.Tests.TestHelperTests;

[TestFixture]
internal class DataAccessMockTests
{
    [Test]
    public void AfterUsing_DeletesCreatedFile()
    {
        // Arrange
        var currentDirectory = Directory.GetCurrentDirectory();
        var filesBefore = Directory.GetFiles(currentDirectory);

        // Act
        using (var dataAccessMock = new DataAccessMock<SampleEntity>())
        {
        }
        var filesAfter = Directory.GetFiles(currentDirectory);

        // Assert
        Assert.That(filesAfter, Is.EquivalentTo(filesBefore));
    }
}
