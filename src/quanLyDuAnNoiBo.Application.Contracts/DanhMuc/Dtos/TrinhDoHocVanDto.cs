using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.DanhMuc.Dtos
{
    public class TrinhDoHocVanDto
    {
        public Guid? Id { get; set; }
        public string? MaTrinhDoHocVan { get; set; }
        public string? TenTrinhDoHocVan { get; set; }
        public string? MoTa { get; set; }
    }
}
