using System;
using OpenQA.Selenium;
using HudlLogInTests.Utils;

namespace HudlLogInTests.PageObjects
{
    public class HomePage
    {
        private IWebDriver _driver;
        private IWebElement HomePageLogo;
        private String pageURL = "https://www.hudl.com/home";

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            
        }

        public T LoadPage<T>()
        {
            _driver.Navigate().GoToUrl(pageURL);
            return (T)Activator.CreateInstance(typeof(T), new object[] { _driver });
        }

        public bool HomePageLogoIsVisible()
        {
            HomePageLogo = _driver.FindElement(By.CssSelector("[data-qa-id=webnav-globalnav-home]"), 10);
            return HomePageLogo.Displayed;
        }
    }
}
