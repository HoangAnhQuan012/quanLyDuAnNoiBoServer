using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class SubTask_NhanVien : FullAuditedEntity<Guid>
    {
        public required Guid SubTaskId { get; set; }
        public required Guid NhanVienId { get; set; }
    }
}
