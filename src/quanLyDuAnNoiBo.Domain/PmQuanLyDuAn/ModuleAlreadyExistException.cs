using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace quanLyDuAnNoiBo.PmQuanLyDuAn
{
    public class ModuleAlreadyExistException : BusinessException
    {
        public ModuleAlreadyExistException() : base(quanLyDuAnNoiBoDomainErrorCodes.ModuleAlreadyExists) { }
    }
}
