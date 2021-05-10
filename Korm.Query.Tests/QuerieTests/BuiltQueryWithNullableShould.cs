using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Korm.Query.Tests.QuerieTests
{
    [Collection("Korm")]
    public class BuiltQueryWithNullableShould
    {
        private readonly DatabaseFixture _fixture;
        private readonly ITestOutputHelper _output;
        public BuiltQueryWithNullableShould(DatabaseFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        public IEnumerable<T> GetFromNullablesButWithNotNullOnlyByQuery<T>()
            => _fixture.Db.Query<T>()
                .Select("Id", "Number", "NullableIdCol", "NullableStringCol")
                .From("Nullables")
                .Where("NullableIdCol IS NOT NULL")
                .ToList();

        public IEnumerable<T> GetAllFromNullablesByQuery<T>()
            => _fixture.Db.Query<T>()
                .Select("Id", "Number", "NullableIdCol", "NullableStringCol")
                .From("Nullables")
                .ToList();

        #region Tests

        #region Select where NullableIdCol is not null

        [Fact]
        public void HaveData_MappedAsRecord()
        {
            var result = GetFromNullablesButWithNotNullOnlyByQuery<Models.NullableRecord>();

            Assert.True(result.Any());
        }

        [Fact]
        public void HaveNullableColumnMapped_MappedAsRecord()
        {
            var result = GetFromNullablesButWithNotNullOnlyByQuery<Models.NullableRecord>().First();

            result.NullableIdCol.Should().NotBeNull();

            _output.WriteLine(result.ToString());
        }

        [Fact]
        public void HaveData_MappedAsClass()
        {
            var result = GetFromNullablesButWithNotNullOnlyByQuery<Models.NullableClass>();

            Assert.True(result.Any());
        }

        [Fact]
        public void HaveNullableColumnMapped_MappedAsClass()
        {
            var result = GetFromNullablesButWithNotNullOnlyByQuery<Models.NullableClass>().First();

            result.NullableIdCol.Should().NotBeNull();

            _output.WriteLine(result.NullableIdCol.ToString());
        }

        #endregion

        #region Select all

        [Fact]
        public void HaveDataWithNullColumn_MappedAsRecord()
        {
            var result = GetAllFromNullablesByQuery<Models.NullableRecord>();

            Assert.True(result.Any());
        }

        // here should be also not nullable data mapped in nullable record.
        [Fact]
        public void HaveDataWithNullColumn_MappedAsRecordMutable()
        {
            var result = GetAllFromNullablesByQuery<Models.NullableRecordMutable>();

            Assert.True(result.Any());
        }

        [Fact]
        public void HaveDataWithNullColumnCorrectlyMapped_MappedAsRecord()
        {
            var result = GetAllFromNullablesByQuery<Models.NullableRecord>();

            Assert.Contains(result, a => a.NullableIdCol.HasValue);
        }

        [Fact]
        public void HaveDataWithNullColumnCorrectlyMapped_MappedAsRecordWithAlias()
        {
            var result = GetAllFromNullablesByQuery<Models.NullableRecordAlias>();

            Assert.Contains(result, a => a.NullableIdCol.HasValue);
        }

        [Fact]
        public void HaveDataWithNullColumnCorrectlyMapped_MappedAsRecordMutable()
        {
            var result = GetAllFromNullablesByQuery<Models.NullableRecordMutable>();

            Assert.Contains(result, a => a.NullableIdCol.HasValue);
        }

        [Fact]
        public void HaveDataWithNullColumn_MappedAsClass()
        {
            var result = GetAllFromNullablesByQuery<Models.NullableClass>();

            Assert.True(result.Any());
        }

        [Fact]
        public void HaveDataWithNullColumnCorrectlyMapped_MappedAsClass()
        {
            var result = GetAllFromNullablesByQuery<Models.NullableClass>();

            Assert.Contains(result, a => a.NullableIdCol.HasValue);
        }

        #endregion

        #endregion
    }
}
