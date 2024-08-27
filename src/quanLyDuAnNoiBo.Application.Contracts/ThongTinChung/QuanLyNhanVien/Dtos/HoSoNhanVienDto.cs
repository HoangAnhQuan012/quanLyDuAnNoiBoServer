using quanLyDuAnNoiBo.HoSoNhanVien;
using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.ThongTinChung.QuanLyNhanVien.Dtos
{
    public class HoSoNhanVienDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string MaNhanVien { get; set; }

        public string HoTen { get; set; }

        public DateTime NgaySinh { get; set; }

        public GioiTinhConsts GioiTinh { get; set; }

        public string? DanToc { get; set; }

        public string? CaLamViec { get; set; }

        public Guid CaLamViecId { get; set; }

        public string CMND { get; set; }

        public DateTime? NgayCapCMND { get; set; }

        public string NoiCapCMND { get; set; }

        public string NoiSinh { get; set; }

        public DateTime NgayVaoLam { get; set; }

        public string? ChucDanh { get; set; }

        public Guid ChucDanhId { get; set; }

        public string? PhongBan { get; set; }

        public Guid PhongBanId { get; set; }

        public string SoDienThoai { get; set; }
        public string? HoChieu { get; set; }

        public string Email { get; set; }
        public string? MaSoThue { get; set; }
        public DateTime? NgayNghiViec { get; set; }
    }
}
