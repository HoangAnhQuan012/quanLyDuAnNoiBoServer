using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.CongViec
{
    public interface ICongViecRepository : IRepository<Entities.CongViec>
    {
        Task<Entities.CongViec> GetCongViecById(Guid? congViecId);
        Task<List<Entities.CongViec>> GetCongViecsByChamCongId(Guid chamCongId);
        Task<double> GetThoiGianCVByChamCongId(Guid chamCongId);
        Task<Entities.CongViec> GetCongViecBySubtaskIdAndNgayBaoCao(Guid subtaskId, DateTime ngayBaoCao);
        Task<List<Entities.CongViec>> GetCongViecsBySubtaskId(Guid subtaskId);
    }
}
