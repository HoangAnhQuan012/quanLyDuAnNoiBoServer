using quanLyDuAnNoiBo.HoSoNhanVien;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace quanLyDuAnNoiBo.Entities
{
    public class Hs_HoSoNhanVien : FullAuditedEntity<Guid>
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(QuanLyDuAnNoiBoConst.MaxCodeLength)]
        public string MaNhanVien { get; set; }
        [Required]
        [MaxLength(QuanLyDuAnNoiBoConst.MaxNameLength)]
        public string HoTen { get; set; }
        [Required]
        public DateTime NgaySinh { get; set; }
        [Required]
        public GioiTinhConsts GioiTinh { get; set; }
        [Required]
        public Guid DanToc { get; set; }
        [Required]
        public Guid CaLamViecId { get; set; }
        [MaxLength(QuanLyDuAnNoiBoConst.MaxCodeLength)]
        public string CMND { get; set; }
        public DateTime? NgayCapCMND { get; set; }
        [MaxLength(QuanLyDuAnNoiBoConst.MaxNameLength)]
        public string NoiCapCMND { get; set; }
        [Required]
        [MaxLength(QuanLyDuAnNoiBoConst.MaxNameLength)]
        public string NoiSinh { get; set; }
        [Required]
        public DateTime NgayVaoLam { get; set; }
        [Required]
        public Guid ChucDanhId { get; set; }
        public Guid PhongBanId { get; set; }
        [Required]
        [MaxLength(QuanLyDuAnNoiBoConst.MaxCodeLength)]
        public string SoDienThoai { get; set; }
        [MaxLength(QuanLyDuAnNoiBoConst.MaxCodeLength)]
        public string? HoChieu { get; set; }
        [Required]
        [MaxLength(QuanLyDuAnNoiBoConst.MaxEmailLength)]
        public string Email { get; set; }
        [MaxLength(QuanLyDuAnNoiBoConst.MaxCodeLength)]
        public string? MaSoThue { get; set; }
        public DateTime? NgayNghiViec { get; set; }
        //public virtual ICollection<TrinhDoHocVan> Hs_TrinhDoHocVan { get; set; }
        //public virtual ICollection<ChungChi> Hs_ChungChi { get; set; }
    }
}
