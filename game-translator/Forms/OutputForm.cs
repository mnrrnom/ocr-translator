using game_translator.Events;
using game_translator.Utils;

namespace game_translator.Forms;

public partial class OutputForm : Form
{
    private readonly GoogleTranslateService _googleTranslateService = new();

    public OutputForm()
    {
        InitializeComponent();

        WindowState = FormWindowState.Normal;
    }

    public void DisplayOutput(object? sender, DisplayOutputEvent args)
    {
        if (string.IsNullOrEmpty(args.Text)) return;

        var translation = _googleTranslateService.GetTranslation(args.Text);
        var romaji = args.Text + Environment.NewLine + _googleTranslateService.GetRomanization(args.Text);

        rtbOutput.Text = translation;
        rtbRomaji.Text = romaji;

        TopMost = true;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _googleTranslateService.Dispose();
        base.OnFormClosing(e);
    }

    private void btnSettings_Click(object sender, EventArgs e)
    {
        var settingsForm = new SettingsForm();
        settingsForm.ShowDialog();
    }

}