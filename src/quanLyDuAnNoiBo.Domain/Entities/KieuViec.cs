using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class KieuViec : FullAuditedEntity<Guid>
    {
        public string TenKieuViec { get; set; }
        public Guid ChucDanhId { get; set; }
    }
}
