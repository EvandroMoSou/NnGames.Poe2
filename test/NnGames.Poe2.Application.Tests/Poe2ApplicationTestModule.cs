using Volo.Abp.Modularity;

namespace NnGames.Poe2;

[DependsOn(
    typeof(Poe2ApplicationModule),
    typeof(Poe2DomainTestModule)
)]
public class Poe2ApplicationTestModule : AbpModule
{

}
