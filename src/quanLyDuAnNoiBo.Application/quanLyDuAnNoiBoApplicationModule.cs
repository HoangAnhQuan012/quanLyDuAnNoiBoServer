using Medallion.Threading;
using Medallion.Threading.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using quanLyDuAnNoiBo.Workers;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.DistributedLocking;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace quanLyDuAnNoiBo;

[DependsOn(
    typeof(quanLyDuAnNoiBoDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(quanLyDuAnNoiBoApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpBackgroundJobsModule),
    typeof(AbpDistributedLockingModule)
    )]
public class quanLyDuAnNoiBoApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<quanLyDuAnNoiBoApplicationModule>();
        });

        Configure<AbpBackgroundJobWorkerOptions>(options => 
        {
            options.DefaultTimeout = 60; // 60 seconds
        });

        var configuration = context.Services.GetConfiguration();

        context.Services.AddSingleton<IDistributedLockProvider>(sp =>
        {
            var connection = configuration.GetConnectionString("Default");

            return new SqlDistributedSynchronizationProvider(connection!);

        });
    }

    public override async void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<CanhBaoWorker>();
    }
}
