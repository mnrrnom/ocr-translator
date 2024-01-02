using System.Text.Json;

namespace game_translator.Utils;

public static class ConfigurationManager
{
    private const string _configPath = "./config.json";
    
    public static void CreateConfiguration()
    {
        if (File.Exists(_configPath)) return;
        
        var config = new Configuration();
        var stringified = JsonSerializer.Serialize(config);
        File.WriteAllText(_configPath, stringified);
    }
    
    public static Configuration? GetConfiguration()
    {
        var stringified = File.ReadAllText(_configPath);
        return JsonSerializer.Deserialize<Configuration>(stringified);
    }
    
    public static void UpdateConfiguration(Configuration config)
    {
        var stringified = JsonSerializer.Serialize(config);
        File.WriteAllText(_configPath, stringified);
    }
}

public class Configuration
{
    public string? GoogleTranslateToken { get; set; }
    public string? GoogleTranslateApiKey { get; set; }
} 