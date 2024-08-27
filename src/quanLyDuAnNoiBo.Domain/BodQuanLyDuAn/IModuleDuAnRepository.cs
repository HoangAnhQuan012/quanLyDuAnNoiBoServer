using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.BodQuanLyDuAn
{
    public interface IModuleDuAnRepository : IRepository<ModuleDuAn>
    {
        Task<List<ModuleDuAn>> GetModuleDuAnAsync(Guid duAnId);
        Task<bool> CheckExistModule(string? tenModule, Guid? duAnId);
        Task<ModuleDuAn> GetModuleById(Guid moduleId);
    }
}
