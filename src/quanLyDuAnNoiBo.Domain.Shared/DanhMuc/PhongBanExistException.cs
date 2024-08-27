using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public class PhongBanExistException : BusinessException
    {
        public PhongBanExistException() : base(quanLyDuAnNoiBoDomainErrorCodes.PhongBanExist) 
        { }
    }
}
