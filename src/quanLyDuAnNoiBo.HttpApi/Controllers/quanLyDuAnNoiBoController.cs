using quanLyDuAnNoiBo.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace quanLyDuAnNoiBo.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class quanLyDuAnNoiBoController : AbpControllerBase
{
    protected quanLyDuAnNoiBoController()
    {
        LocalizationResource = typeof(quanLyDuAnNoiBoResource);
    }
}
