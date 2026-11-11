using OpenQA.Selenium;

namespace SauceDemo.Tests.Pages;

public class LoginPage(IWebDriver driver, int timeout) : BasePage(driver, timeout)
{
    private readonly By username = By.Id("user-name");
    private readonly By password = By.Id("password");
    private readonly By submit = By.Id("login-button");
    private readonly By error = By.CssSelector("[data-test='error']");
    public string Title => Driver.Title;
    public void Open(string url) { Driver.Navigate().GoToUrl(url); Find(username); }
    public void Login(string user, string secret)
    {
        Find(username).SendKeys(user);
        Find(password).SendKeys(secret);
        Find(submit).Click();
    }
    public string Error => Find(error).Text;
}
