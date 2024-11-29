using NnGames.Poe2.Books;
using Xunit;

namespace NnGames.Poe2.EntityFrameworkCore.Applications.Books;

[Collection(Poe2TestConsts.CollectionDefinitionName)]
public class EfCoreBookAppService_Tests : BookAppService_Tests<Poe2EntityFrameworkCoreTestModule>
{

}