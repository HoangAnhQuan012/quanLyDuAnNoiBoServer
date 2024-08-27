using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.Accounts.Dtos
{
    public class CheckAccountInput
    {
        public string Email { get; set; } = string.Empty;

        public Guid? AccountId { get; set; }
    }
}
