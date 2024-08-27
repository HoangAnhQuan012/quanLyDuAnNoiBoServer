using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.ChamCong.Dtos
{
    public class GetAllDuAnChamCongDto
    {
        public Guid? Id { get; set; }
        public string? TenDuAn { get; set; }
        public string? KhachHang { get; set; }
        public TrangThaiDuAnConsts? TrangThai { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? TenQuanLyDuAn { get; set; }
        public Guid QuanLyDuAnId { get; set; }
    }
}
