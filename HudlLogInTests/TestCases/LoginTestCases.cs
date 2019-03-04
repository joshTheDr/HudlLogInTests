using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using HudlLogInTests.PageObjects;


namespace HudlLogInTests.TestCases
{
    class LoginTestCases
    {
        private IWebDriver driver;

        [SetUp]
        public void Init()
        {
            driver = new FirefoxDriver();
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
        }

        [Test]
        public void SuccessfulLogInWithValidCredentials()
        {
            LogInPage loginpage = new LogInPage(driver);

            loginpage.LoadPage();

            String username = Environment.GetEnvironmentVariable("HUDL_USERNAME", EnvironmentVariableTarget.User);
            String password = Environment.GetEnvironmentVariable("HUDL_PASSWORD", EnvironmentVariableTarget.User);

            loginpage.enterUsername(username);
            loginpage.enterPassword(password);
            HomePage homepage = loginpage.ClickLoginButton<HomePage>();

            Assert.IsTrue(homepage.HomePageLogoIsVisible());
        }

        [Test]
        public void LogInFailsWithMissingPassword()
        {
            LogInPage loginpage = new LogInPage(driver);

            loginpage.LoadPage();

            String username = Environment.GetEnvironmentVariable("HUDL_USERNAME", EnvironmentVariableTarget.User);

            loginpage.enterUsername(username);

            loginpage = loginpage.ClickLoginButton<LogInPage>();

            Assert.IsTrue(loginpage.ErrorMessageIsVisible());
        }

        [Test]
        public void LogInFailsWithMissingUserName()
        {
            LogInPage loginpage = new LogInPage(driver);

            loginpage.LoadPage();

            String password = Environment.GetEnvironmentVariable("HUDL_PASSWORD", EnvironmentVariableTarget.User);

            loginpage.enterPassword(password);

            loginpage = loginpage.ClickLoginButton<LogInPage>();

            Assert.IsTrue(loginpage.ErrorMessageIsVisible());
        }

        [Test]
        public void LogInFailsWithBothFieldsEmpty()
        {
            LogInPage loginpage = new LogInPage(driver);

            loginpage.LoadPage();

            loginpage = loginpage.ClickLoginButton<LogInPage>();

            Assert.IsTrue(loginpage.ErrorMessageIsVisible());
        }

        [Test]
        public void LogInFailsWithIncorrectPassword()
        {
            LogInPage loginpage = new LogInPage(driver);

            loginpage.LoadPage();

            String username = Environment.GetEnvironmentVariable("HUDL_USERNAME", EnvironmentVariableTarget.User);
            String password = "WRONG_PASSWORD";

            loginpage.enterUsername(username);
            loginpage.enterPassword(password);
            loginpage = loginpage.ClickLoginButton<LogInPage>();

            Assert.IsTrue(loginpage.ErrorMessageIsVisible());
        }

        [Test]
        public void AnUnauthenicatedUserCannotViewHomePage()
        {
            HomePage homepage = new HomePage(driver);

            LogInPage loginPage = homepage.LoadPage<LogInPage>();

            Assert.AreEqual(driver.Url, "https://www.hudl.com/login?forward=/home");
        }

    }
}
