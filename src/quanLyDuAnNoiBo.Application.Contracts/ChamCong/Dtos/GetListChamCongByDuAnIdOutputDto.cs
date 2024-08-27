using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.ChamCong.Dtos
{
    public class GetListChamCongByDuAnIdOutputDto
    {
        public Guid NhanVienId { get; set; }
        public string? TenNhanVien { get; set; }
        public List<DataChamCong>? ListChamCong { get; set; }
    }

    public class DataChamCong
    {
        public Guid ChamCongId { get; set; }
        public DateTime? NgayChamCong { get; set; }
        public double ThoiGianChamCong { get; set; }
        public TrangThaiChamCongConsts? TrangThaiChamCong { get; set; }
        public bool NghiNuaNgay { get; set; }
        public bool IsDayOff { get; set; }
    }
}
