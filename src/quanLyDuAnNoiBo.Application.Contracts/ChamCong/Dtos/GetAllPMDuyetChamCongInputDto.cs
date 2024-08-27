using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.ChamCong.Dtos
{
    public class GetAllPMDuyetChamCongInputDto : PagedAndSortedResultRequestDto
    {
        public string? Keyword { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
