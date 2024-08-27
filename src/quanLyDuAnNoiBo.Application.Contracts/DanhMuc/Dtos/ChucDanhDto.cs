using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.DanhMuc.Dtos
{
    public class ChucDanhDto
    {
        public Guid? Id { get; set; }
        public string? MaChucDanh { get; set; }
        public string? TenChucDanh { get; set; }
        public int NgachChucDanh { get; set; }
    }
}
