using quanLyDuAnNoiBo.DuAn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.BoDQuanLyDuAn.Dtos
{
    public class GetAllDuAnInputDto : PagedAndSortedResultRequestDto
    {
        public string? Keyword { get; set; }
        public string? KhachHang { get; set; }
        public TrangThaiDuAnConsts? TrangThai { get; set; }
        public Guid? QuanLyDuAnId { get; set; }
    }
}
