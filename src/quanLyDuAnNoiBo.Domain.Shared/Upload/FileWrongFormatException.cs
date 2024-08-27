using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace quanLyDuAnNoiBo.Upload
{
    public class FileWrongFormatException : BusinessException
    {
        public FileWrongFormatException()
            : base(quanLyDuAnNoiBoDomainErrorCodes.FileWrongFormat)
        {
        }
    }
}
