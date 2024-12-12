using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ABM.Authorization;

namespace ABM;

[DependsOn(
    typeof(ABMCoreModule),
    typeof(AbpAutoMapperModule))]
public class ABMApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<ABMAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(ABMApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
