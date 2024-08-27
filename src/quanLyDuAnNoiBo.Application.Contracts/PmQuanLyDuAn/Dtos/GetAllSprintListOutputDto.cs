using quanLyDuAnNoiBo.DuAn;
using quanLyDuAnNoiBo.Global.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.PmQuanLyDuAn.Dtos
{
    public class GetAllSprintListOutputDto
    {
        public Guid? Id { get; set; }
        public string? TenSprint { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public Guid? DuAnId { get; set; }
        public double? TongSoGio { get; set; }
        public TrangThaiSprintConsts? TrangThaiSprint { get; set; }
        public List<TaskInfo>? Tasks { get; set; }
    }

    public class TaskInfo
    {
        public Guid? TaskId { get; set; }
        public string? TenTask { get; set; }
        public double? TongSoGio { get; set; }
        public List<SubtaskInfo>? Subtasks { get; set; }
    }

    public class SubtaskInfo
    {
        public Guid? SubtaskId { get; set; }
        public string? TenSubtask { get; set; }
        public double? PM { get; set; }
        public double? DEV { get; set; }
        public double? BA { get; set; }
        public double? TESTER { get; set; }
        public double? TongSoGio { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public List<LookupTableDto>? NhanSu { get; set; }
        public TrangThaiSubtaskConsts? TrangThaiSubtask { get; set; }
    }
}
