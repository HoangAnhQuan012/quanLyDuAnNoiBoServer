using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class ModuleDuAn : FullAuditedEntity<Guid>
    {
        public virtual required string TenModule { get; set; }
        public virtual double PM { get; set; }
        public virtual double Dev { get; set; }
        public virtual double Test { get; set; }
        public virtual double BA { get; set; }
        public virtual double TongThoiGian { get; set; }
        public virtual required Guid DuAnId { get; set; }
    }
}
