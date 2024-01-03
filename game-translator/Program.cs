using game_translator.Forms;
using game_translator.Utils;

namespace game_translator;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        AppConfigurationManager.EnsureConfigurationCreated();
        ApplicationConfiguration.Initialize();
        
        
        
        Application.Run();
    }
}