using System.Text;
using game_translator.Forms;
using game_translator.Utils;

namespace game_translator;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ConfigurationManager.CreateConfiguration();
        ApplicationConfiguration.Initialize();
        Application.Run(new OutputForm());
    }
}