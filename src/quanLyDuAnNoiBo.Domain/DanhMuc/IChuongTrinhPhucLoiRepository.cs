﻿using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public interface IChuongTrinhPhucLoiRepository : IRepository<ChuongTrinhPhucLoi>
    {
        Task<ChuongTrinhPhucLoi?> GetAsync(Guid? id);
        Task<List<ChuongTrinhPhucLoi>> GetListAsync(string? sorting, int skipCount, int MaxResultCount, string? keyword);
        Task<List<ChuongTrinhPhucLoi>> GetAllListChuongTrinhPhucLoi();
    }
}
