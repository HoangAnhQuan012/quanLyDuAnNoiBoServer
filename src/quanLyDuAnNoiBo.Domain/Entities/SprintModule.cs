using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class SprintModule: FullAuditedEntity<Guid>
    {
        public Guid? SprintId { get; set; }
        public Guid? MaModule { get; set; }
        public string SubTaskModule { get; set; }
        public double? Gio { get; set; }
    }
}
