﻿using Abp.Application.Services;
using NHC.Boilerplate.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace NHC.Boilerplate.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
