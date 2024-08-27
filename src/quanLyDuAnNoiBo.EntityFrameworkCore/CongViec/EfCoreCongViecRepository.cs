using Microsoft.EntityFrameworkCore;
using quanLyDuAnNoiBo.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace quanLyDuAnNoiBo.CongViec
{
    public class EfCoreCongViecRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, Entities.CongViec>, ICongViecRepository
    {
        public EfCoreCongViecRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Entities.CongViec> GetCongViecById(Guid? congViecId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.Id == congViecId).FirstOrDefaultAsync();
        }

        public async Task<Entities.CongViec> GetCongViecBySubtaskIdAndNgayBaoCao(Guid subtaskId, DateTime ngayBaoCao)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.SubtaskId == subtaskId && w.CreationTime.Date == ngayBaoCao.Date).FirstOrDefaultAsync();
        }

        public async Task<List<Entities.CongViec>> GetCongViecsByChamCongId(Guid chamCongId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.ChamCongId == chamCongId).ToListAsync();
        }

        public async Task<List<Entities.CongViec>> GetCongViecsBySubtaskId(Guid subtaskId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.SubtaskId == subtaskId).ToListAsync();
        }

        public async Task<double> GetThoiGianCVByChamCongId(Guid chamCongId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.ChamCongId == chamCongId).Select(e => e.SoGio).FirstOrDefaultAsync();
        }
    }
}
