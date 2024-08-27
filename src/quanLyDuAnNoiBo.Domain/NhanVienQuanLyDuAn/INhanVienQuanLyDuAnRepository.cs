using quanLyDuAnNoiBo.DuAn;
using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.NhanVienQuanLyDuAn
{
    public interface INhanVienQuanLyDuAnRepository : IRepository<QuanLyDuAn>
    {
        Task<List<QuanLyDuAn>> GetAllDuAnByUserId(Guid userId, string? sorting, int skipCount, int MaxResultCount, string? keyword, string? khachHang, TrangThaiDuAnConsts? trangThai);
    }
}
