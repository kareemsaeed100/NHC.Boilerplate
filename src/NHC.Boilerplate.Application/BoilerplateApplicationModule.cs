using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NHC.Boilerplate.Authorization;

namespace NHC.Boilerplate;

[DependsOn(
    typeof(BoilerplateCoreModule),
    typeof(AbpAutoMapperModule))]
public class BoilerplateApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<BoilerplateAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(BoilerplateApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
