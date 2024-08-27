using quanLyDuAnNoiBo.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class ChuongTrinhPhucLoi : FullAuditedEntity<Guid>
    {
        [Required]
        [MaxLength(QuanLyDuAnNoiBoConst.MaxNameLength)]
        public string TenChuongTrinh { get; set; }
        [Required]
        [MaxLength(QuanLyDuAnNoiBoConst.MaxCodeLength)]
        public string MaChuongTrinh { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public ChuongTrinhPhucLoiStatus TrangThai { get; set; }
        [Required]
        [MaxLength(QuanLyDuAnNoiBoConst.MaxNameLength)]
        public string DiaDiem { get; set; }
        [Required]
        [MaxLength(QuanLyDuAnNoiBoConst.MaxNameLength)]
        public string MoTa { get; set; }
    }
}
