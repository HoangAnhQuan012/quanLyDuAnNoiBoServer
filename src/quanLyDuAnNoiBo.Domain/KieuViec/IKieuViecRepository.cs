﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.KieuViec
{
    public interface IKieuViecRepository : IRepository<Entities.KieuViec>
    {
        Task<string> GetKieuViecNameById(Guid kieuViecId);
    }
}
