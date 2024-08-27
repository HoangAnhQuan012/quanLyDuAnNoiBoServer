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
    public class EfCoreSprintDuAnRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, SprintDuAn, Guid>, ISprintDuAnRepository
    {
        public EfCoreSprintDuAnRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<bool> CheckExistSprint(Guid? sprintId, string? tenSprint, Guid? duAnId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.AnyAsync(w => w.Id != sprintId && w.TenSprint == tenSprint && w.DuAnId == duAnId);
        }

        public async Task<List<SprintDuAn>> GetAllSprintListByDuAnId(Guid duAnId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.DuAnId == duAnId).ToListAsync();
        }

        public async Task<List<SprintDuAn>> GetAllSprintListPheDuyetVaChuaPheDuyetByDuAnId(Guid duAnId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.DuAnId == duAnId && (w.TrangThaiSprint == DuAn.TrangThaiSprintConsts.DaPheDuyet || w.TrangThaiSprint == DuAn.TrangThaiSprintConsts.DaGuiPheDuyet)).ToListAsync();
        }

        public async Task<List<SprintDuAn>> GetListSprintNeedToBeAccept(Guid duAnId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.DuAnId == duAnId && w.TrangThaiSprint == DuAn.TrangThaiSprintConsts.DaGuiPheDuyet).ToListAsync();
        }

        public async Task<SprintDuAn> GetSprintById(Guid? sprintId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.Id == sprintId).FirstOrDefaultAsync();
        }

        public async Task<string> GetSprintNameById(Guid sprintId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.Id == sprintId).Select(s => s.TenSprint).FirstOrDefaultAsync();
        }
    }
}
