using quanLyDuAnNoiBo.CanhBao;
using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class CanhBao : FullAuditedEntity<Guid>
    {
        public string? TenTask { get; set; }
        public Guid? SubtaskId { get; set; }
        public string? TenSubtask { get; set; }
        public TrangThaiSubtaskConsts? TrangThaiSubtask { get; set; }
        public DateTime? ThoiGianCanhBao { get; set; }
        public DateTime? ThoiGianKetThucSubtask { get; set; }

        public CanhBao() { }
        public CanhBao UpdateCanhBao(DateTime? ngayKetThucSubtask, TrangThaiSubtaskConsts? trangThaiSubtask)
        {
            ThoiGianCanhBao = DateTime.Now;
            ThoiGianKetThucSubtask = ngayKetThucSubtask;
            TrangThaiSubtask = trangThaiSubtask;
            return this;
        }
    }

}
