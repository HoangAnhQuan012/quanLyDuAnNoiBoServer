using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.PmQuanLyDuAn.Dtos
{
    public class CreateSubTaskDto
    {
        public Guid ModuleId { get; set; }
        public string TenSubTask { get; set; }
        public double PM { get; set; }
        public double Dev { get; set; }
        public double Test { get; set; }
        public double BA { get; set; }
    }
}
