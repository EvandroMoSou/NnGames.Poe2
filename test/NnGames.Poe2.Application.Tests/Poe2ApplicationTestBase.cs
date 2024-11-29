using Volo.Abp.Modularity;

namespace NnGames.Poe2;

public abstract class Poe2ApplicationTestBase<TStartupModule> : Poe2TestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
