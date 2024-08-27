using Microsoft.EntityFrameworkCore;
using quanLyDuAnNoiBo.BodQuanLyDuAn;
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

namespace quanLyDuAnNoiBo.BoDQuanLyDuAn
{
    public class EfCoreQuanLyDuAnRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, QuanLyDuAn, Guid>, IQuanLyDuAnRepository
    {
        public EfCoreQuanLyDuAnRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<QuanLyDuAn> GetDuAnAsync(Guid id)
        {
            var query = await GetDbSetAsync();
            return await query.Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<QuanLyDuAn>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword, string? khachHang, TrangThaiDuAnConsts? trangThai, Guid? quanLyDuAnId)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
            }
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .WhereIf(!string.IsNullOrEmpty(keyword), w => w.TenDuAn.Contains(keyword) || w.MaDuAn.Contains(keyword))
                .WhereIf(!string.IsNullOrEmpty(khachHang), w => w.KhachHang.Contains(khachHang))
                .WhereIf(trangThai != null, w => w.TrangThai == trangThai)
                .WhereIf(quanLyDuAnId != null, w => w.QuanLyDuAnId == quanLyDuAnId)
                .Skip(skipCount)
                .Take(MaxResultCount)
                .OrderByDescending(o => o.CreationTime)
                .ToListAsync();

            return query;
        }

        public async Task<bool> CheckExistDuAn(string? maDuAn, Guid? id)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.MaDuAn.Equals(maDuAn) && w.Id != id).FirstOrDefaultAsync();
            return query != null;
        }

        public async Task<List<string>> GetAllKhachHang()
        {
            var query = await GetDbSetAsync();
            var khachHang = await query.Select(s => s.KhachHang).Distinct().ToListAsync();
            return khachHang;
        }
    }
}
