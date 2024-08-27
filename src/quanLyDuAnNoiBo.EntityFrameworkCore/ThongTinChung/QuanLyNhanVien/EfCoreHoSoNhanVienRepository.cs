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

namespace quanLyDuAnNoiBo.ThongTinChung.QuanLyNhanVien
{
    public class EfCoreHoSoNhanVienRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, Hs_HoSoNhanVien, Guid>, IHoSoNhanVienRepository
    {
        public EfCoreHoSoNhanVienRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<bool> CheckExistHoSoNhanVien(string maNhanVien, Guid id)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.MaNhanVien.Equals(maNhanVien) && w.Id != id).FirstOrDefaultAsync();
            if (query != null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Hs_HoSoNhanVien>> GetAllListHoSoNhanVien()
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.ToListAsync();
            return query;
        }

        public async Task<Hs_HoSoNhanVien?> GetAsync(Guid? id)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.Id.Equals(id)).FirstOrDefaultAsync();
            return query;
        }

        public async Task<List<Hs_HoSoNhanVien>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
            }

            Expression<Func<Hs_HoSoNhanVien, object>> orderBy = e => e.CreationTime;

            if (!string.IsNullOrEmpty(sorting))
            {
                orderBy = e => e.HoTen;
            }
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .WhereIf(!string.IsNullOrEmpty(keyword), w => w.HoTen.Contains(keyword))
                .OrderBy(orderBy)
                .Skip(skipCount)
                .Take(MaxResultCount)
                .OrderByDescending(orderBy)
                .ToListAsync();
            return query;
        }
    }
}
