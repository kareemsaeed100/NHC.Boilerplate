using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NHC.Boilerplate.FarzCertificate;
using NHC.Boilerplate.Integration.NHCIntegrationClient;
using NHC.Boilerplate.Integration.Services.FarzCertificate;
namespace NHC.Boilerplate.Integration;

/// <summary>
/// NHCBoilerplateIntegrationModule is the main module for the NHC Boilerplate Integration project.
/// </summary>
public class NHCBoilerplateIntegrationModule : AbpModule
{
    /// <summary>
    /// Pre-initializes the module use to set up configurations or dependencies before the module is initialized.
    /// </summary>
    public override void PreInitialize()
    {
        // Adding the NHClientSettingProvider to the configuration settings providers
        Configuration.Settings.Providers.Add<NHClientSettingProvider>();
        Configuration.Settings.Providers.Add<FarzDataSettingProvider>();

    }
    /// <summary>
    /// Initializes the module by registering services and dependencies .
    /// </summary>
    public override void Initialize()
    {
        // Registering the assembly for dependency injection
        IocManager.RegisterAssemblyByConvention(typeof(NHCBoilerplateIntegrationModule).GetAssembly());


        // Registering services
        IocManager.Register<INHClient, NHCClient>(lifeStyle: DependencyLifeStyle.Singleton);


        // Registering services with the service collection
        var services = new ServiceCollection();
        services.AddScoped<IFarzCertificateDataProvider, FarzCertificateIntegrationProvider>();
        services.AddScoped<IFarzCertificateDataProviderMapper, FarzCertificateDataProviderMapper>();

        IocManager.IocContainer.AddServices(services);
    }
}