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

namespace quanLyDuAnNoiBo.NhanVienQuanLyDuAn
{
    public class EFCoreNhanVienQuanLyDuAn : EfCoreRepository<quanLyDuAnNoiBoDbContext, QuanLyDuAn>, INhanVienQuanLyDuAnRepository
    {
        public EFCoreNhanVienQuanLyDuAn(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<QuanLyDuAn>> GetAllDuAnByUserId(Guid userId, string? sorting, int skipCount, int MaxResultCount, string? keyword, string? khachHang, TrangThaiDuAnConsts? trangThai)
        {
            if (userId == null)
            {
                return new List<QuanLyDuAn>();
            }

            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                //.Where(w => w.Nha == userId)
                .WhereIf(!string.IsNullOrEmpty(keyword), w => w.TenDuAn.Contains(keyword) || w.MaDuAn.Contains(keyword))
                .WhereIf(!string.IsNullOrEmpty(khachHang), w => w.KhachHang.Contains(khachHang))
                .WhereIf(trangThai != null, w => w.TrangThai == trangThai)
                .Skip(skipCount)
                .Take(MaxResultCount)
                .OrderByDescending(o => o.CreationTime)
                .ToListAsync();

            return query;
        }
    }
}
