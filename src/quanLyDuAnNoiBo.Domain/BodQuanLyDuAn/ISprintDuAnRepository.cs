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
    public interface ISprintDuAnRepository : IRepository<SprintDuAn>
    {
        Task<bool> CheckExistSprint(Guid? sprintId, string? tenSprint, Guid? duAnId);
        Task<SprintDuAn> GetSprintById(Guid? sprintId);
        Task<List<SprintDuAn>> GetListSprintNeedToBeAccept(Guid duAnId);
        Task<List<SprintDuAn>> GetAllSprintListByDuAnId(Guid duAnId);
        Task<List<SprintDuAn>> GetAllSprintListPheDuyetVaChuaPheDuyetByDuAnId(Guid duAnId);
        Task<string> GetSprintNameById(Guid sprintId);
    }
}
