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
    public class EfCoreModuleDuAnRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, ModuleDuAn, Guid>, IModuleDuAnRepository
    {
        public EfCoreModuleDuAnRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<bool> CheckExistModule(string? tenModule, Guid? duAnId)
        {
            var dbSet = await GetDbSetAsync();
            // Check Exist Module with the same name in the same sprint
            var query = await dbSet.Where(w => w.TenModule.Equals(tenModule) && w.DuAnId == duAnId).FirstOrDefaultAsync();
            return query != null;
        }

        public async Task<ModuleDuAn> GetModuleById(Guid moduleId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.Id == moduleId).FirstOrDefaultAsync();
        }

        public async Task<List<ModuleDuAn>> GetModuleDuAnAsync(Guid duAnId)
        {
            var query = await GetDbSetAsync();
            return await query.Where(w => w.DuAnId == duAnId).ToListAsync();
        }
    }
}
