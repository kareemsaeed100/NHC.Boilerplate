using Abp.Configuration;

namespace NHC.Boilerplate.Integration.Services.FarzCertificate;

internal class FarzDataSettingProvider : SettingProvider
{
    public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
    {
        SettingScopes settingScopes = SettingScopes.Application;
        return
        [
            new(FarzConstants.SettingNames.GetCertificateUrl,FarzConstants.SettingNames.GetCertificateUrlDefaultValue,scopes: settingScopes),
        ];
    }
}
