using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface IChuyenMonRepository : IRepository<ChuyenMon>
    {
        Task<ChuyenMon?> GetAsync(Guid? id);
        Task<List<ChuyenMon>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword);
        Task<long> GetCountAsync(string? keyword);
        Task<List<ChuyenMon>> GetAllListChuyenMon();
    }
}
