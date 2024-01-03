using System.Diagnostics;
using game_translator.Forms;
using game_translator.Utils;

namespace game_translator;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        AppConfigurationManager.EnsureConfigurationCreated();
        _updateGoogleBearerToken();
        ApplicationConfiguration.Initialize();
        
        var keyboardHookHandler = new KeyboardHookHandler();
        var ocrService = new OcrService();
        var outputForm = new OutputForm();
        var selectionForm = new SelectionForm(ocrService);
        selectionForm.Show();
        outputForm.Show();
        selectionForm.OnDisplayOutput += (_, args) => outputForm.DisplayOutput(null, args);
        keyboardHookHandler.OnTriggered += (_, _) => selectionForm.StartSelection();
        
        outputForm.FormClosed += (_, _) =>
        {
            keyboardHookHandler.Dispose();
            ocrService.Dispose();
            selectionForm.Close();
        };
        
        Task.Run(async () =>
        {
            await keyboardHookHandler.ActivateAsync();
        });
        
        Application.Run(outputForm);
    }

    private static void _updateGoogleBearerToken()
    {
        var config = AppConfigurationManager.GetConfiguration();
        if (config == null) throw new("Configuration not found");
        
        var process = new Process();
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.FileName = "pwsh.exe";
        process.StartInfo.Arguments = "-c \"gcloud auth print-access-token\"";
        process.Start();
        process.WaitForExit();
        
        var token = process.StandardOutput.ReadToEnd().Trim();
        config.GoogleTranslateToken = token;
        AppConfigurationManager.UpdateConfiguration(config);
    }
}