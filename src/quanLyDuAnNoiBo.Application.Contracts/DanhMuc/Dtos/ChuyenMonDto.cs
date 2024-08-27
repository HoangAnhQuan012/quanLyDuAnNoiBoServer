using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.DanhMuc.Dtos
{
    public class ChuyenMonDto
    {
        public Guid? Id { get; set; }
        public string TenChuyenMon { get; set; }
        public string? MoTa { get; set; }
    }
}
