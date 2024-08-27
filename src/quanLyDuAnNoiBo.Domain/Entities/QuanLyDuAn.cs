using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class QuanLyDuAn : FullAuditedEntity<Guid>
    {
        public required string MaDuAn { get; set; }
        public required string TenDuAn { get; set; }
        public double GiaTriHopDong { get; set; }
        public string? SoHopDong { get; set; }
        public string? NoiDungPhatTrien { get; set; }
        public required string KhachHang { get; set; }
        public TrangThaiDuAnConsts? TrangThai { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public required string QuyTrinhPhatTrien { get; set; }
        public required string CongNgheSuDung { get; set; }
        public string UngDungDauCuoi { get; set; }
        public required Guid QuanLyDuAnId { get; set; }
        public virtual ICollection<ModuleDuAn>? ModuleDuAn { get; set; }
        public virtual ICollection<SprintDuAn>? SprintDuAn { get; set; }
    }
}
