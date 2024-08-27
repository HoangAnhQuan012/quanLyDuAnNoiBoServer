using System;
using System.Collections.Generic;
using System.Text;
using quanLyDuAnNoiBo.Localization;
using Volo.Abp.Application.Services;

namespace quanLyDuAnNoiBo;

/* Inherit your application services from this class.
 */
public abstract class quanLyDuAnNoiBoAppService : ApplicationService
{
    protected quanLyDuAnNoiBoAppService()
    {
        LocalizationResource = typeof(quanLyDuAnNoiBoResource);
    }
}
