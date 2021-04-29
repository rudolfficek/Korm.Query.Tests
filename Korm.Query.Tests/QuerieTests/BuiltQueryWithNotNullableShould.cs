using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Korm.Query.Tests.QuerieTests
{
    [Collection("Korm")]
    public class BuiltQueryWithNotNullableShould
    {
        private readonly DatabaseFixture _fixture;
        public BuiltQueryWithNotNullableShould(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        public IEnumerable<T> GetNotNullableByQuery<T>()
            => _fixture.Db.Query<T>()
                .Select("Id", "Name")
                .From("NotNullables")
                .ToList();

        #region Tests

        [Fact]
        public void HaveNotNullableCollection_ByRecord()
        {
            var result = GetNotNullableByQuery<Models.NotNullableRecord>();

            Assert.True(result.Any());
        }

        [Fact]
        public void HaveNotNullableCollection_ByClass()
        {
            var result = GetNotNullableByQuery<Models.NotNullableClass>();

            Assert.True(result.Any());
        }

        [Fact]
        public void HaveNotNullableCollection_ByRecordWithAlias()
        {
            var result = GetNotNullableByQuery<Models.NotNullableRecordAlias>();

            Assert.True(result.Any());
        }

        [Fact]
        public void HaveNotNullableCollection_ByClassWithAlias()
        {
            var result = GetNotNullableByQuery<Models.NotNullableClassAlias>();

            Assert.True(result.Any());
        }

        #endregion
    }
}
