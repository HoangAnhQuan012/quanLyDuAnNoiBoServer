using Microsoft.EntityFrameworkCore;
using quanLyDuAnNoiBo.Entities;
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
    public class EfCoreTaiLieuDinhKemCongViecRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, TaiLieuDinhKemCongViec>, ITaiLieuDinhKemCongViecRepository
    {
        public EfCoreTaiLieuDinhKemCongViecRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<TaiLieuDinhKemCongViec>> GetTaiLieuDinhKemCongViecByCongViecId(Guid? congViecId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.CongViecId == congViecId).ToListAsync();
        }
    }
}
