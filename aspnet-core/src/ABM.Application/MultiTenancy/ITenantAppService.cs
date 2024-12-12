using Abp.Application.Services;
using ABM.MultiTenancy.Dto;

namespace ABM.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

