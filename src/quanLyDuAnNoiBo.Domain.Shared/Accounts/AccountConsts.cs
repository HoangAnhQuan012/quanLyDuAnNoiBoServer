using System;
using System.Collections.Generic;
using System.Text;

namespace quanLyDuAnNoiBo.Accounts
{
    public static class AccountConsts
    {
        public const int MaxNameLength = 128;
        public const int MaxEmailLength = 256;
        public const int MinPasswordLength = 7;
        public const int MaxPasswordLength = 128;

        public const string RegexEmail = @"^[\w-\\.]+@([\w-]+\.)+[\w-]{2,4}$";
        public const string ErrorEmailMessage = "Định dạng email không hợp lệ";
    }
}
