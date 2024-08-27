using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.ChamCong.Dtos
{
    public class GetThongTinChamCongChiTietOutputDto
    {
        public Guid? ChamCongId { get; set; }
        public Guid? NhanVienId { get; set; }
        public string? TenNhanVien { get; set; }
        public DateTime? NgayChamCong { get; set; }
        public string? TenSprint { get; set; }
        public string? TenTask { get; set; }
        public string? TenSubtask { get; set; }
        public string? KieuViec { get; set; }
        public string? LoaiHinh { get; set; }
        public double ThoiGian { get; set; }
        public TrangThaiSubtaskConsts? TrangThaiSubtask { get; set; }
        public TrangThaiChamCongConsts? TrangThaiChamCong { get; set; }
        public DateTime NgayDuyetChamCong { get; set; }
        public List<ChamCongDinhKemFiles>? ChamCongDinhKemFiles { get; set; }
        public string? GhiChu { get; set; }
    }
}
