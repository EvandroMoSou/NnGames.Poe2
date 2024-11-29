using NnGames.Poe2.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace NnGames.Poe2.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class Poe2Controller : AbpControllerBase
{
    protected Poe2Controller()
    {
        LocalizationResource = typeof(Poe2Resource);
    }
}
