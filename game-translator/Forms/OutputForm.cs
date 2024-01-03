using game_translator.Events;
using game_translator.Utils;

namespace game_translator.Forms;

public partial class OutputForm : Form
{
    private readonly SelectionForm _selectionForm = new();
    private readonly KeyboardHookHandler _keyboardHookHandler = new();
    private readonly GoogleTranslateService _googleTranslateService = new();

    public OutputForm()
    {
        InitializeComponent();

        _keyboardHookHandler.OnTriggered += KeyboardHookHandlerOnOnTriggered;
        WindowState = FormWindowState.Normal;
        
        _selectionForm.OnDisplayOutput += OnDisplayOutput;
    }

    private void KeyboardHookHandlerOnOnTriggered(object? sender, OnKeyboardHookTriggeredEvent e)
    {
        _selectionForm.Show();
    }

    private void OnDisplayOutput(object? sender, DisplayOutputEvent args)
    {
        if (string.IsNullOrEmpty(args.Text)) return;

        var translation = _googleTranslateService.GetTranslation(args.Text);
        var romaji = args.Text + Environment.NewLine + _googleTranslateService.GetRomanization(args.Text);

        rtbOutput.Text = translation;
        rtbRomaji.Text = romaji;

        TopMost = true;
    }

    protected override void OnActivated(EventArgs e)
    {
        Console.WriteLine("activated");
        _keyboardHookHandler.Activate();
        base.OnActivated(e);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _keyboardHookHandler.Dispose();
        _googleTranslateService.Dispose();
        _selectionForm.CleanUp();
        base.OnFormClosing(e);
    }

    private void btnSettings_Click(object sender, EventArgs e)
    {
        var settingsForm = new SettingsForm();
        settingsForm.ShowDialog();
    }

}