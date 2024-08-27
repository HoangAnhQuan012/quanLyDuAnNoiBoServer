using quanLyDuAnNoiBo.DanhMuc.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface ILoaiHopDongAppService
    {
        Task<bool> CreateLoaiHopDong(LoaiHopDongDto input);
        Task<bool> UpdateLoaiHopDong(LoaiHopDongDto input);
        Task<bool> DeleteLoaiHopDong(Guid id);
        Task<LoaiHopDongDto?> GetLoaiHopDongById(Guid id);
        Task<List<LoaiHopDongDto>> GetAllLoaiHopDong();
        Task<PagedResultDto<LoaiHopDongDto>> GetAllAsync(GetAllInputLoaiHopDongDto input);
    }
}
