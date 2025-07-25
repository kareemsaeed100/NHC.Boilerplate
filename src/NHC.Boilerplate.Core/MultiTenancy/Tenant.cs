﻿using Abp.MultiTenancy;
using NHC.Boilerplate.Authorization.Users;

namespace NHC.Boilerplate.MultiTenancy;

public class Tenant : AbpTenant<User>
{
    public Tenant()
    {
    }

    public Tenant(string tenancyName, string name)
        : base(tenancyName, name)
    {
    }
}
