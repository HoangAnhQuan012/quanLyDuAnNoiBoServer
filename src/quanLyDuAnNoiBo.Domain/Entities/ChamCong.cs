using quanLyDuAnNoiBo.ChamCong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class ChamCong : FullAuditedEntity<Guid>
    {
        public Guid NhanVienId { get; set; }
        public Guid SprintId { get; set; }
        public Guid DuAnId { get; set; }
        public Guid TaskId { get; set; }
        public string? TenTask { get; set; }
        public DateTime NgayChamCong { get; set; }
        public DateTime NgayDuyetChamCong { get; set; }
        public TrangThaiChamCongConsts TrangThaiChamCong { get; set; }
        public double ThoiGianChamCong { get; set; }
    }
}
