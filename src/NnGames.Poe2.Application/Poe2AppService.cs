using NnGames.Poe2.Localization;
using Volo.Abp.Application.Services;

namespace NnGames.Poe2;

/* Inherit your application services from this class.
 */
public abstract class Poe2AppService : ApplicationService
{
    protected Poe2AppService()
    {
        LocalizationResource = typeof(Poe2Resource);
    }
}
