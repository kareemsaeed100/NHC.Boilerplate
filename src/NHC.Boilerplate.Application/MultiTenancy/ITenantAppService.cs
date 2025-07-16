using Abp.Application.Services;
using NHC.Boilerplate.MultiTenancy.Dto;

namespace NHC.Boilerplate.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

