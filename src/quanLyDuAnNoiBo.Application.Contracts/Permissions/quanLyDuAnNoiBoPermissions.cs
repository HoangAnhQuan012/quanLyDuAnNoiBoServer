namespace quanLyDuAnNoiBo.Permissions;

public static class quanLyDuAnNoiBoPermissions
{
    public const string GroupName = "quanLyDuAnNoiBo";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public static class AccountManagement
    {
        public const string Default = GroupName + ".AccountManagement";
    }
    public static class RoleManagement
    {
        public const string Default = GroupName + ".RoleManagement";
    }
    public static class ProjectManagement
    {
        public const string Default = GroupName + ".ProjectManagement";
    }
    public static class DanhMucManagement
    {
        public const string Default = GroupName + ".DanhMucManagement";
    }

    public static class BoDQuanLyDuAn
    {
        public const string Default = GroupName + ".BoDProjectManagement";
    }

    public static class HRManagement
    {
        public const string Default = GroupName + ".HRManagement";
    }

    public static class NhanVienChamCong
    {
        public const string Default = GroupName + ".NhanVienChamCong";
    }

    public static class PheDuyetChamCong
    {
        public const string Default = GroupName + ".PheDuyetChamCong";
    }

    public static class BaoCaoChamCong
    {
        public const string Default = GroupName + ".BaoCaoChamCong";
    }
}
