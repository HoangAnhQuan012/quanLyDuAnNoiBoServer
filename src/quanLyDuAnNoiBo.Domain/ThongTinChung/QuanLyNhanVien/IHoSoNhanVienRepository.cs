using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.ThongTinChung.QuanLyNhanVien
{
    public interface IHoSoNhanVienRepository : IRepository<Hs_HoSoNhanVien>
    {
        Task<Hs_HoSoNhanVien?> GetAsync(Guid? id);
        Task<List<Hs_HoSoNhanVien>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword);
        Task<List<Hs_HoSoNhanVien>> GetAllListHoSoNhanVien();
        Task<bool> CheckExistHoSoNhanVien(string maNhanVien, Guid id);
    }
}
