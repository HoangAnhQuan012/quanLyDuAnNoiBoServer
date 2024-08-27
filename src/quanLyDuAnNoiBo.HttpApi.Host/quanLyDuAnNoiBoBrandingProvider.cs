using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace quanLyDuAnNoiBo;

[Dependency(ReplaceServices = true)]
public class quanLyDuAnNoiBoBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "quanLyDuAnNoiBo";
}
