using System;
using FluentAssertions;
using Kros.Data.Schema;
using Kros.Data.Schema.SqlServer;
using Kros.KORM;
using System.Collections.Generic;
using Xunit;

namespace Korm.Query.Tests
{
    public class DatabaseFixture : Kros.UnitTests.SqlServerDatabaseTestBase
    {
        #region Constants

        private const string NotNullablesTableName = "NotNullables";
        private const string NullablesTableName = "Nullables";

        private static readonly string[] _databaseInitScripts = new string[]
        {
            CreateTable_NotNullables,
            CreateTable_Nullables,
            Seed_Script
        };


        private const string CreateTable_NotNullables =
            @"CREATE TABLE [dbo].[NotNullables](
                [Id] [bigint] NOT NULL,
                [Name] [nvarchar](255) NOT NULL,

                CONSTRAINT [PK_NotNullables] PRIMARY KEY CLUSTERED ([Id] ASC)
                WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY];";

        private const string CreateTable_Nullables =
            @"CREATE TABLE [dbo].[Nullables](
                    [Id] [bigint] NOT NULL,
                    [Number] [int] NOT NULL,
                    [NullableIdCol] [bigint] NULL,
                    [NullableStringCol] [nvarchar](255) NULL,

                    CONSTRAINT [PK_Nullables] PRIMARY KEY CLUSTERED ([Id] ASC)
                    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                ) ON [PRIMARY]

                ALTER TABLE [dbo].[Nullables]  WITH CHECK ADD  CONSTRAINT [FK_Nullables_NotNullables] FOREIGN KEY([NullableIdCol])
                REFERENCES [dbo].[NotNullables] ([Id])
                ON DELETE CASCADE

                ALTER TABLE [dbo].[Nullables] CHECK CONSTRAINT [FK_Nullables_NotNullables]";


        private const string Seed_Script =
            @"  INSERT INTO [dbo].[NotNullables] ([Id], [Name]) VALUES (1, 'Sausage of Lovers')
                INSERT INTO [dbo].[NotNullables] ([Id], [Name]) VALUES (2, 'One Time Next Time')

                INSERT INTO [dbo].[Nullables] ([Id], [Number], [NullableIdCol], [NullableStringCol]) VALUES (1, 10, 1, NULL)
                INSERT INTO [dbo].[Nullables] ([Id], [Number], [NullableIdCol], [NullableStringCol]) VALUES (2, 20, 1, 'Text')
                INSERT INTO [dbo].[Nullables] ([Id], [Number], [NullableIdCol], [NullableStringCol]) VALUES (3, 30, 2, 'Text')
                INSERT INTO [dbo].[Nullables] ([Id], [Number], [NullableIdCol], [NullableStringCol]) VALUES (4, 60, NULL, 'Text')
                INSERT INTO [dbo].[Nullables] ([Id], [Number], [NullableIdCol], [NullableStringCol]) VALUES (5, 70, NULL, 'Text')";


        #endregion

        #region SqlServerDatabaseTestBase Overrides

        protected override string BaseConnectionString => SqlHelper.ConnectionString;

        protected override IEnumerable<string> DatabaseInitScripts => _databaseInitScripts;

        #endregion

        public Database Db { get; private set; }

        public DatabaseFixture()
        {
            Db = new Database(ServerHelper.Connection);
        }

        #region Tests

        [Fact]
        public void NotNullablesTable_ShouldExists()
        {
            SqlServerSchemaLoader loader = new SqlServerSchemaLoader();

            TableSchema schema = loader.LoadTableSchema(ServerHelper.Connection, NotNullablesTableName);

            schema.Should().NotBeNull();
        }

        [Fact]
        public void NullablesTable_ShouldExists()
        {
            SqlServerSchemaLoader loader = new SqlServerSchemaLoader();

            TableSchema schema = loader.LoadTableSchema(ServerHelper.Connection, NullablesTableName);

            schema.Should().NotBeNull();
        }

        [Fact]
        public void DatabaseShouldExists()
        {
            Db.Should().NotBeNull();
        }

        #endregion
    }
}
