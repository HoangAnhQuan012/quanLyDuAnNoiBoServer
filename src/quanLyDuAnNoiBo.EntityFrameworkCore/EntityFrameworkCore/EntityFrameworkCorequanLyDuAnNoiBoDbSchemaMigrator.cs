using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using quanLyDuAnNoiBo.Data;
using Volo.Abp.DependencyInjection;

namespace quanLyDuAnNoiBo.EntityFrameworkCore;

public class EntityFrameworkCorequanLyDuAnNoiBoDbSchemaMigrator
    : IquanLyDuAnNoiBoDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCorequanLyDuAnNoiBoDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the quanLyDuAnNoiBoDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<quanLyDuAnNoiBoDbContext>()
            .Database
            .MigrateAsync();
    }
}
