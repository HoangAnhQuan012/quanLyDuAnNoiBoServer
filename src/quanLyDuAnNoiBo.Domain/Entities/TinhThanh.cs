using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class TinhThanh : FullAuditedEntity<Guid>
    {
        [Required]
        public Guid QuocGiaId { get; set; }
        [Required]
        [MaxLength(QuanLyDuAnNoiBoConst.MaxNameLength)]
        public string TenTinhThanh { get; set; }
    }
}
