using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyDuAnNoiBo.BoDQuanLyDuAn.Dtos
{
    public class CreateDuAnDto
    {
        public Guid? Id { get; set; }
        public string? MaDuAn { get; set; }
        public string? TenDuAn { get; set; }
        public double GiaTriHopDong { get; set; }
        public string? SoHopDong { get; set; }
        public string? NoiDungPhatTrien { get; set; }
        public string? KhachHang { get; set; }
        public TrangThaiDuAnConsts? TrangThai { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? QuyTrinhPhatTrien { get; set; }
        public string? CongNgheSuDung { get; set; }
        public string UngDungDauCuoi { get; set; }
        public Guid? QuanLyDuAnId { get; set; }
        public double LuongCoSo { get; set; }
        public Guid DuAnId { get; set; }
        public string? BanGiaoDungHan { get; set; }
        public string? HieuQuaSuDungNhanSu { get; set; }
        public string? NoLucKhacPhucLoi { get; set; }
        public string? MucDoHaiLongCuaKhachHang { get; set; }
        public string? MucDoloiBiPhatHien { get; set; }
        public string? MucDoLoiUAT { get; set; }
        public string? TyLeThucHienDungQuyTrinh { get; set; }
        public string? NangSuatTaoTestcase { get; set; }
        public string? NangSuatThuThiTestcase { get; set; }
        public string? NangSuatDev { get; set; }
        public string? NangSuatVietUT { get; set; }
        public string? NangSuatThucThiUT { get; set; }
        public string? NangSuatBA { get; set; }
        public double GiaTriHD { get; set; }
        public double ChiPhiABH { get; set; }
        public double ChiPhiOpexPhanBo { get; set; }
        public double ChiPhiLuongDuKien { get; set; }
        public double ChiPhiLuongThucTe { get; set; }
        public double LaiDuKien { get; set; }
        public double LaiThucTe { get; set; }
        public List<ModuleDto>? Module { get; set; }
    }
}
