using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.CongViec
{
    public interface ITaiLieuDinhKemCongViecRepository :IRepository<TaiLieuDinhKemCongViec>
    {
        Task<List<TaiLieuDinhKemCongViec>> GetTaiLieuDinhKemCongViecByCongViecId(Guid? congViecId);
    }
}
