using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.DanhMuc.Dtos
{
    public class CaLamViecDto
    {
        public Guid? Id { get; set; }
        public string? MaCaLamViec { get; set; }
        public string? GioVaoLam { get; set; }
        public string? GioTanLam { get; set; }
        public string? Mota { get; set; }
    }
}
