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

namespace quanLyDuAnNoiBo.DanhMuc
{
    public class EfCoreDanTocRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, DanToc, Guid>, IDanTocRepository
    {
        public EfCoreDanTocRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        public async Task<List<DanToc>> GetAll()
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.ToListAsync();
            return query;
        }
    }
}
