using Xunit;

namespace Korm.Query.Tests
{
    [CollectionDefinition("Korm")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {

    }
}
