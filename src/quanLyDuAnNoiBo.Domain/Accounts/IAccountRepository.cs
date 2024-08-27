using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace quanLyDuAnNoiBo.Accounts
{
    public interface IAccountRepository : IRepository<AppUser>
    {
        Task<AppUser> GetAccountByEmailAsync(string email);
        Task<List<AppUser>> GetAllAccountsAsync(
            string? sorting,
            int skipCount,
            int maxResultCount,
            string? keyword);
        Task<long> GetAccountCountAsync(string? keyword);
        Task<AppUser?> FindByIdAsync(Guid? id);
        Task<AppUser?> GetAccountAsync(Guid? id);
        Task<List<AppUser>> GetAllAccountsForFilter();
        Task<AppUser> GetAdminDefault();
        Task<string> GetUsernameById(Guid? id);
    }
}
