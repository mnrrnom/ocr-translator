using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace game_translator.Utils;

public class GoogleTranslateService : IDisposable
{
    private readonly HttpClient _translationClient;
    private readonly HttpClient _romajiClient;
    
    private const string _romanizeUrl = "https://translation.googleapis.com/v3/projects/game-ocr-410008/locations/us-central1:romanizeText";
    private readonly string _translateUrl = $"https://translation.googleapis.com/language/translate/v2?key={ConfigurationManager.GetConfiguration()?.GoogleTranslateApiKey}";
    
    private readonly string? _token = ConfigurationManager.GetConfiguration()?.GoogleTranslateToken;

    public GoogleTranslateService()
    {
        _translationClient = new ();
        _romajiClient = new ();
        
        _romajiClient.DefaultRequestHeaders.Authorization = new ("Bearer", _token);
        _romajiClient.DefaultRequestHeaders.Add("x-goog-user-project", "game-ocr-410008");
    }
    
    public string GetRomanization(string text)
    {
        var stringified = JsonSerializer.Serialize(new RomanizeRequest
        {
            SourceLanguageCode = "ja",
            Contents = text
        });
        
        var requestBody = new StringContent(stringified, Encoding.UTF8, "application/json");
        
        var response = _romajiClient.PostAsync(_romanizeUrl, requestBody).Result;

        if (!response.IsSuccessStatusCode) return string.Empty;
        
        var responseContent = response.Content.ReadAsStringAsync().Result;
        var result = JsonSerializer.Deserialize<RomanizeResponse>(responseContent);
        return result?.Romanizations.FirstOrDefault()?.RomanizedText ?? "";
    }
    
    public string GetTranslation(string text)
    {
        var stringified = JsonSerializer.Serialize(new TranslateRequest()
        {
            Q = text
        });
        
        var requestBody = new StringContent(stringified, Encoding.UTF8, "application/json");
        
        var response = _translationClient.PostAsync(_translateUrl, requestBody).Result;

        if (!response.IsSuccessStatusCode) return string.Empty;
        
        var responseContent = response.Content.ReadAsStringAsync().Result;
        var result = JsonSerializer.Deserialize<TranslateResponse>(responseContent);
        return result?.Data.Translations.FirstOrDefault()?.TranslatedText ?? "";
    }

    public void Dispose()
    {
        _romajiClient.Dispose();
        _translationClient.Dispose();
    }
}

internal class RomanizeRequest
{
    [JsonPropertyName("source_language_code")]
    public string SourceLanguageCode { get; set; }

    [JsonPropertyName("contents")]
    public string Contents { get; set; }
}

internal class RomanizeResponse
{
    [JsonPropertyName("romanizations")]
    public List<Romanization> Romanizations { get; set; } = null!;
}

internal class Romanization
{
    [JsonPropertyName("romanizedText")]
    public string RomanizedText { get; set; }
}

internal class TranslateRequest
{
    [JsonPropertyName("q")]
    public required string Q { get; set; }

    [JsonPropertyName("target")] public string Target { get; set; } = "en";

    [JsonPropertyName("source")] public string Source { get; set; } = "ja";

    [JsonPropertyName("format")] public string Format { get; set; } = "text";
}

internal class TranslateResponse
{
    [JsonPropertyName("data")] public TranslateData Data { get; set; }
}

internal class TranslateData
{
    [JsonPropertyName("translations")] public List<Translation> Translations { get; set; } = null!;
}

internal class Translation
{
    [JsonPropertyName("translatedText")] public string TranslatedText { get; set; } = null!;
}