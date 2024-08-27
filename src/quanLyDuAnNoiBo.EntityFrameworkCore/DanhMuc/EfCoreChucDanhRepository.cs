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
    public class EfCoreChucDanhRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, ChucDanh, Guid>, IChucDanhRepository
    {
        public EfCoreChucDanhRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        public async Task<List<ChucDanh>> GetAllListChucDanh()
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.ToListAsync();
            return query;
        }

        public async Task<ChucDanh?> GetAsync(Guid? id)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.Id.Equals(id)).FirstOrDefaultAsync();
            return query;
        }

        public async Task<List<ChucDanh>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
            }

            Expression<Func<ChucDanh, object>> orderBy = e => e.CreationTime;

            if (!string.IsNullOrEmpty(sorting))
            {
                orderBy = e => e.TenChucDanh;
            }
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .WhereIf(!string.IsNullOrEmpty(keyword), w => w.TenChucDanh.Contains(keyword) || w.MaChucDanh.Contains(keyword))
                .OrderBy(orderBy)
                .Skip(skipCount)
                .Take(MaxResultCount)
                .OrderByDescending(orderBy)
                .ToListAsync();
            return query;
        }

        public async Task<ChucDanh> GetChucDanhPM()
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.MaChucDanh.Equals("PM")).FirstOrDefaultAsync();
            return query;
        }

        public async Task<bool> CheckExist(string? maChucDanh)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.MaChucDanh == maChucDanh).FirstOrDefaultAsync();
            return query != null;
        }
    }
}
