using Abp.Application.Services;
using ABM.Sessions.Dto;
using System.Threading.Tasks;

namespace ABM.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
