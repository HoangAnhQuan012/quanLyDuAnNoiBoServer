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
    public class EfCorePhongBanRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, PhongBan, Guid>, IPhongBanRepository
    {
        public EfCorePhongBanRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<bool> CheckExist(string? maPhongBan)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.MaPhongBan == maPhongBan).FirstOrDefaultAsync();
            return query != null;
        }

        public async Task<List<PhongBan>> GetAllListPhongBan()
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.ToListAsync();
            return query;
        }

        public async Task<PhongBan?> GetAsync(Guid? id)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.Id.Equals(id)).FirstOrDefaultAsync();
            return query;
        }

        public async Task<List<PhongBan>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
            }

            Expression<Func<PhongBan, object>> orderBy = e => e.CreationTime;

            if (!string.IsNullOrEmpty(sorting))
            {
                orderBy = e => e.TenPhongBan;
            }
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .WhereIf(!string.IsNullOrEmpty(keyword), w => w.TenPhongBan.Contains(keyword) || w.MaPhongBan.Contains(keyword))
                .OrderBy(orderBy)
                .Skip(skipCount)
                .Take(MaxResultCount)
                .OrderByDescending(orderBy)
                .ToListAsync();
            return query;
        }
    }
}
