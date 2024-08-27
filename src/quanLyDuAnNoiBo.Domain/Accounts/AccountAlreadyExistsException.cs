using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace quanLyDuAnNoiBo.Accounts
{
    public class AccountAlreadyExistsException : BusinessException
    {
        public AccountAlreadyExistsException() : base(quanLyDuAnNoiBoDomainErrorCodes.AccountAlreadyExists) { }
    }
}
