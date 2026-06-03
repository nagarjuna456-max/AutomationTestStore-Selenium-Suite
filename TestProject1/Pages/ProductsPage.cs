using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestProject1.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // Locators
        private readonly By _searchBox = By.Id("filter_keyword");
        private readonly By _addToCartButton = By.CssSelector("a.cart");
        private readonly By _cartCheckoutButton = By.Id("cart_checkout1");
        private readonly By _finalConfirmButton = By.Id("checkout_btn");
        private readonly By _orderSuccessHeader = By.CssSelector("h1.heading1 span.maintext");

        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void SearchForProduct(string productName)
        {
            IWebElement searchInput = _wait.Until(d => {
                try
                {
                    IWebElement el = d.FindElement(_searchBox);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (NoSuchElementException) { return null; }
            });

             searchInput.SendKeys(productName + Keys.Enter);
        }

        public void AddProductToCart()
        {
            IWebElement btn = _wait.Until(d => {
                IWebElement element = d.FindElement(_addToCartButton);
                return element.Displayed ? element : null;
            });
            btn.Click();
        }

        public void ProceedToCheckout()
        {
            IWebElement btn = _wait.Until(d => {
                try
                {
                    IWebElement element = d.FindElement(_cartCheckoutButton);
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException) { return null; }
            });
            btn.Click();
        }

        public void ConfirmFinalOrder()
        {
            IWebElement btn = _wait.Until(d => {
                try
                {
                    IWebElement element = d.FindElement(_finalConfirmButton);
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException) { return null; }
            });
            btn.Click();
        }

        public IWebElement WaitForOrderSuccessHeader()
        {
            return _wait.Until(d => {
                try
                {
                    IWebElement element = d.FindElement(_orderSuccessHeader);
                    return element.Displayed && !element.Text.Contains("CONFIRMATION") ? element : null;
                }
                catch (NoSuchElementException) { return null; }
            });
        }
    }
}