using Microsoft.EntityFrameworkCore;
using quanLyDuAnNoiBo.DuAn;
using quanLyDuAnNoiBo.Entities;
using quanLyDuAnNoiBo.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace quanLyDuAnNoiBo.PmQuanLyDuAn
{
    public class EfPmQuanLyDuAnRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, QuanLyDuAn>, IPmQuanLyDuAnRepository
    {
        public EfPmQuanLyDuAnRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<QuanLyDuAn>> GetAllDuAnByPm(Guid? currentUserId, string? sorting, int skipCount, int MaxResultCount, string? keyword, string? khachHang, TrangThaiDuAnConsts? trangThai)
        {
            if (currentUserId == null)
            {
                return new List<QuanLyDuAn>();
            }

            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .Where(w => w.QuanLyDuAnId == currentUserId)
                .WhereIf(!string.IsNullOrEmpty(keyword), w => w.TenDuAn.Contains(keyword) || w.MaDuAn.Contains(keyword))
                .WhereIf(!string.IsNullOrEmpty(khachHang), w => w.KhachHang.Contains(khachHang))
                .WhereIf(trangThai != null, w => w.TrangThai == trangThai)
                .Skip(skipCount)
                .Take(MaxResultCount)
                .OrderByDescending(o => o.CreationTime)
                .ToListAsync();

            return query;
        }

        public Task<QuanLyDuAn> GetDuAnByPmId(Guid? currentUserId, Guid duAnId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<QuanLyDuAn>> GetListDuAnByInput(Guid PMId, string? keyword, DateTime? startTime, DateTime? endTime, string? sorting, int skipCount, int maxResultCount)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .Where(w => w.QuanLyDuAnId == PMId)
                .WhereIf(!string.IsNullOrEmpty(keyword), w => w.TenDuAn.Contains(keyword))
                .WhereIf(startTime != null, w => w.CreationTime >= startTime)
                .WhereIf(endTime != null, w => w.CreationTime <= endTime)
                .Skip(skipCount)
                .Take(maxResultCount)
                .OrderByDescending(o => o.CreationTime)
                .ToListAsync();

            return query;
        }
    }
}
