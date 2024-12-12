using Abp.Application.Services;
using ABM.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace ABM.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
