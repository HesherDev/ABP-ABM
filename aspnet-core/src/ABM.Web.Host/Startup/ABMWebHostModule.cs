using Abp.Modules;
using Abp.Reflection.Extensions;
using ABM.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ABM.Web.Host.Startup
{
    [DependsOn(
       typeof(ABMWebCoreModule))]
    public class ABMWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ABMWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ABMWebHostModule).GetAssembly());
        }
    }
}
