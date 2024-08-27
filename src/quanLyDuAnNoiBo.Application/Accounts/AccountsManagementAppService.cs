using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using quanLyDuAnNoiBo.Accounts.Dtos;
using quanLyDuAnNoiBo.DanhMuc;
using quanLyDuAnNoiBo.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace quanLyDuAnNoiBo.Accounts
{
    [Authorize(IdentityPermissions.Users.Default)]
    public class AccountsManagementAppService : quanLyDuAnNoiBoAppService, IAccountsManagementAppService
    {
        private readonly IdentityUserManager _userManagement;
        private readonly IAccountRepository _accountRepository;
        private readonly IChucDanhRepository _chucDanhRepository;
        private readonly IdentityUserAppService _identityUserAppService;
        private readonly string SERVICE_NAME = "AccountManagementAppService";
        public AccountsManagementAppService(
            IAccountRepository accountRepository,
            IdentityUserManager userManagement,
            IChucDanhRepository chucDanhRepository,
            IdentityUserAppService identityUserAppService
            ) 
        {
            this._accountRepository = accountRepository;
            this._userManagement = userManagement;
            this._chucDanhRepository = chucDanhRepository;
            this._identityUserAppService = identityUserAppService;
        }

        public async Task<bool> CreateAccountAsync(CreateOrEditAccountDto input)
        {
            try
            {
                Logger.LogInformation("Start {service}.CreateAsync", SERVICE_NAME);
                var checkExist = await this.CheckExistAccount(new CheckAccountInput()
                {
                    Email = input.Email,
                    AccountId = input.Id
                });

                if (checkExist)
                {
                    Logger.LogError("Account has already existed");
                    throw new AccountAlreadyExistsException();
                }

                var user = this.ObjectMapper.Map<CreateOrEditAccountDto, AppUser>(input);
                Logger.LogDebug("Map to entity successful");

                user.FullName = user.GetFullName();
                user.SetIsActive(input.IsActive);

                await this._userManagement.CreateAsync(user);
                await this._userManagement.AddPasswordAsync(user, input.Password);
                Logger.LogDebug("Inserted to database");

                if (input.RoleNames != null)
                {
                    Logger.LogDebug("Number of roles = {count}", input.RoleNames.Length);
                    await this._userManagement.SetRolesAsync(user, input.RoleNames);
                }
                else
                {
                    Logger.LogWarning("User have no any roles");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<bool> CheckExistAccount(CheckAccountInput input)
        {
            var query = await this._accountRepository.GetAccountByEmailAsync(input.Email);
            var result = query != null && (input.AccountId == null || (input.AccountId != null && query.Id != input.AccountId));

            return result;
        }

        public async Task<PagedResultDto<AccountDto>> GetAllAccountAsync(GetAllInputAccountDto input)
        {
            try
            {
                var query = await this._accountRepository.GetAllAccountsAsync(input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword);
                var chuyenMonList = await this._chucDanhRepository.GetAllListChucDanh();
                //var totalCount = await this._accountRepository.GetAccountCountAsync(input.Keyword);
                var totalCount = query.Count();

                var items = query.Select(s => new AccountDto()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    SurName = s.Surname,
                    FullName = s.FullName,
                    Email = s.Email,
                    UserName = s.UserName,
                    IsActive = s.IsActive,
                    ChucDanhId = s.ChucDanhId
                }).ToList();

                foreach (var item in items)
                {
                    var chuyenMon = chuyenMonList.FirstOrDefault(f => f.Id == item.ChucDanhId);
                    if (chuyenMon != null)
                    {
                        item.TenChucDanh = chuyenMon.TenChucDanh;
                    }
                }

                return new PagedResultDto<AccountDto>()
                {
                    TotalCount = totalCount,
                    Items = items
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateAccountAsync(CreateOrEditAccountDto input)
        {
            try
            {
                Logger.LogInformation("Start {service}.UpdateAsync", SERVICE_NAME);
                var user = await this._accountRepository.FindByIdAsync(input.Id);
                if (user == null)
                {
                    Logger.LogError("User not found");
                    //throw new UserNotFoundException();
                }

                var checkExist = await this.CheckExistAccount(new CheckAccountInput()
                {
                    Email = input.Email,
                    AccountId = input.Id
                });

                if (checkExist)
                {
                    Logger.LogError("Account has already existed");
                    throw new AccountAlreadyExistsException();
                }

                user = this.ObjectMapper.Map<CreateOrEditAccountDto, AppUser>(input, user);
                Logger.LogDebug("Map to entity successful");

                user.FullName = user.GetFullName();
                user.SetIsActive(input.IsActive);

                await this._userManagement.UpdateAsync(user);
                Logger.LogDebug("Updated to database");

                if (input.RoleNames != null)
                {
                    Logger.LogDebug("Number of roles = {count}", input.RoleNames.Length);
                    await this._userManagement.SetRolesAsync(user, input.RoleNames);
                }
                else
                {
                    Logger.LogWarning("User have no any roles");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AccountDto> GetAccountAsync(Guid? id)
        {
            if (id == Guid.Empty || id == null)
            {
                throw new UserFriendlyException("Id is required");
            }

            var user = await this._accountRepository.GetAccountAsync(id);
            if (user == null)
            {
                throw new UserFriendlyException("User not found");
            }

            var result = new AccountDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SurName = user.Surname,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                IsActive = user.IsActive,
                ChucDanhId = user.ChucDanhId
            };

            result.RoleNames = await this._identityUserAppService.GetRolesAsync(user.Id);
            return result;
        }
    }
}
