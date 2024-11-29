using NnGames.Poe2.Samples;
using Xunit;

namespace NnGames.Poe2.EntityFrameworkCore.Domains;

[Collection(Poe2TestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<Poe2EntityFrameworkCoreTestModule>
{

}
