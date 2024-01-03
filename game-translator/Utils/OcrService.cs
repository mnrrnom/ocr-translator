using Tesseract;

namespace game_translator.Utils;

public class OcrService : IDisposable
{
    private readonly TesseractEngine _tessEngine = new("./tessdata", "Japanese", EngineMode.Default);
    
    public string? GetTextFromScreenInBound(Rectangle rectangle)
    {
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
        return text;
    }

    public void Dispose()
    {
        _tessEngine.Dispose();
    }
}