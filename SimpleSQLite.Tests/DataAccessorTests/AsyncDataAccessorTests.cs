using Kildetoft.SimpleSQLite.TestHelpers;
using SimpleSQLite.Samples.Entities;
using SimpleSQLite.Samples.Specifications;
using SimpleSQLite.Tests.TestHelpers;

namespace SimpleSQLite.Tests.DataAccessorTests;

[TestFixture]
internal class AsyncDataAccessorTests
{
    [Test]
    public async Task GetByIdAsync_EntityExists_GetsEntity()
    {
        // Arrange
        var entities = SampleEntityCreator.GetSampleEntities();
        var smallestSpec = new Smallest();

        // Act
        SampleEntity? result;
        SampleEntity existingEntity;
        using (var dataAccessMock = new DataAccessMock<SampleEntity>(entities))
        {
            existingEntity = dataAccessMock.DataAccessor.Get(smallestSpec);
            result = await dataAccessMock.AsyncDataAccessor.GetByIdAsync<SampleEntity>(existingEntity.Id);
        }

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(existingEntity.Id));
        Assert.That(result.UniqueName, Is.EqualTo(existingEntity.UniqueName));
    }

    [Test]
    public async Task CreateAsync_CreatesEntityAndReturnsCreated()
    {
        // Arrange
        var entity = SampleEntityCreator.GetSampleEntities().First();

        // Act
        SampleEntity? createdEntity;
        SampleEntity newEntity;
        using (var dataAccessMock = new DataAccessMock<SampleEntity>())
        {
            newEntity = await dataAccessMock.AsyncDataAccessor.CreateAsync(entity);
            createdEntity = await dataAccessMock.AsyncDataAccessor.GetByIdAsync<SampleEntity>(newEntity.Id);
        }

        // Assert
        Assert.That(createdEntity, Is.Not.Null);
        Assert.That(createdEntity.Id, Is.EqualTo(newEntity.Id));
        Assert.That(createdEntity.UniqueName, Is.EqualTo(newEntity.UniqueName));
    }

    [Test]
    public async Task UpdateAsync_UpdatesValue()
    {
        // Arrange
        var entity = SampleEntityCreator.GetSampleEntities().First();
        var originalName = entity.Name;

        // Act
        SampleEntity? updatedEntity;
        SampleEntity originalEntity;
        using (var dataAccessMock = new DataAccessMock<SampleEntity>())
        {
            originalEntity = await dataAccessMock.AsyncDataAccessor.CreateAsync(entity);
            originalEntity.Name = "NewName";
            await dataAccessMock.AsyncDataAccessor.UpdateAsync(originalEntity);
            updatedEntity = await dataAccessMock.AsyncDataAccessor.GetByIdAsync<SampleEntity>(originalEntity.Id);
        }

        // Assert
        Assert.That(updatedEntity, Is.Not.Null);
        Assert.That(updatedEntity.Id, Is.EqualTo(originalEntity.Id));
        Assert.That(updatedEntity.Name, Is.EqualTo(originalEntity.Name));
        Assert.That(updatedEntity.Name, Is.Not.EqualTo(originalName));
    }

    [Test]
    public async Task DeleteAsync_RemovesEntity()
    {
        // Arrange
        var entity = SampleEntityCreator.GetSampleEntities().First();

        // Act
        SampleEntity? resultingEntity;
        SampleEntity originalEntity;
        using (var dataAccessMock = new DataAccessMock<SampleEntity>())
        {
            originalEntity = await dataAccessMock.AsyncDataAccessor.CreateAsync(entity);
            await dataAccessMock.AsyncDataAccessor.DeleteAsync<SampleEntity>(originalEntity.Id);
            resultingEntity = await dataAccessMock.AsyncDataAccessor.GetByIdAsync<SampleEntity>(originalEntity.Id);
        }

        // Assert
        Assert.That(resultingEntity, Is.Null);
    }
}
