using Xunit;

namespace NnGames.Poe2.EntityFrameworkCore;

[CollectionDefinition(Poe2TestConsts.CollectionDefinitionName)]
public class Poe2EntityFrameworkCoreCollection : ICollectionFixture<Poe2EntityFrameworkCoreFixture>
{

}
