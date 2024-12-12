using ABM.Configuration.Dto;
using System.Threading.Tasks;

namespace ABM.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
