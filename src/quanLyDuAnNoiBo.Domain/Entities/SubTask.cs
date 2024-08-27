using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class SubTask : FullAuditedEntity<Guid>
    {
        public required string TenSubTask { get; set; }
        public required double PM { get; set; }
        public required double Dev { get; set; }
        public required double Test { get; set; }
        public required double BA { get; set; }
        public double TongThoiGian { get; set; }
        public required Guid ModuleId { get; set; }
        public Guid? SprintId { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public string? NhanSu { get; set; }
        public TrangThaiSubtaskConsts? TrangThaiSubtask { get; set; }
    }
}
