using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.ChamCong.Dtos
{
    public class DuyetChamCongListInputDto
    {
        public Guid NhanVienId { get; set; }
        public List<Guid> ChamCongIds { get; set; }
    }
}
