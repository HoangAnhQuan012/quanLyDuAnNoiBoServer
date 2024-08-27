using quanLyDuAnNoiBo.CongViec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class CongViec : FullAuditedEntity<Guid>
    {
        public Guid ChamCongId { get; set; }
        public Guid SubtaskId { get; set; }
        public LoaiHinhConsts LoaiHinh { get; set; }
        public bool OnSite { get; set; }
        public double SoGio { get; set; }
        public Guid KieuViecId { get; set; }
        public string? GhiChu { get; set; }
        public ICollection<TaiLieuDinhKemCongViec>? TaiLieuDinhKemCongViec { get; set; }
    }
}
