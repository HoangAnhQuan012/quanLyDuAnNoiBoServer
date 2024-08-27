using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.Global.Dtos
{
    public class LookupTableDto : LookupTableDto<Guid>
    {
        public LookupTableDto()
        {
        }
    }

    public class LookupTableDto<Guid>
    {
        public Guid? Id { get; set; }

        public string? DisplayName { get; set; }
    }
}
