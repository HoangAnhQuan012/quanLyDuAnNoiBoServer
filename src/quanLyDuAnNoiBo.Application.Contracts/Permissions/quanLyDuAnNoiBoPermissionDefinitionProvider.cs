using quanLyDuAnNoiBo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace quanLyDuAnNoiBo.Permissions;

public class quanLyDuAnNoiBoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(quanLyDuAnNoiBoPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(quanLyDuAnNoiBoPermissions.MyPermission1, L("Permission:MyPermission1"));
        myGroup.AddPermission(quanLyDuAnNoiBoPermissions.AccountManagement.Default, L("Permission:AccountManagement"));
        //myGroup.AddPermission(quanLyDuAnNoiBoPermissions.RoleManagement.Default, L("Permission:RoleManagement"));
        myGroup.AddPermission(quanLyDuAnNoiBoPermissions.ProjectManagement.Default, L("Permission:ProjectManagement"));
        myGroup.AddPermission(quanLyDuAnNoiBoPermissions.BoDQuanLyDuAn.Default, L("Permission:BoDProjectManagement"));
        myGroup.AddPermission(quanLyDuAnNoiBoPermissions.DanhMucManagement.Default, L("Permission:DanhMucManagement"));
        myGroup.AddPermission(quanLyDuAnNoiBoPermissions.HRManagement.Default, L("Permission:HRManagement"));
        myGroup.AddPermission(quanLyDuAnNoiBoPermissions.NhanVienChamCong.Default, L("Permission:NhanVienChamCong"));
        myGroup.AddPermission(quanLyDuAnNoiBoPermissions.PheDuyetChamCong.Default, L("Permission:PheDuyetChamCong"));
        myGroup.AddPermission(quanLyDuAnNoiBoPermissions.BaoCaoChamCong.Default, L("Permission:BaoCaoChamCong"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<quanLyDuAnNoiBoResource>(name);
    }
}
