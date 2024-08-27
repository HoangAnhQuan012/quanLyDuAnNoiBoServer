using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.ChamCongRepo
{
    public interface IChamCongRepository : IRepository<Entities.ChamCong>
    {
        Task<List<Entities.ChamCong>> GetAllChamCongListByDuAnId(Guid duAnId, Guid nhanVienId, int skipCount, int maxResultCount, string? sorting);
        Task<Entities.ChamCong> GetChamCongById(Guid? chamCongId);
        Task<List<Entities.ChamCong>> GetListChamCongChoPheDuyet();
        Task<List<Entities.ChamCong>> GetListChamCongDuAn(Guid duAnId, DateTime startTime, DateTime endTime, int skipCount, int maxResultCount);
        Task<List<Entities.ChamCong>> GetListChamCongByNhanVienId(Guid nhanVienId, DateTime ngayBaoCao);
        Task<Entities.ChamCong> GetChamCongByNhanVienId(Guid NhanVienId, DateTime ngayChamCong);
        Task<List<Entities.ChamCong>> GetListChamCongByTheoKhoang(DateTime startDate, DateTime endDate);
        Task<Entities.ChamCong> GetChamCongByNhanVienId(Guid nhanVienId);
        Task<List<Entities.ChamCong>> GetListChamCongByDuAnId(Guid duAnId);
    }
}
