using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.Global.Dtos
{
    public class LookupTableIntDto : LookupTableIntDto<int>
    {
        public LookupTableIntDto()
        {
        }
    }

    public class LookupTableIntDto<TPrimaryKey>
    {
        public TPrimaryKey? Id { get; set; }

        public string? DisplayName { get; set; }
    }
}
