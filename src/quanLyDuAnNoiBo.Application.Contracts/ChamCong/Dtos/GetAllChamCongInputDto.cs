using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.ChamCong.Dtos
{
    public class GetAllChamCongInputDto : PagedAndSortedResultRequestDto
    {
        public Guid DuAnId { get; set; }
    }
}
