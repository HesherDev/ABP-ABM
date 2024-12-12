using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Security;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using ABM.Authorization.Roles;
using ABM.Authorization.Users;
using ABM.Configuration;
using ABM.Localization;
using ABM.MultiTenancy;
using ABM.Timing;

namespace ABM;

[DependsOn(typeof(AbpZeroCoreModule))]
public class ABMCoreModule : AbpModule
{
    public override void PreInitialize()
    {
        // Establecer la conexión predeterminada desde appsettings.json
        Configuration.DefaultNameOrConnectionString = "Default";

        Configuration.Auditing.IsEnabledForAnonymousUsers = true;

        // Declarar tipos de entidad
        Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
        Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
        Configuration.Modules.Zero().EntityTypes.User = typeof(User);

        ABMLocalizationConfigurer.Configure(Configuration.Localization);

        // Habilitar aplicación multi-tenant
        Configuration.MultiTenancy.IsEnabled = ABMConsts.MultiTenancyEnabled;

        // Configurar roles
        AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

        Configuration.Settings.Providers.Add<AppSettingProvider>();

        Configuration.Localization.Languages.Add(new LanguageInfo("fa", "فارسی", "famfamfam-flags ir"));

        // Configuración de cifrado
        Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = ABMConsts.DefaultPassPhrase;
        SimpleStringCipher.DefaultPassPhrase = ABMConsts.DefaultPassPhrase;
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(ABMCoreModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
    }
}
