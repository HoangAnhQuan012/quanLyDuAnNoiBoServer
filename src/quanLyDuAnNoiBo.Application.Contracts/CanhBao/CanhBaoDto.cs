using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.CanhBao
{
    public class CanhBaoDto
    {
        public Guid? CanhBaoId { get; set; }
        public Guid? NhanVienId { get; set; }
        public string? TenTask { get; set; }
        public Guid SubtaskId { get; set; }
        public string? TenSubtask { get; set; }
        public DateTime ThoiGianCanhBao { get; set; }
        public TrangThaiSubtaskConsts? TrangThaiSubtask { get; set; }
        public DateTime? NgayKetThucSubtask { get; set; }
    }
}
