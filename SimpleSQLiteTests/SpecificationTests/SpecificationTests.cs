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
    }
}
