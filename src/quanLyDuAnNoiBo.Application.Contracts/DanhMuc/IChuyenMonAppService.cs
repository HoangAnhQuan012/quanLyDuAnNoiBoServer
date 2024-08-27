using quanLyDuAnNoiBo.DanhMuc.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface IChuyenMonAppService
    {
        Task<bool> CreateChuyenMon(ChuyenMonDto input);
        Task<bool> UpdateChuyenMon(ChuyenMonDto input);
        Task<bool> DeleteChuyenMon(Guid id);
        Task<ChuyenMonDto?> GetChuyenMonById(Guid id);
        Task<PagedResultDto<ChuyenMonDto>> GetAllChuyenMon(GetAllInputChuyenMonDto input);
    }
}
