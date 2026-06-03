using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject1.Pages;

namespace TestProject1
{
    public class Tests
    {
        private IWebDriver driver;
        private LoginPage loginPage;
        private RegistrationPage registrationPage;
        private ProductsPage productsPage;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Initialize Page Objects
            loginPage = new LoginPage(driver);
            registrationPage = new RegistrationPage(driver);
            productsPage = new ProductsPage(driver);
        }

        [Test, Order(1)]
        public void Login()
        {
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=account/login");

            loginPage.PerformUserLogin("Nagarjuna", "nag456");

            IWebElement logoffLink = loginPage.WaitForLogoutLink();
            Assert.That(logoffLink.Displayed, Is.True, "Authentication Failed: Logoff link was not found.");

            loginPage.ClickLogout();
        }

        [Test, Order(2)]
        public void Registration()
        {
            string uniqueId = Guid.NewGuid().ToString().Substring(0, 5);
            string firstName = "Nag" + uniqueId;
            string lastName = "Arjuna";
            string email = $"nag_{uniqueId}@test.com";
            string loginName = "nag_" + uniqueId;
            string password = "nag456!";

            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=account/create");

            registrationPage.FillPersonalDetails(firstName, lastName, email, "1234567890");
            registrationPage.FillAddressDetails("123 Automation Lane", "Aberdeen", "Aberdeen", "75001");
            registrationPage.FillLoginDetails(loginName, password);
            registrationPage.AcceptPoliciesAndSubmit();

            IWebElement successHeading = registrationPage.WaitForSuccessHeading();
            Assert.That(successHeading.Text, Does.Contain("YOUR ACCOUNT HAS BEEN CREATED!"), "Registration Failed: Success banner was not found.");
        }

        [Test, Order(3)]
        public void Products()
        {
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=account/login");

            loginPage.PerformUserLogin("Nagarjuna", "nag456");

            productsPage.SearchForProduct("Skinsheen");
            productsPage.AddProductToCart();
            productsPage.ProceedToCheckout();
            productsPage.ConfirmFinalOrder();

            IWebElement orderSuccessHeader = productsPage.WaitForOrderSuccessHeader();
            Assert.That(orderSuccessHeader.Text, Does.Contain("YOUR ORDER HAS BEEN PROCESSED!"), "Checkout Flow Failed: Success text screen was not reached.");
        }

        [Test, Order(4)]
        public void NegativeScenario()
        {
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=account/login");

            loginPage.PerformUserLogin("Nagarjuna", "nag");

            IWebElement errorAlert = loginPage.WaitForErrorAlert();
            Assert.That(errorAlert.Text, Does.Contain("Incorrect login or password"), "Negative test failed: The error warning banner was not captured.");
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}