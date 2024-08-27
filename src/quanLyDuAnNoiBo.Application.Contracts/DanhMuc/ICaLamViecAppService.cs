using quanLyDuAnNoiBo.DanhMuc.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface ICaLamViecAppService
    {
        Task<bool> CreateCaLamViec(CaLamViecDto input);
        Task<bool> DeleteCaLamViec(Guid id);
        Task<PagedResultDto<CaLamViecDto>> GetAllAsync(GetAllInputCaLamViecDto input);
        Task<CaLamViecDto> GetCaLamViecById(Guid id);
        Task<bool> UpdateCaLamViec(CaLamViecDto input);
        Task<List<CaLamViecDto>> GetAllCaLamViec();
    }
}
