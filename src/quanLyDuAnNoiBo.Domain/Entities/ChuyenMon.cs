using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class ChuyenMon : FullAuditedEntity<Guid>
    {
        [Required]
        [MaxLength(QuanLyDuAnNoiBoConst.MaxNameLength)]
        public string TenChuyenMon { get; set; }

        [CanBeNull]
        [MaxLength(QuanLyDuAnNoiBoConst.MaxNameLength)]
        public string? MoTa { get; set; }
    }
}
