using quanLyDuAnNoiBo.BoDQuanLyDuAn.Dtos;
using quanLyDuAnNoiBo.DuAn;
using quanLyDuAnNoiBo.Global.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.PmQuanLyDuAn.Dtos
{
    public class CreateOrUpdateSprintDto
    {
        public Guid? SprintId { get; set; }
        public string? TenSprint { get; set; }
        public  DateTime NgayBatDau { get; set; }
        public  DateTime NgayKetThuc { get; set; }
        public  Guid DuAnId { get; set; }
        public TrangThaiSprintConsts TrangThaiSprint { get; set; }
        public List<SubtaskTimeAndEmployees>? Subtasks { get; set; }
    }

    public class SubtaskTimeAndEmployees
    {
        public Guid SubtaskId { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public List<LookupTableDto> NhanSu { get; set; }
    }
}
