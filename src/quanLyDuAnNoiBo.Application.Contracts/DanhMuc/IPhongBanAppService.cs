using quanLyDuAnNoiBo.DanhMuc.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface IPhongBanAppService
    {
        Task<bool> CreatePhongBan(PhongBanDto input);
        Task<bool> UpdatePhongBan(PhongBanDto input);
        Task<bool> DeletePhongBan(Guid id);
        Task<PhongBanDto?> GetPhongBanById(Guid id);
        Task<PagedResultDto<PhongBanDto>> GetAllPhongBan(GetAllInputPhongBanDto input);
    }
}
