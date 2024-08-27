using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.BoDQuanLyDuAn.Dtos
{
    public class SubTaskDto
    {
        public Guid Id { get; set; }
        public  string? TenSubTask { get; set; }
        public  double PM { get; set; }
        public  double Dev { get; set; }
        public  double Test { get; set; }
        public  double BA { get; set; }
        public  Guid ModuleId { get; set; }
        public  Guid SprintId { get; set; }
    }
}
