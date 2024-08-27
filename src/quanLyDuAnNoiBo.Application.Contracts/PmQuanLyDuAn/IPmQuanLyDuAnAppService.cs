using quanLyDuAnNoiBo.PmQuanLyDuAn.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.PmQuanLyDuAn
{
    public interface IPmQuanLyDuAnAppService
    {
        Task<PagedResultDto<GetAllDuAnByPmDto>> GetAllDuAnByPm(GetAllDuAnByPmInputDto input);
        Task<GetDuAnByPmIdDto> GetDuAnByPmId(Guid duAnId);
    }
}
