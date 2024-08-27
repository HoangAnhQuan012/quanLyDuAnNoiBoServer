using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.DanhMuc.Dtos
{
    public class GetAllInputCaLamViecDto : PagedAndSortedResultRequestDto
    {
        public string? Keyword { get; set; }
    }
}
