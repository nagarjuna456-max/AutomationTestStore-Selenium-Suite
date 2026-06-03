using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestProject1.Pages
{
    public class RegistrationPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // Locators
        private readonly By _firstNameField = By.Id("AccountFrm_firstname");
        private readonly By _lastNameField = By.Id("AccountFrm_lastname");
        private readonly By _emailField = By.Id("AccountFrm_email");
        private readonly By _telephoneField = By.Id("AccountFrm_telephone");
        private readonly By _addressField = By.Id("AccountFrm_address_1");
        private readonly By _cityField = By.Id("AccountFrm_city");
        private readonly By _zoneDropdown = By.Id("AccountFrm_zone_id");
        private readonly By _postcodeField = By.Id("AccountFrm_postcode");
        private readonly By _loginNameField = By.Id("AccountFrm_loginname");
        private readonly By _passwordField = By.Id("AccountFrm_password");
        private readonly By _confirmPasswordField = By.Id("AccountFrm_confirm");
        private readonly By _newsletterRadio = By.Id("AccountFrm_newsletter0");
        private readonly By _agreeCheckbox = By.Id("AccountFrm_agree");
        private readonly By _continueButton = By.XPath("//button[@title='Continue']");
        private readonly By _successHeading = By.CssSelector("h1.heading1 span.maintext");

        public RegistrationPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void FillPersonalDetails(string first, string last, string email, string phone)
        {
            _driver.FindElement(_firstNameField).SendKeys(first);
            _driver.FindElement(_lastNameField).SendKeys(last);
            _driver.FindElement(_emailField).SendKeys(email);
            _driver.FindElement(_telephoneField).SendKeys(phone);
        }

        public void FillAddressDetails(string address, string city, string zoneText, string postcode)
        {
            _driver.FindElement(_addressField).SendKeys(address);
            _driver.FindElement(_cityField).SendKeys(city);

            SelectElement zoneDropdown = new SelectElement(_driver.FindElement(_zoneDropdown));
            zoneDropdown.SelectByText(zoneText);

            _driver.FindElement(_postcodeField).SendKeys(postcode);
        }

        public void FillLoginDetails(string loginName, string password)
        {
            _driver.FindElement(_loginNameField).SendKeys(loginName);
            _driver.FindElement(_passwordField).SendKeys(password);
            _driver.FindElement(_confirmPasswordField).SendKeys(password);
        }

        public void AcceptPoliciesAndSubmit()
        {
            _driver.FindElement(_newsletterRadio).Click();
            _driver.FindElement(_agreeCheckbox).Click();
            _driver.FindElement(_continueButton).Click();
        }

        public IWebElement WaitForSuccessHeading()
        {
            return _wait.Until(d => {
                IWebElement el = d.FindElement(_successHeading);
                return el.Displayed ? el : null;
            });
        }
    }
}