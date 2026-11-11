using System.Text.Json;

namespace SauceDemo.Tests.Framework;

public record Settings(string BaseUrl, string Browser, int TimeoutSeconds, bool Headless)
{
    public static Settings Load()
    {
        var settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json")))
            ?? throw new InvalidDataException("Missing settings");
        settings = settings with { Browser = Environment.GetEnvironmentVariable("TEST_BROWSER") ?? settings.Browser };
        if (!Uri.TryCreate(settings.BaseUrl, UriKind.Absolute, out var uri) || uri.Scheme != "https" || settings.TimeoutSeconds <= 0 || settings.Browser is not ("chrome" or "coccoc"))
            throw new InvalidDataException("Expected HTTPS URL, positive timeout and chrome/coccoc browser");
        return settings;
    }
}
