using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.ThongTinChung.QuanLyNhanVien.Dtos
{
    public class GetAllInputHoSoNhanVienDto : PagedAndSortedResultRequestDto
    {
        public string? Keyword { get; set; }
    }
}
