using Kros.KORM.Metadata.Attribute;

namespace Korm.Query.Tests
{
    public static class Models
    {
        #region NotNullables

        /// <summary>
        /// Record model for table with not nullable column.
        /// </summary>
        public record NotNullableRecord(long Id, string Name);

        /// <summary>
        /// Record model for table with not nullable column with alias.
        /// </summary>
        [Alias("NotNullables")]
        public record NotNullableRecordAlias(long Id, string Name);

        /// <summary>
        /// NotNullable class model.
        /// </summary>
        public class NotNullableClass
        {
            public long Id { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// NotNullable class model with alias.
        /// </summary>
        [Alias("NotNullables")]
        public class NotNullableClassAlias : NotNullableClass
        {

        }

        #endregion

        #region Nullables

        /// <summary>
        /// Nullable record model.
        /// </summary>
        public record NullableRecord(long Id, int Number, long? NullableIdCol);

        /// <summary>
        /// Nullable record model with mutable NullableIdCol property.
        /// </summary>

        public record NullableRecordMutable(long Id, int Number)
        {
            public long? NullableIdCol { get; set; }
        }

        /// <summary>
        /// Nullable record model with alias.
        /// </summary>
        [Alias("Nullables")]
        public record NullableRecordAlias(long Id, int Number, long? NullableIdCol);

        /// <summary>
        /// Nullable record model with alias and mutable NullableIdCol property.
        /// </summary>
        [Alias("Nullables")]
        public record NullableRecordAliasMutable(long Id, int Number)
        {
            public long? NullableIdCol { get; set; }
        }

        /// <summary>
        /// Nullable class model.
        /// </summary>
        public class NullableClass
        {
            public long Id { get; set; }
            public int Number { get; set; }
            public long? NullableIdCol { get; set; }
        }

        /// <summary>
        /// Nullable class model with alias.
        /// </summary>
        [Alias("Nullables")]
        public class NullableClassAlias : NullableClass
        {

        }

        #endregion
    }
}
