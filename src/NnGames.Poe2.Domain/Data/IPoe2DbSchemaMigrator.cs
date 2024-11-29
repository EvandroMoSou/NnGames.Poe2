using System.Threading.Tasks;

namespace NnGames.Poe2.Data;

public interface IPoe2DbSchemaMigrator
{
    Task MigrateAsync();
}
