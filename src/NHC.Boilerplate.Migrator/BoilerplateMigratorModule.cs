using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NHC.Boilerplate.Configuration;
using NHC.Boilerplate.EntityFrameworkCore;
using NHC.Boilerplate.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace NHC.Boilerplate.Migrator;

[DependsOn(typeof(BoilerplateEntityFrameworkModule))]
public class BoilerplateMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public BoilerplateMigratorModule(BoilerplateEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(BoilerplateMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            BoilerplateConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(BoilerplateMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
