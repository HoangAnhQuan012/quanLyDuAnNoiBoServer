using quanLyDuAnNoiBo.DuAn;
using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.BodQuanLyDuAn
{
    public interface ISubTaskRepository : IRepository<SubTask>
    {
        Task<bool> CheckExistSubTask(string? tenSubTask, Guid? sprintId);
        Task<SubTask> GetSubTaskById(Guid subTaskId);
        Task<List<SubTask>> GetAllListSubTaskByModuleId(Guid moduleId);
        Task<List<SubTask>> GetAllSubTasks();
        Task<SubTask> GetSubtaskBySprintId(Guid sprintId);
        Task<List<SubTask>> GetSubtaksBySprintId(Guid sprintId);
        Task<List<SubTask>> GetListSubTaskByNhanVienId(Guid? nhanVienId);
        Task<List<SubTask>> GetSubtaskBySprintIdAndTaskId(Guid sprintId, Guid taskId);
        Task<string> GetSubtasknameById(Guid subTaskId);
        Task<TrangThaiSubtaskConsts?> GetTrangThaiById(Guid subTaskId);
        Task<List<SubTask>> GetSubtaskByUserId(Guid nhanVienId);
    }
}
