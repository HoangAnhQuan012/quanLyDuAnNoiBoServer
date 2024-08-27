using Volo.Abp.Modularity;

namespace quanLyDuAnNoiBo;

/* Inherit from this class for your domain layer tests. */
public abstract class quanLyDuAnNoiBoDomainTestBase<TStartupModule> : quanLyDuAnNoiBoTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
