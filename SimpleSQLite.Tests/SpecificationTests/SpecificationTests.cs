using Kildetoft.SimpleSQLite.TestHelpers;
using SimpleSQLite.Samples.Entities;
using SimpleSQLite.Samples.Specifications;
using SimpleSQLite.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSQLite.Tests.SpecificationTests
{
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
            SampleEntity? result = null;
            using (var mockAccess = new DataAccessMock<SampleEntity>(entities))
            {
                result = await mockAccess.DataAccessor.Get(specification);
            }

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(nameOfSoughtEntity));
        }

        [Test]
        public async Task ByUniqueName_ReturnsNullIfNotExist()
        {
            // Arrange
            var entities = SampleEntityCreator.GetSampleEntities().ToList();
            var uniqueNameSought = Guid.NewGuid().ToString();

            var specification = new ByUniqueName(uniqueNameSought);

            // Act
            SampleEntity? result = null;
            using (var mockAccess = new DataAccessMock<SampleEntity>(entities))
            {
                result = await mockAccess.DataAccessor.Get(specification);
            }

            // Assert
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
            IEnumerable<SampleEntity>? result = null;
            using (var mockAccess = new DataAccessMock<SampleEntity>(entities))
            {
                result = await mockAccess.DataAccessor.Get(specification);
            }

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo(SecondLargestName));
            Assert.That(result.Skip(1).First().Name, Is.EqualTo(ThirdLargestName));
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
            IEnumerable<SampleEntity>? result = null;
            using (var mockAccess = new DataAccessMock<SampleEntity>(entities))
            {
                result = await mockAccess.DataAccessor.Get(specification);
            }

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo(SecondLargestName));
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
            IEnumerable<SampleEntity>? result = null;
            using (var mockAccess = new DataAccessMock<SampleEntity>(entities))
            {
                result = await mockAccess.DataAccessor.Get(specification);
            }

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }
    }
}
