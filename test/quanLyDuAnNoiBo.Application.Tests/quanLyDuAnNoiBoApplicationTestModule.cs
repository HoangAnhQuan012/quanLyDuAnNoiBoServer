using Volo.Abp.Modularity;

namespace quanLyDuAnNoiBo;

[DependsOn(
    typeof(quanLyDuAnNoiBoApplicationModule),
    typeof(quanLyDuAnNoiBoDomainTestModule)
)]
public class quanLyDuAnNoiBoApplicationTestModule : AbpModule
{

}
