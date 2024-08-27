using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.BoDQuanLyDuAn.Dtos
{
    public class SprintDto
    {
        public  string TenSprint { get; set; }
        public  DateTime NgayBatDau { get; set; }
        public  DateTime NgayKetThuc { get; set; }
        public  Guid DuAnId { get; set; }
        public List<ModuleDto> Modules { get; set; }
    }
}
