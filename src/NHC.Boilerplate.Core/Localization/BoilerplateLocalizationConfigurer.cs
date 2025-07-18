﻿using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace NHC.Boilerplate.Localization;

public static class BoilerplateLocalizationConfigurer
{
    public static void Configure(ILocalizationConfiguration localizationConfiguration)
    {
        localizationConfiguration.Sources.Add(
            new DictionaryBasedLocalizationSource(BoilerplateConsts.LocalizationSourceName,
                new XmlEmbeddedFileLocalizationDictionaryProvider(
                    typeof(BoilerplateLocalizationConfigurer).GetAssembly(),
                    "NHC.Boilerplate.Localization.SourceFiles"
                )
            )
        );
    }
}
