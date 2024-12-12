using ABM.Debugging;

namespace ABM;

public class ABMConsts
{
    public const string LocalizationSourceName = "ABM";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "e653f7a6771942c98853b45d7bdf3a4a";
}
