using Volo.Abp.Settings;

namespace NnGames.Poe2.Settings;

public class Poe2SettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(Poe2Settings.MySetting1));
    }
}
