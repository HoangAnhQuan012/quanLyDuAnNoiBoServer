using quanLyDuAnNoiBo.BodQuanLyDuAn;
using quanLyDuAnNoiBo.Entities;
using quanLyDuAnNoiBo.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace quanLyDuAnNoiBo.BoDQuanLyDuAn
{
    public class EfCoreSubTask_NhanVienRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, SubTask_NhanVien, Guid>, ISubTask_NhanVienRepository
    {
        public EfCoreSubTask_NhanVienRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
