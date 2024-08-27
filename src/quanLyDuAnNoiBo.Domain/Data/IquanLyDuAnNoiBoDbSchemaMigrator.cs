using System.Threading.Tasks;

namespace quanLyDuAnNoiBo.Data;

public interface IquanLyDuAnNoiBoDbSchemaMigrator
{
    Task MigrateAsync();
}
