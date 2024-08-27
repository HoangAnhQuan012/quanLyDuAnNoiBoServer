using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.ChamCong.Dtos
{
    public class GetAllDuAnPMDuyetChamCongOutputDto
    {
        public Guid DuAnId { get; set; }
        public string TenDuAn { get; set; }
        public int NhanSuChoDuyetChamCong { get; set; }
        public double TongThoiGianChoDuyet { get; set; }
        public double TongThoiGianDaThucHien { get; set; }
        public TrangThaiPheDuyetChamCongConsts TrangThaiPheDuyet { get; set; }
    }
}
