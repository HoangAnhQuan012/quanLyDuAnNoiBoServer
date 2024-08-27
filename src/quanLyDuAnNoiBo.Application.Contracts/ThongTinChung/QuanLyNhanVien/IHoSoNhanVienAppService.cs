using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using quanLyDuAnNoiBo.Global.Dtos;
using quanLyDuAnNoiBo.ThongTinChung.QuanLyNhanVien.Dtos;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.ThongTinChung.QuanLyNhanVien
{
    public interface IHoSoNhanVienAppService
    {
        Task<bool> CreateHoSoNhanVienAsync(HoSoNhanVienDto input);
        Task<bool> UpdateHoSoNhanVienAsync(HoSoNhanVienDto input);
        Task<bool> DeleteHoSoNhanVienAsync(Guid id);
        Task<HoSoNhanVienDto> GetHoSoNhanVienByIdAsync(Guid id);
        Task<List<HoSoNhanVienDto>> GetAllHoSoNhanVienAsync();
        Task<PagedResultDto<HoSoNhanVienDto>> GetAllAsync(GetAllInputHoSoNhanVienDto input);
        Task<List<LookupTableDto>> GetAllUserAsync();
    }
}
