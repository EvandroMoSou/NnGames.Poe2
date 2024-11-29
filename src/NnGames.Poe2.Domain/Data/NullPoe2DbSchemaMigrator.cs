using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace NnGames.Poe2.Data;

/* This is used if database provider does't define
 * IPoe2DbSchemaMigrator implementation.
 */
public class NullPoe2DbSchemaMigrator : IPoe2DbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
