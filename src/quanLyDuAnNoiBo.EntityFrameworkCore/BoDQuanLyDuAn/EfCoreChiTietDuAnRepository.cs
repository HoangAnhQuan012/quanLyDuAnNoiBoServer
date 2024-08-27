using Microsoft.EntityFrameworkCore;
using quanLyDuAnNoiBo.BodQuanLyDuAn;
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
    public class EfCoreChiTietDuAnRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, ChiTietDuAn, Guid>, IChiTietDuAnRepository
    {
        public EfCoreChiTietDuAnRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<ChiTietDuAn> GetChiTietDuAnAsync(Guid duAnId)
        {
            var query = await GetDbSetAsync();
            return await query.Where(w => w.DuAnId == duAnId).FirstOrDefaultAsync();
        }
    }
}
