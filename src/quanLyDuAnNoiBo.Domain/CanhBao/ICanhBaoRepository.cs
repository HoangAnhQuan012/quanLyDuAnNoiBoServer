using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.CanhBao
{
    public interface ICanhBaoRepository : IRepository<Entities.CanhBao>
    {
        Task<Entities.CanhBao> GetCanhBaoBySubtaskId(Guid subtaskId);
    }
}
