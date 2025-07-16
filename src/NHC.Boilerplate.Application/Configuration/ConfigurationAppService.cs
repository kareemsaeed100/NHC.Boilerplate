﻿using Abp.Authorization;
using Abp.Runtime.Session;
using NHC.Boilerplate.Configuration.Dto;
using System.Threading.Tasks;

namespace NHC.Boilerplate.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : BoilerplateAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
