using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.NhanVienQuanLyDuAn.Dtos
{
    public class GetAllDuAnInputDto : PagedAndSortedResultRequestDto
    {
        public string? Keyword { get; set; }
        public string? KhachHang { get; set; }
        public TrangThaiDuAnConsts? TrangThai { get; set; }
    }
}
