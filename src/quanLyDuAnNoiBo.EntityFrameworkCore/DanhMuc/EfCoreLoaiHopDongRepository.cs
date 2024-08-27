using Microsoft.EntityFrameworkCore;
using quanLyDuAnNoiBo.Entities;
using quanLyDuAnNoiBo.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public class EfCoreLoaiHopDongRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, LoaiHopDong, Guid>, ILoaiHopDongRepository
    {
        public EfCoreLoaiHopDongRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        public async Task<List<LoaiHopDong>> GetAllListLoaiHopDong()
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.ToListAsync();
            return query;
        }

        public async Task<LoaiHopDong?> GetAsync(Guid? id)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.Id.Equals(id)).FirstOrDefaultAsync();
            return query;
        }

        public async Task<List<LoaiHopDong>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
            }

            Expression<Func<LoaiHopDong, object>> orderBy = e => e.CreationTime;

            if (!string.IsNullOrEmpty(sorting))
            {
                orderBy = e => e.TenLoaiHopDong;
            }
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .WhereIf(!string.IsNullOrEmpty(keyword), w => w.TenLoaiHopDong.Contains(keyword) || w.MaLoaiHopDong.Contains(keyword))
                .OrderBy(orderBy)
                .Skip(skipCount)
                .Take(MaxResultCount)
                .OrderByDescending(orderBy)
                .ToListAsync();
            return query;
        }
    }
}
