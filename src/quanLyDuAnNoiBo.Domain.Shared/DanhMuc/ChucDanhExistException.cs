using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public class ChucDanhExistException : BusinessException
    {
        public ChucDanhExistException()
            : base(quanLyDuAnNoiBoDomainErrorCodes.MaChucDanhExist)
        {
        }
    }
}
