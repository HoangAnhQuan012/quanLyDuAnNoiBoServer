using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyDuAnNoiBo.BoDQuanLyDuAn.Dtos
{
    public class GetAllDuAnDto 
    {
        public Guid? Id { get; set; }
        public string? TenDuAn { get; set; }
        public string? KhachHang { get; set; }
        public TrangThaiDuAnConsts? TrangThai { get; set; }
        public int? TienDo { get; set; }
        public int? ChiPhi { get; set; }
        public string? MaDuAn { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? NhanSu { get; set; }
    }
}
