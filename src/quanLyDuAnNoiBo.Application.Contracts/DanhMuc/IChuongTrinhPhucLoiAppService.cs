using quanLyDuAnNoiBo.DanhMuc.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface IChuongTrinhPhucLoiAppService
    {
        Task<bool> CreateChuongTrinhPhucLoi(ChuongTrinhPhucLoiDto input);
        Task<bool> DeleteChuongTrinhPhucLoi(Guid id);
        Task<PagedResultDto<ChuongTrinhPhucLoiDto>> GetAllAsync(GetAllInputChuongTrinhPhucLoiDto input);
        Task<ChuongTrinhPhucLoiDto> GetChuongTrinhPhucLoiById(Guid id);
        Task<bool> UpdateChuongTrinhPhucLoi(ChuongTrinhPhucLoiDto input);
        Task<List<ChuongTrinhPhucLoiDto>> GetAllChuongTrinhPhucLoi();
    }
}
