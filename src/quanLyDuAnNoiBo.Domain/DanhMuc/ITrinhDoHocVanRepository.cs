using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface ITrinhDoHocVanRepository : IRepository<TrinhDoHocVan>
    {
        Task<TrinhDoHocVan?> GetAsync(Guid? id);
        Task<List<TrinhDoHocVan>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword);
        Task<List<TrinhDoHocVan>> GetAllListTrinhDoHocVan();
    }
}
