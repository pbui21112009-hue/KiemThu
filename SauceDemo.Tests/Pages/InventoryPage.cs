using OpenQA.Selenium;

namespace SauceDemo.Tests.Pages;

public class InventoryPage(IWebDriver driver, int timeout) : BasePage(driver, timeout)
{
    private readonly By title = By.CssSelector("[data-test='title']");
    private readonly By backpack = By.Id("add-to-cart-sauce-labs-backpack");
    private readonly By cart = By.CssSelector("[data-test='shopping-cart-link']");
    public string Title => Find(title).Text;
    public void AddBackpack() => Find(backpack).Click();
    public void OpenCart() => Find(cart).Click();
}
