using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using NHC.Boilerplate.EntityFrameworkCore.Seed.Host;
using NHC.Boilerplate.EntityFrameworkCore.Seed.Tenants;
using System;
using System.Transactions;

namespace NHC.Boilerplate.EntityFrameworkCore.Seed;

public static class SeedHelper
{
    public static void SeedHostDb(IIocResolver iocResolver)
    {
        //WithDbContext<BoilerplateDbContext>(iocResolver, SeedHostDb);
        WithDbContext<BoilerplateDbContext>(iocResolver, context => SeedHostDb(context, iocResolver));

    }

    public static void SeedHostDb(BoilerplateDbContext context, IIocResolver iocResolver)
    {
        context.SuppressAutoSetTenantId = true;

        // Host seed
        new InitialHostDbBuilder(context, iocResolver).Create();

        // Default tenant seed (in host database).
        new DefaultTenantBuilder(context).Create();
        new TenantRoleAndUserBuilder(context, 1).Create();
    }

    private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
        where TDbContext : DbContext
    {
        using (var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
        {
            using (var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
            {
                var context = uowManager.Object.Current.GetDbContext<TDbContext>(MultiTenancySides.Host);

                contextAction(context);

                uow.Complete();
            }
        }
    }
}
