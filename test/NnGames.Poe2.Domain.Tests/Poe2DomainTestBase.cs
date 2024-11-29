using Volo.Abp.Modularity;

namespace NnGames.Poe2;

/* Inherit from this class for your domain layer tests. */
public abstract class Poe2DomainTestBase<TStartupModule> : Poe2TestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
