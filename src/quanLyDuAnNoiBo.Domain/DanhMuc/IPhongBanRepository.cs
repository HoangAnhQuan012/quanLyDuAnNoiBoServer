using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface IPhongBanRepository : IRepository<PhongBan>
    {
        Task<PhongBan?> GetAsync(Guid? id);
        Task<List<PhongBan>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword);
        Task<List<PhongBan>> GetAllListPhongBan();
        Task<bool> CheckExist(string? maPhongBan);
    }
}
