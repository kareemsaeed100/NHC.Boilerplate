﻿using Abp.Auditing;
using NHC.Boilerplate.Sessions.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NHC.Boilerplate.Sessions;

public class SessionAppService : BoilerplateAppServiceBase, ISessionAppService
{
    [DisableAuditing]
    public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
    {
        var output = new GetCurrentLoginInformationsOutput
        {
            Application = new ApplicationInfoDto
            {
                Version = AppVersionHelper.Version,
                ReleaseDate = AppVersionHelper.ReleaseDate,
                Features = new Dictionary<string, bool>()
            }
        };

        if (AbpSession.TenantId.HasValue)
        {
            output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
        }

        if (AbpSession.UserId.HasValue)
        {
            output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
        }

        return output;
    }
}
