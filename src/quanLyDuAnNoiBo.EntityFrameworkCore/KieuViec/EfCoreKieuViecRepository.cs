using Microsoft.EntityFrameworkCore;
using quanLyDuAnNoiBo.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace quanLyDuAnNoiBo.KieuViec
{
    public class EfCoreKieuViecRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, Entities.KieuViec>, IKieuViecRepository
    {
        public EfCoreKieuViecRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<string> GetKieuViecNameById(Guid kieuViecId)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.Id == kieuViecId).FirstOrDefaultAsync();
            return query.TenKieuViec;
        }
    }
}
