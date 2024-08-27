using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class ChiTietDuAn : FullAuditedEntity<Guid>
    {
        public Guid DuAnId { get; set; }
        public double LuongCoSo { get; set; }
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
        public double GiaTriHopDong { get; set; }
        public double ChiPhiABH { get; set; }
        public double ChiPhiOpexPhanBo { get; set; }
        public double ChiPhiLuongDuKien { get; set; }
        public double ChiPhiLuongThucTe { get; set; }
        public double LaiDuKien { get; set; }
        public double LaiThucTe { get; set; }
        public virtual ICollection<TaiLieuDinhKemDuAn>? DanhSachDuAnDinhKem { get; set; }
    }
}
