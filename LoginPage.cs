using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestProject1.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // Locators
        private readonly By _loginNameField = By.Name("loginname");
        private readonly By _passwordField = By.Name("password");
        private readonly By _loginButton = By.XPath("//*[@id=\"loginFrm\"]/fieldset/button");
        private readonly By _logoutLink = By.CssSelector("ul.side_account_list a[href*='logout']");
        private readonly By _errorAlert = By.XPath("//div[contains(@class, 'alert') and contains(., 'Incorrect')]");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void PerformUserLogin(string username, string password)
        {
            _driver.FindElement(_loginNameField).SendKeys(username);
            _driver.FindElement(_passwordField).SendKeys(password);
            _driver.FindElement(_loginButton).Click();
        }

        public IWebElement WaitForLogoutLink()
        {
            return _wait.Until(d => {
                try
                {
                    IWebElement element = d.FindElement(_logoutLink);
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException) { return null; }
            });
        }

        public void ClickLogout()
        {
            _driver.FindElement(_logoutLink).Click();
        }

        public IWebElement WaitForErrorAlert()
        {
            return _wait.Until(d => {
                try
                {
                    IWebElement element = d.FindElement(_errorAlert);
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException) { return null; }
            });
        }
    }
}