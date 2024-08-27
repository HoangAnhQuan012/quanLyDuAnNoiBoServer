using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace quanLyDuAnNoiBo.Accounts.Dtos
{
    public class AccountDto
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? FullName { get; set; }

        public string? Email { get; set; }
        public string? Password { get; set; }

        public ListResultDto<IdentityRoleDto> RoleNames { get; set; }

        public string? UserName { get; set; }

        public bool IsActive { get; set; }

        public string? TenChucDanh { get; set; }

        public Guid? ChucDanhId { get; set; }

        public int Bac { get; set; }

        public int TyLe { get; set; }
    }
}
