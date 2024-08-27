using quanLyDuAnNoiBo.DuAn;
using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.PmQuanLyDuAn
{
    public interface IPmQuanLyDuAnRepository : IRepository<QuanLyDuAn>
    {
        Task<List<QuanLyDuAn>> GetAllDuAnByPm(Guid? currentUserId, string? sorting, int skipCount, int MaxResultCount, string? keyword, string? khachHang, TrangThaiDuAnConsts? trangThai);
        Task<QuanLyDuAn> GetDuAnByPmId(Guid? currentUserId, Guid duAnId);
        Task<List<QuanLyDuAn>> GetListDuAnByInput(Guid PMId, string? keyword, DateTime? startTime, DateTime? endTime, string? sorting, int skipCount, int maxResultCount);
    }
}
