using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class TaiLieuDinhKemDuAn : FullAuditedEntity<Guid>
    {
        public  string TenDinhKem { get; set; }
        public  string FileDinhKem { get; set; }
        public  Guid? DuAnId { get; set; }
    }
}
