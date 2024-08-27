using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface ILoaiHopDongRepository : IRepository<LoaiHopDong>
    {
        Task<LoaiHopDong?> GetAsync(Guid? id);
        Task<List<LoaiHopDong>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword);
        Task<List<LoaiHopDong>> GetAllListLoaiHopDong();
    }
}
