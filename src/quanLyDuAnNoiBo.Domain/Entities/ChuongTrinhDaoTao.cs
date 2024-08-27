using quanLyDuAnNoiBo.HoSoNhanVien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class ChuongTrinhDaoTao : FullAuditedEntity<Guid>
    {
        public string MaKhoaDaoTao { get; set; }
        public string TenKhoaDaoTao { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public ChuongTrinhDaoTaoStatusConsts TrangThai { get; set; }
        public string DiaDiem { get; set; }
        public string NoiDungDaoTao { get; set; }
    }
}
