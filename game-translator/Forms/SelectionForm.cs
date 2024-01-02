using game_translator.Events;
using Tesseract;
using Timer = System.Windows.Forms.Timer;

namespace game_translator.Forms;

public partial class SelectionForm : Form
{
    private static Point? _startPoint;
    private static Rectangle? _rectangle;
    private static readonly Timer _timer = new();
    private static readonly TesseractEngine _tessEngine = new("./tessdata", "Japanese", EngineMode.Default);
    private readonly Pen _pen = new(Color.Red, 2);
    public event EventHandler<DisplayOutputEvent>? OnDisplayOutput;
    
    public SelectionForm()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.None;
        WindowState = FormWindowState.Maximized;
        Opacity = 0;
        KeyPreview = true;
        TopMost = true;
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        
        _timer.Interval = 1000 / 60;
        _timer.Tick += (_, _) => Invalidate();
        _timer.Start();
    }

    public void ShowOverlay()
    {
        Opacity = 0.3;
        TopMost = true;
    }
    
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _timer.Stop();
        _tessEngine.Dispose();
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
            Opacity = 0;
            SaveScreenInRectangle();
        }

        base.OnMouseUp(e);
    }

    private void SaveScreenInRectangle()
    {
        if (_rectangle == null) return;
        var rectangle = _rectangle.Value;

        using var bitmap = new Bitmap(rectangle.Width, rectangle.Height);
        using var g = Graphics.FromImage(bitmap);
        g.CopyFromScreen(rectangle.Location, Point.Empty, rectangle.Size);

        using var pix = PixConverter.ToPix(bitmap);
        using var page = _tessEngine.Process(pix);
        var text = page?.GetText()
            .Replace(" ", "")
            .Replace('\n', ' ')
            .Replace('\r', ' ')
            .Trim(' ', '\n', '\r');
        if (text?.Length <= 0) return;
        Clipboard.SetText(text ?? string.Empty);
        OnDisplayOutputEvent(new(text));
    }
    
    private void OnDisplayOutputEvent(DisplayOutputEvent e)
    {
        OnDisplayOutput?.Invoke(null, e);
    }
}