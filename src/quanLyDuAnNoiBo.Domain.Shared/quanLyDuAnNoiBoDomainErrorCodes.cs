namespace quanLyDuAnNoiBo;

public static class quanLyDuAnNoiBoDomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */
    public static class Login
    {

    }

    public const string AccountAlreadyExists = "Tài Khoản đã tồn tại!";
    public const string DuAnAlreadyExists = "Dự Án đã tồn tại!";
    public const string SprintAlreadyExists = "Sprint đã tồn tại!";
    public const string ModuleAlreadyExists = "Module đã tồn tại!";
    public const string SubTaskAlreadyExists = "SubTask đã tồn tại!";
    public const string FileWrongFormat = "File tải lên không đúng định dạng!";
    public const string MaChucDanhExist = "Mã chức danh đã tồn tại!";
    public const string PhongBanExist = "Mã phòng ban đã tồn tại";
    public const string ChamCongQuaThoiGianEst = "Thời gian chấm công hiện tại đã quá tổng thời gian ước lượng!";
}
