using quanLyDuAnNoiBo.DanhMuc.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface IChucDanhAppService
    {
        Task<bool> CreateChucDanh(ChucDanhDto input);
        Task<bool> UpdateChucDanh(ChucDanhDto input);
        Task<bool> DeleteChucDanh(Guid id);
        Task<ChucDanhDto?> GetChucDanhById(Guid id);
        Task<List<ChucDanhDto>> GetAllChucDanh();
        Task<PagedResultDto<ChucDanhDto>> GetAllAsync(GetAllInputChucDanhDto input);
    }
}
