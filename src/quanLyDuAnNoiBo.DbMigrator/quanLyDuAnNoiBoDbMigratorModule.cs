using quanLyDuAnNoiBo.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace quanLyDuAnNoiBo.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(quanLyDuAnNoiBoEntityFrameworkCoreModule),
    typeof(quanLyDuAnNoiBoApplicationContractsModule)
    )]
public class quanLyDuAnNoiBoDbMigratorModule : AbpModule
{
}
