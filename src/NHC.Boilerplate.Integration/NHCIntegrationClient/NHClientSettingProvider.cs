using Abp.Configuration;

namespace NHC.Boilerplate.Integration.NHCIntegrationClient;

internal class NHClientSettingProvider : SettingProvider
{
    private readonly ISettingEncryptionService _settingEncryptionService;

    public NHClientSettingProvider(ISettingEncryptionService settingEncryptionService)
    {
        _settingEncryptionService = settingEncryptionService;
    }
    public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
    {
        SettingScopes settingScopes = SettingScopes.Application;
        return
        [
            new(NhcClientSetting.BaseUrl,"https://integration-gw.housingapps.sa/nhc/dev/",scopes: settingScopes),
            new(NhcClientSetting.ClientId, NhcClientSetting.ClientIdDefaultValue,scopes: settingScopes,isEncrypted:true),
            new SettingDefinition(NhcClientSetting.ClientSecret, NhcClientSetting.ClientSecretDefaultValue,scopes: settingScopes,isEncrypted:true),
            new(NhcClientSetting.RefId,"12",scopes: settingScopes),
        ];
    }
}
