using Volo.Abp.Modularity;

namespace NnGames.Poe2;

[DependsOn(
    typeof(Poe2DomainModule),
    typeof(Poe2TestBaseModule)
)]
public class Poe2DomainTestModule : AbpModule
{

}
