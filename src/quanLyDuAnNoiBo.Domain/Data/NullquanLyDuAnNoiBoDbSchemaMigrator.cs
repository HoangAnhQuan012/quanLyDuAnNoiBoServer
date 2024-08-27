using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace quanLyDuAnNoiBo.Data;

/* This is used if database provider does't define
 * IquanLyDuAnNoiBoDbSchemaMigrator implementation.
 */
public class NullquanLyDuAnNoiBoDbSchemaMigrator : IquanLyDuAnNoiBoDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
