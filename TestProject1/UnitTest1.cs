using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestProject1
{
    public class Tests
    {
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=account/login");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        //reusable method for login
        private void PerformUserLogin(string username, string password)
        {
            IWebElement loginNameField = driver.FindElement(By.Name("loginname"));
            IWebElement passwordField = driver.FindElement(By.Name("password"));
            IWebElement loginButton = driver.FindElement(By.XPath("//*[@id=\"loginFrm\"]/fieldset/button"));

            loginNameField.SendKeys(username);
            passwordField.SendKeys(password);
            loginButton.Click();
        }

        //Login Flow
        [Test,Order(1)]
        public void Login()
        {
           
            PerformUserLogin("Nagarjuna", "nag456");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => {
                try
                {
                    IWebElement element = d.FindElement(By.CssSelector("ul.side_account_list a[href*='logout']"));
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });

            // Assert success and log off
            IWebElement logoffLink = driver.FindElement(By.CssSelector("ul.side_account_list a[href*='logout']"));
            Assert.That(logoffLink.Displayed, Is.True, "Authentication Failed: Logoff link was not found.");
            logoffLink.Click();
        }

        //Registration Flow
        [Test,Order(2)]
        public void Registration()
        {
            string uniqueId = Guid.NewGuid().ToString().Substring(0, 5); // Generates a unique 5-character string
            string FirstName = "Nag" + uniqueId;
            string LastName = "Arjuna";
            string Email = $"nag_{uniqueId}@test.com";
            string LoginName = "nag_" + uniqueId;
            string password = "nag456!";

            // Navigate straight to the registration form page
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=account/create");

            // Fill out Personal Details
            driver.FindElement(By.Id("AccountFrm_firstname")).SendKeys(FirstName);
            driver.FindElement(By.Id("AccountFrm_lastname")).SendKeys(LastName);
            driver.FindElement(By.Id("AccountFrm_email")).SendKeys(Email);
            driver.FindElement(By.Id("AccountFrm_telephone")).SendKeys("1234567890");

            // Fill out Address details
            driver.FindElement(By.Id("AccountFrm_address_1")).SendKeys("123 Automation Lane");
            driver.FindElement(By.Id("AccountFrm_city")).SendKeys("Aberdeen");

            // Selecting Dropdown
            SelectElement zoneDropdown = new SelectElement(driver.FindElement(By.Id("AccountFrm_zone_id")));
            zoneDropdown.SelectByText("Aberdeen");

            driver.FindElement(By.Id("AccountFrm_postcode")).SendKeys("75001");

            // Filling out Login Details
            driver.FindElement(By.Id("AccountFrm_loginname")).SendKeys(LoginName);
            driver.FindElement(By.Id("AccountFrm_password")).SendKeys(password);
            driver.FindElement(By.Id("AccountFrm_confirm")).SendKeys(password);

            // Checking for Privacy Policy
            driver.FindElement(By.Id("AccountFrm_newsletter0")).Click();
            driver.FindElement(By.Id("AccountFrm_agree")).Click();

            // Submitting the registration form
            driver.FindElement(By.XPath("//button[@title='Continue']")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement successHeading = wait.Until(d => {
                IWebElement el = d.FindElement(By.CssSelector("h1.heading1 span.maintext"));
                return el.Displayed ? el : null;
            });

            // Verifying account creation success
            Assert.That(successHeading.Text, Does.Contain("YOUR ACCOUNT HAS BEEN CREATED!"),"Registration Failed: Success banner was not found.");
        }

        //Product Search and Checkout Flow
        [Test,Order(3)]
        public void Products()
        {
            // Authentication
            PerformUserLogin("Nagarjuna", "nag456");
            //Wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // Search for the product
            IWebElement searchInput = wait.Until(d => {
                try
                {
                    IWebElement el = d.FindElement(By.Id("filter_keyword"));
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
            // Searching for the specific product
            searchInput.SendKeys("Skinsheen");
            searchInput.SendKeys(Keys.Enter);
            // Click the 'Add to Cart' button 
                IWebElement addToCartButton = wait.Until(d => {
                IWebElement element = d.FindElement(By.CssSelector("a.cart"));
                return element.Displayed ? element : null;
            });
            addToCartButton.Click();
            // Clicking the Checkout button
            IWebElement cartCheckoutButton = wait.Until(d => {
                try
                {
                    IWebElement element = d.FindElement(By.Id("cart_checkout1"));
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException) { return null; }
            });
            cartCheckoutButton.Click();
            // Clicking the Confirm Button
            IWebElement finalConfirmButton = wait.Until(d => {
                try
                {
                    IWebElement element = d.FindElement(By.Id("checkout_btn"));
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException) { return null; 
                }
            });
            finalConfirmButton.Click();
            // Success Response
            IWebElement orderSuccessHeader = wait.Until(d => {
                try
                {
                    IWebElement element = d.FindElement(By.CssSelector("h1.heading1 span.maintext"));
                    // To make sure it's not reading the old 'CHECKOUT CONFIRMATION' text
                    return element.Displayed && !element.Text.Contains("CONFIRMATION") ? element : null;
                }
                catch (NoSuchElementException) { return null; }
            });
            // Final Assertion
            Assert.That(orderSuccessHeader.Text, Does.Contain("YOUR ORDER HAS BEEN PROCESSED!"),
                "Checkout Flow Failed: Success text screen was not reached.");
        }

        // Negative Flow for Login
        [Test,Order(4)]
        public void NegativeScenario()
        {
            PerformUserLogin("Nagarjuna", "nag");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
           // Looking for Alert
            IWebElement errorAlert = wait.Until(d => {
                try
                {
                    IWebElement element = d.FindElement(By.XPath("//div[contains(@class, 'alert') and contains(., 'Incorrect')]"));
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });

            // 5. Final validation check
            Assert.That(errorAlert.Text, Does.Contain("Incorrect login or password"),
                "Negative test failed: The error warning banner was not captured.");
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
