using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using HudlLogInTests.PageObjects;


namespace HudlLogInTests.TestCases
{
    class LoginTestCases
    {
        [Test]
        public void SuccessfulLogInWithValidCredentials()
        {
            IWebDriver driver = new FirefoxDriver();

            LogInPage loginpage = new LogInPage(driver);

            loginpage.LoadPage();

            String username = Environment.GetEnvironmentVariable("HUDL_USERNAME", EnvironmentVariableTarget.User);
            String password = Environment.GetEnvironmentVariable("HUDL_PASSWORD", EnvironmentVariableTarget.User);

            loginpage.enterUsername(username);
            loginpage.enterPassword(password);
            HomePage homepage = loginpage.ClickLoginButton<HomePage>();

            Assert.IsTrue(homepage.HomePageLogoIsVisible());

            driver.Quit();

        }

        [Test]
        public void LogInFailsWithMissingPassword()
        {
            IWebDriver driver = new FirefoxDriver();

            LogInPage loginpage = new LogInPage(driver);

            loginpage.LoadPage();

            String username = Environment.GetEnvironmentVariable("HUDL_USERNAME", EnvironmentVariableTarget.User);

            loginpage.enterUsername(username);

            loginpage = loginpage.ClickLoginButton<LogInPage>();

            Assert.IsTrue(loginpage.ErrorMessageIsVisible());

            driver.Quit();
        }

        [Test]
        public void LogInFailsWithMissingUserName()
        {
            IWebDriver driver = new FirefoxDriver();

            LogInPage loginpage = new LogInPage(driver);

            loginpage.LoadPage();

            String password = Environment.GetEnvironmentVariable("HUDL_PASSWORD", EnvironmentVariableTarget.User);

            loginpage.enterPassword(password);

            loginpage = loginpage.ClickLoginButton<LogInPage>();

            Assert.IsTrue(loginpage.ErrorMessageIsVisible());

            driver.Quit();
        }

        [Test]
        public void LogInFailsWithBothFieldsEmpty()
        {
            IWebDriver driver = new FirefoxDriver();

            LogInPage loginpage = new LogInPage(driver);

            loginpage.LoadPage();

            loginpage = loginpage.ClickLoginButton<LogInPage>();

            Assert.IsTrue(loginpage.ErrorMessageIsVisible());

            driver.Quit();
        }

        [Test]
        public void LogInFailsWithIncorrectPassword()
        {
            IWebDriver driver = new FirefoxDriver();

            LogInPage loginpage = new LogInPage(driver);

            loginpage.LoadPage();

            String username = Environment.GetEnvironmentVariable("HUDL_USERNAME", EnvironmentVariableTarget.User);
            String password = "WRONG_PASSWORD";

            loginpage.enterUsername(username);
            loginpage.enterPassword(password);
            loginpage = loginpage.ClickLoginButton<LogInPage>();

            Assert.IsTrue(loginpage.ErrorMessageIsVisible());

            driver.Quit();
        }
        [Test]
        public void AnUnauthenicatedUserCannotViewHomePage()
        {
            IWebDriver driver = new FirefoxDriver();

            HomePage homepage = new HomePage(driver);

            LogInPage loginPage = homepage.LoadPage<LogInPage>();

            Assert.AreEqual(driver.Url, "https://www.hudl.com/login?forward=/home");

            driver.Quit();
        }

    }
}
