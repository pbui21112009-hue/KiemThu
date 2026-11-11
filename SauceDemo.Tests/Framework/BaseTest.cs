using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemo.Tests.Pages;

namespace SauceDemo.Tests.Framework;

public abstract class BaseTest
{
    protected IWebDriver Driver = null!;
    protected Settings Config = null!;
    protected LoginPage Login = null!;
    private string directory = "";

    [SetUp]
    public void Start()
    {
        directory = Path.Combine(TestContext.CurrentContext.WorkDirectory, "artifacts", TestContext.CurrentContext.Test.ID);
        Directory.CreateDirectory(directory);
        Log("START " + TestContext.CurrentContext.Test.Name);
        Config = Settings.Load();
        var options = new ChromeOptions();
        if (Config.Headless) options.AddArgument("--headless=new");
        options.AddArguments("--window-size=1440,1000", "--no-sandbox", "--disable-dev-shm-usage");
        var binary = Environment.GetEnvironmentVariable("CHROME_BINARY");
        if (!string.IsNullOrWhiteSpace(binary)) options.BinaryLocation = binary;
        Driver = new ChromeDriver(options);
        Login = new LoginPage(Driver, Config.TimeoutSeconds);
        Login.Open(Config.BaseUrl);
    }

    protected void Log(string message)
    {
        var line = $"{DateTime.UtcNow:O} {message}";
        TestContext.Progress.WriteLine(line);
        File.AppendAllText(Path.Combine(directory, "test.log"), line + Environment.NewLine);
    }

    [TearDown]
    public void Stop()
    {
        try
        {
            var result = TestContext.CurrentContext.Result;
            Log($"END {result.Outcome} {result.Message}");
            if (result.Outcome.Status == TestStatus.Failed && Driver is ITakesScreenshot camera)
            {
                var path = Path.Combine(directory, "failure.png");
                camera.GetScreenshot().SaveAsFile(path);
                TestContext.AddTestAttachment(path);
            }
        }
        catch (Exception e) { TestContext.Error.WriteLine("Artifact capture failed: " + e.Message); }
        finally { Driver?.Quit(); Driver?.Dispose(); }
    }
}
