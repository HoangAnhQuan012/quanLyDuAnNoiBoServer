using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface ICaLamViecRepository : IRepository<CalamViec>
    {
        Task<CalamViec?> GetAsync(Guid? id);
        Task<List<CalamViec>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword);
        Task<List<CalamViec>> GetAllListCaLamViec();
    }
}
