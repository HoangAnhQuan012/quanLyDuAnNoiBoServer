using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.ChamCong.Dtos
{
    public class GetAllChamCongListOutput
    {
        public Guid? ChamCongId { get; set; }
        public DateTime? NgayChamCong { get; set; }
        public string? TenSprint { get; set; }
        public string? TenTask { get; set; }
        public double ThoiGian { get; set; }
        public TrangThaiChamCongConsts? TrangThaiChamCong { get; set; }
        public List<ChamCongDetail>? ChamCongDetail { get; set; }
    }

    public class ChamCongDetail
    {
        public string? TenSubtask { get; set; }
        public double? ThoiGian { get; set; }
        public string? KieuViec { get; set; }
        public string? LoaiHinh { get; set; }
        public List<ChamCongDinhKemFiles>? ChamCongDinhKemFiles { get; set; }
        public DateTime? NgayDuyetChamCong { get; set; }
    }

    public class ChamCongDinhKemFiles
    {
        public string? TenDinhKem { get; set; }
        public string? FileDinhKem { get; set; }
    }
}
