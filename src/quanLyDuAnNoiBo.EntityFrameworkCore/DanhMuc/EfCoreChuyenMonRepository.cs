using Microsoft.EntityFrameworkCore;
using quanLyDuAnNoiBo.Entities;
using quanLyDuAnNoiBo.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public class EfCoreChuyenMonRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, ChuyenMon, Guid>, IChuyenMonRepository
    {
        public EfCoreChuyenMonRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        { }

        public async Task<List<ChuyenMon>> GetAllListChuyenMon()
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.ToListAsync();
            return query;
        }

        public async Task<ChuyenMon?> GetAsync(Guid? id)
        {
            var dbSet =  await GetDbSetAsync();
            var query = await dbSet.Where(w => w.Id.Equals(id)).FirstOrDefaultAsync();
            return query;
        }

        public async Task<long> GetCountAsync(string? keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
            }

            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .WhereIf(!string.IsNullOrEmpty(keyword), w => w.TenChuyenMon.Contains(keyword)).OrderByDescending(o => o.CreationTime).CountAsync();
            return query;
        }

        public async Task<List<ChuyenMon>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
            }

            Expression<Func<ChuyenMon, object>> orderBy = e => e.CreationTime;

            if (!string.IsNullOrEmpty(sorting))
            {
                orderBy = e => e.TenChuyenMon;
            }
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .WhereIf(!string.IsNullOrEmpty(keyword), w => w.TenChuyenMon.Contains(keyword))
                .Skip(skipCount)
                .Take(MaxResultCount)
                .OrderByDescending(orderBy)
                .ToListAsync();
            return query;
        }
    }
}
