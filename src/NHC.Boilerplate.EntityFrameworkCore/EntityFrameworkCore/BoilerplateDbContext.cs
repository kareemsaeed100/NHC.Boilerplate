using Abp.Zero.EntityFrameworkCore;
using NHC.Boilerplate.Authorization.Roles;
using NHC.Boilerplate.Authorization.Users;
using NHC.Boilerplate.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace NHC.Boilerplate.EntityFrameworkCore;

public class BoilerplateDbContext : AbpZeroDbContext<Tenant, Role, User, BoilerplateDbContext>
{
    /* Define a DbSet for each entity of the application */

    public BoilerplateDbContext(DbContextOptions<BoilerplateDbContext> options)
        : base(options)
    {
    }
}
