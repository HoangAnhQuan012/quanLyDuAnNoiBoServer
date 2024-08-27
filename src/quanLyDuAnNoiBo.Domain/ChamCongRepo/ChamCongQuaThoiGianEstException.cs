using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace quanLyDuAnNoiBo.ChamCongRepo
{
    public class ChamCongQuaThoiGianEstException : BusinessException
    {
        public ChamCongQuaThoiGianEstException() : base(quanLyDuAnNoiBoDomainErrorCodes.ChamCongQuaThoiGianEst) { }
    }
}
