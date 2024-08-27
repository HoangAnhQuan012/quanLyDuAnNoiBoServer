using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface IChucDanhRepository : IRepository<ChucDanh>
    {
        Task<ChucDanh?> GetAsync(Guid? id);
        Task<List<ChucDanh>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword);
        Task<List<ChucDanh>> GetAllListChucDanh();
        Task<ChucDanh> GetChucDanhPM();
        Task<bool> CheckExist(string? maChucDanh);
    }
}
