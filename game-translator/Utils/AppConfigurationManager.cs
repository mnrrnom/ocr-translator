using System.Text.Json;

namespace game_translator.Utils;

public static class AppConfigurationManager
{
    private const string _configPath = "./config.json";
    
    public static void EnsureConfigurationCreated()
    {
        if (File.Exists(_configPath)) return;
        
        var config = new AppConfiguration();
        _writeConfiguration(config);
    }
    
    public static AppConfiguration? GetConfiguration()
    {
        var stringified = File.ReadAllText(_configPath);
        return JsonSerializer.Deserialize<AppConfiguration>(stringified);
    }
    
    public static void UpdateConfiguration(AppConfiguration config)
    {
        _writeConfiguration(config);
    }
    
    private static void _writeConfiguration(AppConfiguration config)
    {
        var stringified = JsonSerializer.Serialize(config);
        File.WriteAllText(_configPath, stringified);
    }
}

public class AppConfiguration
{
    public string? GoogleTranslateToken { get; set; }
    public string? GoogleTranslateApiKey { get; set; }
} 