using NnGames.Poe2.Localization;
using Volo.Abp.AspNetCore.Components;

namespace NnGames.Poe2.Blazor;

public abstract class Poe2ComponentBase : AbpComponentBase
{
    protected Poe2ComponentBase()
    {
        LocalizationResource = typeof(Poe2Resource);
    }
}
