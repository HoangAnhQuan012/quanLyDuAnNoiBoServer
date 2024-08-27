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
    public interface IQuanLyDuAnRepository : IRepository<QuanLyDuAn>
    {
        Task<QuanLyDuAn> GetDuAnAsync(Guid id);
        Task<List<QuanLyDuAn>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword, string? khachHang, TrangThaiDuAnConsts? trangThai, Guid? quanLyDuAnId);
        Task<bool> CheckExistDuAn(string? maDuAn, Guid? id);
        Task<List<string>> GetAllKhachHang();
    }
}
