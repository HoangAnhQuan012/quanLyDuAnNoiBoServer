using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.DanhMuc.Dtos
{
    public class PhongBanDto
    {
        public Guid? Id { get; set; }
        public string? MaPhongBan { get; set; }
        public string? TenPhongBan { get; set; }
        public string? MoTa { get; set; }
    }
}
