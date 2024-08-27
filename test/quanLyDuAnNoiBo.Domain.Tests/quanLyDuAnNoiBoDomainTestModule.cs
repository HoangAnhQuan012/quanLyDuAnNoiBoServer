using Volo.Abp.Modularity;

namespace quanLyDuAnNoiBo;

[DependsOn(
    typeof(quanLyDuAnNoiBoDomainModule),
    typeof(quanLyDuAnNoiBoTestBaseModule)
)]
public class quanLyDuAnNoiBoDomainTestModule : AbpModule
{

}
