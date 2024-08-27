using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.PmQuanLyDuAn.Dtos
{
    public class GetAllDuAnByPmInputDto : PagedAndSortedResultRequestDto
    {
        public string? Keyword { get; set; }
        public string? KhachHang { get; set; }
        public TrangThaiDuAnConsts? TrangThai { get; set; }
    }
}
