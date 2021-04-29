using FluentAssertions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Korm.Query.Tests.QuerieTests
{
    [Collection("Korm")]
    public class GeneratedQueryByKormShould
    {
        private readonly DatabaseFixture _fixture;
        public GeneratedQueryByKormShould(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        public IEnumerable<T> GetWithKormQueryBuilder<T>()
            => _fixture.Db.Query<T>().ToList();


        #region Tests

        [Fact]
        public void HaveNotNullableCollection_ByRecordWithAlias()
        {
            var result =  GetWithKormQueryBuilder<Models.NotNullableRecordAlias>();

            Assert.True(result.Any());
        }

        [Fact]
        public void HaveNotNullableCollection_ByClassWithAlias()
        {
            var result = GetWithKormQueryBuilder<Models.NotNullableClassAlias>();

            Assert.True(result.Any());
        }

        [Fact]
        public void ThrowSqlExceptionBecauseMissingAlias_ByRecord()
        {
            Action action = () => GetWithKormQueryBuilder<Models.NotNullableRecord>();

            action.Should().Throw<SqlException>().WithMessage("Invalid object name 'NotNullableRecord'.");
        }

        [Fact]
        public void ThrowSqlExceptionBecauseMissingAlias_ByClass()
        {
            Action action = () => GetWithKormQueryBuilder<Models.NotNullableClass>();

            action.Should().Throw<SqlException>().WithMessage("Invalid object name 'NotNullableClass'.");
        }

        #endregion
    }
}
