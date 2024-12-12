using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ABM.Configuration;
using ABM.EntityFrameworkCore;
using ABM.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace ABM.Migrator;

[DependsOn(typeof(ABMEntityFrameworkModule))]
public class ABMMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public ABMMigratorModule(ABMEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(ABMMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            ABMConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(ABMMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
