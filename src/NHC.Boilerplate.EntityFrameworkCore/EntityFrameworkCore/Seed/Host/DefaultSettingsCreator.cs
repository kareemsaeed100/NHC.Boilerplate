using Abp.Configuration;
using Abp.Dependency;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Net.Mail;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace NHC.Boilerplate.EntityFrameworkCore.Seed.Host;

public class DefaultSettingsCreator
{
    private readonly BoilerplateDbContext _context;
    private readonly ISettingDefinitionManager _settingDefinitionManager;
    private readonly ISettingManager _SettinManger;
    private readonly ISettingEncryptionService _settingEncryptionService;

    public DefaultSettingsCreator(BoilerplateDbContext context, IIocResolver iocResolver)
    {
        _context = context;
        _settingDefinitionManager = iocResolver.Resolve<ISettingDefinitionManager>();
        _SettinManger = iocResolver.Resolve<ISettingManager>();
        _settingEncryptionService = iocResolver.Resolve<ISettingEncryptionService>();
    }

    public void Create()
    {
        int? tenantId = null;

        if (BoilerplateConsts.MultiTenancyEnabled == false)
        {
            tenantId = MultiTenancyConsts.DefaultTenantId;
        }

        const SettingScopes dataBaseSettingScopes = SettingScopes.Application;

        var dataBaseSettingDefinition = _settingDefinitionManager
            .GetAllSettingDefinitions()
            .Where(x => (x.Scopes & dataBaseSettingScopes) != 0)
            .ToList();

        foreach (var settingDefinition in dataBaseSettingDefinition)
        {

            if (!settingDefinition.IsEncrypted)
            {

                AddSettingIfNotExists(settingDefinition.Name, settingDefinition.DefaultValue, tenantId);
            }

            if (settingDefinition.IsEncrypted)
            {
                var encryptedDefaultValue = _settingEncryptionService.Encrypt(settingDefinition, settingDefinition.DefaultValue);
                settingDefinition.DefaultValue = encryptedDefaultValue;
                AddSettingIfNotExists(settingDefinition.Name, settingDefinition.DefaultValue, tenantId);
            }
        }

        // Emailing
        AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "admin@mydomain.com", tenantId);
        AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "mydomain.com mailer", tenantId);

        // Languages
        AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "en", tenantId);
    }

    private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
    {
        if (_context.Settings.IgnoreQueryFilters().Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
        {
            return;
        }

        _context.Settings.Add(new Setting(tenantId, null, name, value));
        _context.SaveChanges();
    }
}
