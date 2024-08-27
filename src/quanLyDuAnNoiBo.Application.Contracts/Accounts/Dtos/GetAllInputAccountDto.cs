using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.Accounts.Dtos
{
    public class GetAllInputAccountDto : PagedAndSortedResultRequestDto
    {
        public string? Keyword { get; set; }
    }
}
