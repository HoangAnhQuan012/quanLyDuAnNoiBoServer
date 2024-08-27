using quanLyDuAnNoiBo.DanhMuc.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface ITrinhDoHocVanAppService
    {
        Task<bool> CreateTrinhDoHocVan(TrinhDoHocVanDto input);
        Task<bool> DeleteTrinhDoHocVan(Guid id);
        Task<PagedResultDto<TrinhDoHocVanDto>> GetAllAsync(GetAllInputTrinhDoHocVanDto input);
        Task<TrinhDoHocVanDto> GetTrinhDoHocVanById(Guid id);
        Task<bool> UpdateTrinhDoHocVan(TrinhDoHocVanDto input);
        Task<List<TrinhDoHocVanDto>> GetAllTrinhDoHocVan();
    }
}
