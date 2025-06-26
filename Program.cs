using System.Text.Json;

//Console.ForegroundColor = ConsoleColor.Red;

Console.WriteLine(@"

▗▞▀▀▘▗▞▀▚▖   ■  ▗▞▀▘▐▌    ▄▄▄  ■  ▗▞▀▜▌
▐▌   ▐▛▀▀▘▗▄▟▙▄▖▝▚▄▖▐▌   ▀▄▄▗▄▟▙▄▖▝▚▄▟▌
▐▛▀▘ ▝▚▄▄▖  ▐▌      ▐▛▀▚▖▄▄▄▀ ▐▌       
▐▌          ▐▌      ▐▌ ▐▌     ▐▌       
            ▐▌                ▐▌       
                        by                              
                               
                     ▄▄▄ ▗▞▀▘▗▞▀▜▌▄▄▄▄  █  ▄  ▄▄▄ 
                    ▀▄▄  ▝▚▄▖▝▚▄▟▌█   █ █▄▀  ▀▄▄  
                    ▄▄▄▀          █   █ █ ▀▄ ▄▄▄▀ 
                                        █  █      
                              
                                    v1.0
====================================================

");


var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var configDir = Path.Combine(homeDir, ".fetchsta");
var configPath = Path.Combine(configDir, "config.json");

Config? config = new();

if (!Directory.Exists(configDir))
    Directory.CreateDirectory(configDir);

if (!File.Exists(configPath))
{
    var defaultConfig = new
    {
        DefaultDownloadPath = Path.Combine(homeDir, "Downloads")
    };

    var json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(configPath, json);

    Console.WriteLine("First launch detected. Creating config file @ " + configPath);
}
else
{
    config = ConfigHandler.GetConfig();
}

if (config == null)
{
    Console.WriteLine("Config Error\nGo to ~/.fetchsta and delete config and try again");
    return;
}



Console.WriteLine("File Request Link:");

while (true)
{
    string? requestLink = Console.ReadLine();
    if (!string.IsNullOrEmpty(requestLink))
    {
        try
        {
            // Handle Download
            await DownloadHandler.Execute(requestLink, config.DefaultDownloadPath);
            break;
        }
        catch (UriFormatException)
        {
            Console.WriteLine("Please enter a valid request URL");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("IO exception occurred when fetching file\nMake sure the URL contains a file to fetch....");
        }
    }

}




