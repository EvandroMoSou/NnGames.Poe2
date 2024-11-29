using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NnGames.Poe2.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class Poe2DbContextFactory : IDesignTimeDbContextFactory<Poe2DbContext>
{
    public Poe2DbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        Poe2EfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<Poe2DbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new Poe2DbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NnGames.Poe2.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
