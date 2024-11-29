using Microsoft.Extensions.Localization;
using NnGames.Poe2.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace NnGames.Poe2.Blazor;

[Dependency(ReplaceServices = true)]
public class Poe2BrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<Poe2Resource> _localizer;

    public Poe2BrandingProvider(IStringLocalizer<Poe2Resource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
