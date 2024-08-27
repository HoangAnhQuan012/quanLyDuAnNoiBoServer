using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace quanLyDuAnNoiBo.PmQuanLyDuAn
{
    public class SubTaskAlreadyExistException : BusinessException
    {
        public SubTaskAlreadyExistException() : base(quanLyDuAnNoiBoDomainErrorCodes.SubTaskAlreadyExists) { }
    }
}
