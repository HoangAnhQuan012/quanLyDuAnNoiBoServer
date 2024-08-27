using Microsoft.EntityFrameworkCore;
using quanLyDuAnNoiBo.ChamCongRepo;
using quanLyDuAnNoiBo.Entities;
using quanLyDuAnNoiBo.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace quanLyDuAnNoiBo.ChamCong
{
    public class EfCoreChamCongRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, Entities.ChamCong>, IChamCongRepository
    {
        public EfCoreChamCongRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Entities.ChamCong>> GetAllChamCongListByDuAnId(Guid duAnId, Guid nhanVienId, int skipCount, int maxResultCount, string? sorting)
        {
            Expression<Func<Entities.ChamCong, object>> orderBy = e => e.CreationTime;

            if (!string.IsNullOrEmpty(sorting))
            {
                orderBy = e => e.NgayChamCong;
            }
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .Where(w => w.DuAnId == duAnId && w.NhanVienId == nhanVienId)
                .OrderBy(orderBy)
                .Skip(skipCount)
                .Take(maxResultCount)
                .OrderByDescending(orderBy)
                .ToListAsync();
            return query;
        }

        //public async Task<List<Entities.ChamCong>> GetAllChamCongListByDuAnId(Guid duAnId)
        //{
        //    var dbSet = await GetDbSetAsync();
        //    return await dbSet.Where(w => w.DuAnId == duAnId).ToListAsync();
        //}

        public async Task<Entities.ChamCong> GetChamCongById(Guid? chamCongId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.Id == chamCongId).FirstOrDefaultAsync();
        }

        public async Task<Entities.ChamCong> GetChamCongByNhanVienId(Guid NhanVienId, DateTime NgayChamCong)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.NhanVienId == NhanVienId && w.NgayChamCong == NgayChamCong).FirstOrDefaultAsync();
        }

        public async Task<Entities.ChamCong> GetChamCongByNhanVienId(Guid nhanVienId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.NhanVienId == nhanVienId).FirstOrDefaultAsync();
        }

        public async Task<List<Entities.ChamCong>> GetListChamCongByDuAnId(Guid duAnId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.DuAnId == duAnId).ToListAsync();
        }

        public async Task<List<Entities.ChamCong>> GetListChamCongByNhanVienId(Guid nhanVienId, DateTime ngayBaoCao)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.NhanVienId == nhanVienId
            &&
            w.NgayChamCong.Date == ngayBaoCao.Date
            ).ToListAsync();
            return query;
        }

        public async Task<List<Entities.ChamCong>> GetListChamCongByTheoKhoang(DateTime startDate, DateTime endDate)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.NgayChamCong >= startDate && w.NgayChamCong <= endDate).ToListAsync();
        }

        public async Task<List<Entities.ChamCong>> GetListChamCongChoPheDuyet()
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.TrangThaiChamCong == TrangThaiChamCongConsts.ChoDuyet).ToListAsync();
        }

        public async Task<List<Entities.ChamCong>> GetListChamCongDuAn(Guid duAnId, DateTime startTime, DateTime endTime, int skipCount, int maxResultCount)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(w => w.DuAnId == duAnId && w.NgayChamCong >= startTime && w.NgayChamCong <= endTime)
                .OrderByDescending(e => e.CreationTime)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
