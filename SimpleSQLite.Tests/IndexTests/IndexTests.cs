using Kildetoft.SimpleSQLite.TestHelpers;
using Kildetoft.SimpleSQLite;
using SimpleSQLite.Samples.Entities;
using SimpleSQLite.Samples.Indexes;
using static SQLite.SQLite3;
using SQLite;

namespace SimpleSQLite.Tests.IndexTests;

[TestFixture]
internal class IndexTests
{
    [Test]
    public void UniqueNameIndex_PreventsCreationOfDuplicateValues()
    {
        // Arrange
        var index = new UniqueNameIndex();
        var entity = new SampleEntity { Id = 1, Name = "Name", UniqueName = "UniqueName" };
        IEnumerable<SampleEntity> initialEntities = [entity];

        // Act
        // Assert
        using (var mockAccess = new DataAccessMock<SampleEntity>(initialEntities, [index]))
        {
            Assert.Throws<SQLiteException>(() => mockAccess.DataAccessor.Create(entity));
        }
    }

    [Test]
    public void NameIndex_DoesNotPreventsCreationOfDuplicateValues()
    {
        // Arrange
        var index = new NameIndex();
        var entity = new SampleEntity { Id = 1, Name = "Name", UniqueName = "UniqueName" };
        IEnumerable<SampleEntity> initialEntities = [entity];

        // Act
        // Assert
        using (var mockAccess = new DataAccessMock<SampleEntity>(initialEntities, [index]))
        {
            Assert.DoesNotThrow(() => mockAccess.DataAccessor.Create(entity));
        }
    }
}
