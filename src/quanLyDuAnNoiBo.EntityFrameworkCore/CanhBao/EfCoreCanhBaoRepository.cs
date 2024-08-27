using Microsoft.EntityFrameworkCore;
using quanLyDuAnNoiBo.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace quanLyDuAnNoiBo.CanhBao
{
    public class EfCoreCanhBaoRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, Entities.CanhBao>, ICanhBaoRepository
    {
        public EfCoreCanhBaoRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Entities.CanhBao> GetCanhBaoBySubtaskId(Guid subtaskId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.SubtaskId == subtaskId).FirstOrDefaultAsync();
        }
    }
}
