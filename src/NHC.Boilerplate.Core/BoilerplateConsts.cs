using NHC.Boilerplate.Debugging;

namespace NHC.Boilerplate;

public class BoilerplateConsts
{
    public const string LocalizationSourceName = "Boilerplate";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "3ce208f4c19b4fb3a241e8853ea16d41";
}
