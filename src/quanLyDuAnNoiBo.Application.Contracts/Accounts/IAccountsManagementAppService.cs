using quanLyDuAnNoiBo.Accounts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.Accounts
{
    public interface IAccountsManagementAppService
    {
        Task<bool> CreateAccountAsync(CreateOrEditAccountDto input);
        Task<bool> CheckExistAccount(CheckAccountInput input);
        Task<PagedResultDto<AccountDto>> GetAllAccountAsync(GetAllInputAccountDto input);
        Task<bool> UpdateAccountAsync(CreateOrEditAccountDto input);
    }
}
