using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class SprintDuAn : FullAuditedEntity<Guid>
    {
        public required string TenSprint { get; set; }
        public required DateTime NgayBatDau { get; set; }
        public required DateTime NgayKetThuc { get; set; }
        public required Guid DuAnId { get; set; }
        public required TrangThaiSprintConsts TrangThaiSprint { get; set; }

        public IEnumerable<object> DefaultIfEmpty()
        {
            throw new NotImplementedException();
        }
    }
}
