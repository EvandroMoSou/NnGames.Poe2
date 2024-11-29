using NnGames.Poe2.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace NnGames.Poe2.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(Poe2EntityFrameworkCoreModule),
    typeof(Poe2ApplicationContractsModule)
)]
public class Poe2DbMigratorModule : AbpModule
{
}
