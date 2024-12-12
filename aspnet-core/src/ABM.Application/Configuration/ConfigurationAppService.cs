using Abp.Authorization;
using Abp.Runtime.Session;
using ABM.Configuration.Dto;
using System.Threading.Tasks;

namespace ABM.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : ABMAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
