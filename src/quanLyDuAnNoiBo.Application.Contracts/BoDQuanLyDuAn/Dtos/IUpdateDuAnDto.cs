using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;

namespace quanLyDuAnNoiBo.BoDQuanLyDuAn.Dtos
{
    public interface IUpdateDuAnDto
    {
        string? BanGiaoDungHan { get; set; }
        double ChiPhiABH { get; set; }
        double ChiPhiLuongDuKien { get; set; }
        double ChiPhiLuongThucTe { get; set; }
        double ChiPhiOpexPhanBo { get; set; }
        string CongNgheSuDung { get; set; }
        Guid DuAnId { get; set; }
        double GiaTriHD { get; set; }
        double GiaTriHopDong { get; set; }
        string? HieuQuaSuDungNhanSu { get; set; }
        Guid Id { get; set; }
        string KhachHang { get; set; }
        double LaiDuKien { get; set; }
        double LaiThucTe { get; set; }
        double LuongCoSo { get; set; }
        string MaDuAn { get; set; }
        List<ModuleDto> Module { get; set; }
        string? MucDoHaiLongCuaKhachHang { get; set; }
        string? MucDoloiBiPhatHien { get; set; }
        string? MucDoLoiUAT { get; set; }
        string? NangSuatBA { get; set; }
        string? NangSuatDev { get; set; }
        string? NangSuatTaoTestcase { get; set; }
        string? NangSuatThucThiUT { get; set; }
        string? NangSuatThuThiTestcase { get; set; }
        string? NangSuatVietUT { get; set; }
        DateTime? NgayBatDau { get; set; }
        DateTime NgayKetThuc { get; set; }
        string? NoiDungPhatTrien { get; set; }
        string? NoLucKhacPhucLoi { get; set; }
        Guid QuanLyDuAnId { get; set; }
        string QuyTrinhPhatTrien { get; set; }
        string? SoHopDong { get; set; }
        string TenDuAn { get; set; }
        string? TyLeThucHienDungQuyTrinh { get; set; }
        UngDungDauCuoiConsts UngDungDauCuoi { get; set; }
    }
}