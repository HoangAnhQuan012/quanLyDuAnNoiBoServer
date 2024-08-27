using Microsoft.EntityFrameworkCore;
using quanLyDuAnNoiBo.BodQuanLyDuAn;
using quanLyDuAnNoiBo.DuAn;
using quanLyDuAnNoiBo.Entities;
using quanLyDuAnNoiBo.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace quanLyDuAnNoiBo.BoDQuanLyDuAn
{
    public class EfCoreSubTaskRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, SubTask, Guid>, ISubTaskRepository
    {
        public EfCoreSubTaskRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<bool> CheckExistSubTask(string? tenSubTask, Guid? sprintId)
        {
            var dbSet = await GetDbSetAsync();
            // Check Exist SubTask with the same name in the same module and sprint
            var query = await dbSet.Where(w => w.TenSubTask.Equals(tenSubTask)).FirstOrDefaultAsync();
            return query != null;
        }

        public async Task<List<SubTask>> GetAllListSubTaskByModuleId(Guid moduleId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.ModuleId == moduleId).ToListAsync();
        }

        public async Task<List<SubTask>> GetAllSubTasks()
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.ToListAsync();
        }

        public async Task<List<SubTask>> GetListSubTaskByNhanVienId(Guid? nhanVienId)
        {
            var dbSet = await GetDbSetAsync();

            // Fetch all records without filtering
            var allSubTasks = await dbSet.ToListAsync();

            // Perform the filtering in memory
            var filteredSubTasks = allSubTasks
                .Where(w => w.NhanSu.Split(',').Contains(nhanVienId.ToString()))
                .ToList();

            return filteredSubTasks;
        }

        public async Task<List<SubTask>> GetSubtaksBySprintId(Guid sprintId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.SprintId == sprintId).ToListAsync();
        }

        public async Task<SubTask> GetSubTaskById(Guid subTaskId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.Id == subTaskId).FirstOrDefaultAsync();
        }

        public async Task<SubTask> GetSubtaskBySprintId(Guid sprintId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.SprintId == sprintId).FirstOrDefaultAsync();
        }

        public async Task<List<SubTask>> GetSubtaskBySprintIdAndTaskId(Guid sprintId, Guid taskId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(w => w.SprintId == sprintId && w.ModuleId == taskId).ToListAsync();
        }

        public async Task<List<SubTask>> GetSubtaskByUserId(Guid nhanVienId)
        {
            var dbSet = await GetDbSetAsync();
            var subTasks = await dbSet.ToListAsync();
            return subTasks.Where(w => w.NhanSu != null && w.NhanSu.Split(',').Contains(nhanVienId.ToString())).ToList();
        }

        public async Task<string> GetSubtasknameById(Guid subTaskId)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.Id == subTaskId).FirstOrDefaultAsync();
            return query.TenSubTask;
        }

        public async Task<TrangThaiSubtaskConsts?> GetTrangThaiById(Guid subTaskId)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.Id == subTaskId).FirstOrDefaultAsync();
            return query.TrangThaiSubtask;
        }
    }
}
