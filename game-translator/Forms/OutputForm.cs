using game_translator.Events;
using game_translator.Utils;

namespace game_translator.Forms;

public partial class OutputForm : Form
{
    private readonly SelectionForm _selectionForm = new ();
    private readonly KeyboardHookHandler _keyboardHookHandler = new();
    private readonly GoogleTranslateService _googleTranslateService = new();

    public OutputForm()
    {
        InitializeComponent();
        
        _keyboardHookHandler.OnTriggered += KeyboardHookHandlerOnOnTriggered;
        WindowState = FormWindowState.Normal;
        rtbOutput.Font = new ("Arial", 16, FontStyle.Bold);
        rtbRomaji.Font = new ("Arial", 16, FontStyle.Bold);
        
        _selectionForm.Show();
        _selectionForm.OnDisplayOutput += OnDisplayOutput;
    }

    private void KeyboardHookHandlerOnOnTriggered(object? sender, OnKeyboardHookTriggeredEvent e)
    {
        _selectionForm.ShowOverlay();
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
    
    protected override void OnActivated (EventArgs e)
    {
        _keyboardHookHandler.Activate();
        base.OnActivated(e);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _keyboardHookHandler.Dispose();
        _selectionForm.Dispose();
        _googleTranslateService.Dispose();
        base.OnFormClosing(e);
    }
}