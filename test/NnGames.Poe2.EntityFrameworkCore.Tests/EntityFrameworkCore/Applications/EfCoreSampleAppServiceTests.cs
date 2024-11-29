using NnGames.Poe2.Samples;
using Xunit;

namespace NnGames.Poe2.EntityFrameworkCore.Applications;

[Collection(Poe2TestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<Poe2EntityFrameworkCoreTestModule>
{

}
