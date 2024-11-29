using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using NnGames.Poe2.Localization;

namespace NnGames.Poe2.Blazor.Client;

public class Poe2BrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<Poe2Resource> _localizer;

    public Poe2BrandingProvider(IStringLocalizer<Poe2Resource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
