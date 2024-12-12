using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ABM.Controllers
{
    public abstract class ABMControllerBase : AbpController
    {
        protected ABMControllerBase()
        {
            LocalizationSourceName = ABMConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
