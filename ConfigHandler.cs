using System.Text.Json;

public class ConfigHandler
{
    public static Config? GetConfig()
    {
        var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var configDir = Path.Combine(homeDir, ".fetchsta");
        var configPath = Path.Combine(configDir, "config.json");


        var configText = File.ReadAllText(configPath);
        return JsonSerializer.Deserialize<Config>(configText) ?? null;
    }
}