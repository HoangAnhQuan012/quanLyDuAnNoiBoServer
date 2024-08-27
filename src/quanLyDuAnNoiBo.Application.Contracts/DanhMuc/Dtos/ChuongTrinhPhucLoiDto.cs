using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.DanhMuc.Dtos
{
    public class ChuongTrinhPhucLoiDto
    {
        public Guid? Id { get; set; }
        public string? MaChuongTrinhPhucLoi { get; set; }
        public string? TenChuongTrinhPhucLoi { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public ChuongTrinhPhucLoiStatus TrangThai { get; set; }
        public string DiaDiem { get; set; }
        public string? MoTa { get; set; }
    }
}
