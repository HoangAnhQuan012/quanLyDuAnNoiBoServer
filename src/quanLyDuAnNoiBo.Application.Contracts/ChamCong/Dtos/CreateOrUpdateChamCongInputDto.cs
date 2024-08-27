using quanLyDuAnNoiBo.CongViec;
using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.ChamCong.Dtos
{
    public class CreateOrUpdateChamCongInputDto
    {
        public Guid? ChamCongId { get; set; }
        public Guid SprintId { get; set; }
        public Guid TaskId { get; set; }
        public string? TenTask { get; set; }
        public Guid DuAnId { get; set; }
        public DateTime NgayChamCong { get; set; }
        public double ThoiGianChamCong { get; set; }
        public List<CongViecDto>? CongViecs { get; set; }
    }

    public class CongViecDto
    {
        public Guid? CongViecId { get; set; }
        public Guid SubtaskId { get; set; }
        public Guid KieuViecId { get; set; }
        public bool DanhDauHoanThanh { get; set; }
        public LoaiHinhConsts LoaiHinh { get; set; }
        public bool OnSite { get; set; }
        public double SoGio { get; set; }
        public TrangThaiSubtaskConsts? TrangThaiSubtask { get; set; }
        public List<ChamCongDinhKemFiles>? ChamCongDinhKemFiles { get; set; }
        public string? GhiChu { get; set; }
    }
}
