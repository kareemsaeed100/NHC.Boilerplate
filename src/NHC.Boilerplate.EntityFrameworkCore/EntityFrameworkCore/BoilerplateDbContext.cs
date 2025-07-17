using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NHC.Boilerplate.Authorization.Roles;
using NHC.Boilerplate.Authorization.Users;
using NHC.Boilerplate.MultiTenancy;
using NHC.Boilerplate.Notifications;

namespace NHC.Boilerplate.EntityFrameworkCore;

public class BoilerplateDbContext : AbpZeroDbContext<Tenant, Role, User, BoilerplateDbContext>
{
    /* Define a DbSet for each entity of the application */
    public DbSet<Notification> Notification { get; set; }

    public BoilerplateDbContext(DbContextOptions<BoilerplateDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoilerplateDbContext).Assembly);
    }
}
