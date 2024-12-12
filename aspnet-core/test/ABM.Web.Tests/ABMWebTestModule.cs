using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ABM.EntityFrameworkCore;
using ABM.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ABM.Web.Tests;

[DependsOn(
    typeof(ABMWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class ABMWebTestModule : AbpModule
{
    public ABMWebTestModule(ABMEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(ABMWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(ABMWebMvcModule).Assembly);
    }
}