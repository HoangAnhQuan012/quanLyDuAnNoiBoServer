using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace quanLyDuAnNoiBo.Accounts.Dtos
{
    public class CreateOrEditAccountDto
    {
        public Guid? Id { get; set; }
        [MaxLength(AccountConsts.MaxNameLength)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(AccountConsts.MaxNameLength)]
        public string SurName { get; set; } = string.Empty;

        [MaxLength(AccountConsts.MaxEmailLength)]
        [RegularExpression(AccountConsts.RegexEmail, ErrorMessage = AccountConsts.ErrorEmailMessage)]
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;

        public string[]? RoleNames { get; set; }

        public string? UserName { get; set; }

        public bool IsActive { get; set; } = true;

        public Guid ChucDanhId { get; set; }
    }
}
