using NnGames.Poe2.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace NnGames.Poe2.Permissions;

public class Poe2PermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(Poe2Permissions.GroupName);

        var booksPermission = myGroup.AddPermission(Poe2Permissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(Poe2Permissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(Poe2Permissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(Poe2Permissions.Books.Delete, L("Permission:Books.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(Poe2Permissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<Poe2Resource>(name);
    }
}
