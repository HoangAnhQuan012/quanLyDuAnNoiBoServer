using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.ChamCong.Dtos
{
    public class DuyetChamCongDanhSachNhanVien
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DataChamCong> ChamCongs { get; set; }
    }
}
