using game_translator.Events;
using game_translator.Utils;
using Tesseract;
using Timer = System.Windows.Forms.Timer;

namespace game_translator.Forms;

public partial class SelectionForm : Form
{
    private readonly OcrService _ocrService;
    private static Point? _startPoint;
    private static Rectangle? _rectangle;
    private static readonly Timer _timer = new();
    private readonly Pen _pen = new(Color.Red, 2);
    public event EventHandler<DisplayOutputEvent>? OnDisplayOutput;
    
    public SelectionForm(OcrService ocrService)
    {
        _ocrService = ocrService;
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.None;
        WindowState = FormWindowState.Maximized;
        KeyPreview = true;
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        Opacity = 0;
        
        _timer.Interval = 1000 / 60;
        _timer.Tick += (_, _) => Invalidate();
        _timer.Start();
    }

    public void StartSelection()
    {
        TopMost = true;
        Opacity = 0.3;
    } 
    
    public void StopSelection()
    {
        TopMost = false;
        Opacity = 0;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _timer.Stop();
        _pen.Dispose();
        _timer.Dispose();
        
        base.OnFormClosing(e);
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
        if (_startPoint != null && _rectangle != null && Opacity > 0)
        {
            e.Graphics.DrawRectangle(_pen, _rectangle.Value);
        }

        base.OnPaint(e);
    }
    
    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && _startPoint == null)
        {
            _startPoint = e.Location;
        }

        if (_startPoint != null)
        {
            _rectangle = new(
                Math.Min(_startPoint.Value.X, e.Location.X),
                Math.Min(_startPoint.Value.Y, e.Location.Y),
                Math.Abs(_startPoint.Value.X - e.Location.X),
                Math.Abs(_startPoint.Value.Y - e.Location.Y)
            );
        }
        
        base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _startPoint = null;
            StopSelection();
            SaveScreenInRectangle();
            _rectangle = null;
        }

        base.OnMouseUp(e);
    }

    private void SaveScreenInRectangle()
    {
        if (_rectangle == null) return;
        var text = _ocrService.GetTextFromScreenInBound(_rectangle.Value);
        if (string.IsNullOrEmpty(text)) return;
        Clipboard.SetText(text);
        OnDisplayOutputEvent(new(text));
    }
    
    private void OnDisplayOutputEvent(DisplayOutputEvent e)
    {
        OnDisplayOutput?.Invoke(null, e);
    }
}