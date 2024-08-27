using Localization.Resources.AbpUi;
using quanLyDuAnNoiBo.Localization;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Aws;
using Microsoft.Extensions.DependencyInjection;

namespace quanLyDuAnNoiBo;

[DependsOn(
    typeof(quanLyDuAnNoiBoApplicationContractsModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule)
    )]
public class quanLyDuAnNoiBoHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        ConfigureLocalization();

        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.UseAws(Aws =>
                {
                    Aws.AccessKeyId = configuration["AwsS3:AccessKey"];
                    Aws.SecretAccessKey = configuration["AwsS3:SecretKey"];
                    Aws.UseCredentials = false;
                    Aws.Region = configuration["AwsS3:Region"];
                    Aws.ContainerName = configuration["AwsS3:BucketName"];
                    Aws.UseTemporaryCredentials = false;
                    Aws.UseTemporaryFederatedCredentials = false;
                });
            });
        });
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<quanLyDuAnNoiBoResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
