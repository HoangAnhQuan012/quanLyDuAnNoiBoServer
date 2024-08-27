using quanLyDuAnNoiBo.BoDQuanLyDuAn.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.PmQuanLyDuAn.Dtos
{
    public class GetDuAnByPmIdDto
    {
        public string TenDuAn { get; set; }
        public List<ModuleDto> Modules { get; set; }
    }
}
