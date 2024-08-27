using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.DanhMuc.Dtos
{
    public class LoaiHopDongDto
    {
        public Guid? Id { get; set; }
        public string? MaLoaiHopDong { get; set; }
        public string? TenLoaiHopDong { get; set; }
        public ThoiHanLoaiHopDongConst ThoiHanLoaiHopDong { get; set; }
    }
}
