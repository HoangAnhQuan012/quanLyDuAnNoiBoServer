using quanLyDuAnNoiBo.Global.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.PmQuanLyDuAn.Dtos
{
    public class SubTaskListGroupByModuleDto
    {
        public Guid SprintId { get; set; }
        public Guid ModuleId { get; set; }
        public string TenModule { get; set; }
        public double TongSogGio { get; set; }
        public List<SubTaskInner> SubTasks { get; set; }
    }

    public class SubTaskInner
    {
        public Guid SubTaskId { get; set; }
        public string SubTaskName { get; set; }
        public double PM { get; set; }
        public double Dev { get; set; }
        public double Test { get; set; }
        public double BA { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public List<LookupTableDto> NhanSu { get; set; }
    }
}
