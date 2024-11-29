using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NnGames.Poe2.Data;
using Volo.Abp.DependencyInjection;

namespace NnGames.Poe2.EntityFrameworkCore;

public class EntityFrameworkCorePoe2DbSchemaMigrator
    : IPoe2DbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCorePoe2DbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the Poe2DbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<Poe2DbContext>()
            .Database
            .MigrateAsync();
    }
}
