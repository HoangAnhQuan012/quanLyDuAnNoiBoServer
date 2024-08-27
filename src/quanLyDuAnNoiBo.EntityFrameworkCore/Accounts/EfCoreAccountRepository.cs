using Microsoft.EntityFrameworkCore;
using quanLyDuAnNoiBo.Entities;
using quanLyDuAnNoiBo.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace quanLyDuAnNoiBo.Accounts
{
    public class EfCoreAccountRepository : EfCoreRepository<quanLyDuAnNoiBoDbContext, AppUser, Guid>, IAccountRepository
    {
        public EfCoreAccountRepository(IDbContextProvider<quanLyDuAnNoiBoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<AppUser?> FindByIdAsync(Guid? id)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Include(e => e.Roles).Where(w => w.Id.Equals(id)).FirstOrDefaultAsync();
            return query;
        }

        public async Task<AppUser?> GetAccountAsync(Guid? id)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Include(e => e.Roles).Where(w => w.Id.Equals(id)).FirstOrDefaultAsync();
            return query;
        }

        public async Task<AppUser> GetAccountByEmailAsync(string email)
        {
            email = email.Trim();
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Include(e => e.Roles).Where(w => w.Email.Equals(email)).FirstOrDefaultAsync();
            return query;
        }

        public async Task<long> GetAccountCountAsync(string? keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
            }

            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .WhereIf(!string.IsNullOrEmpty(keyword), w => w.UserName.Contains(keyword) || w.Email.Contains(keyword))
                .OrderByDescending(o => o.CreationTime)
                .CountAsync();

            return query;
        }

        public async Task<AppUser> GetAdminDefault()
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.FirstOrDefaultAsync(e => e.Email.Contains("admin@abp.io"));
            return query;
        }

        public async Task<List<AppUser>> GetAllAccountsAsync(string? sorting, int skipCount, int maxResultCount, string? keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
            }

            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .Include(e => e.Roles)
                .WhereIf(!string.IsNullOrEmpty(keyword), w => w.UserName.Contains(keyword) || w.Email.Contains(keyword))
                .OrderBy(sorting ?? "UserName")
                .ToListAsync();

            return query;
        }

        public async Task<List<AppUser>> GetAllAccountsForFilter()
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.ToListAsync();

            return query;
        }

        public async Task<string> GetUsernameById(Guid? id)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.Id.Equals(id)).Select(s => s.UserName).FirstOrDefaultAsync();
            return query;
        }
    }
}
