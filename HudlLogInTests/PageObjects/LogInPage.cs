﻿using System;
using OpenQA.Selenium;
using System.Threading;
using HudlLogInTests.Utils;

namespace HudlLogInTests.PageObjects
{
    class LogInPage
    {
        private int waitSeconds = 10;
        private String pageURL = "https://www.hudl.com/login";
        private IWebDriver _driver;
        private IWebElement UsernameTextBox;
        private IWebElement PasswordTextBox;
        private IWebElement LogInButton;

        public LogInPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void LoadPage()
        {
            _driver.Navigate().GoToUrl(pageURL);
            UsernameTextBox = _driver.FindElement(By.Id("email"), waitSeconds);
            PasswordTextBox = _driver.FindElement(By.Id("password"));
            LogInButton = _driver.FindElement(By.Id("logIn"));
        }

        public void enterUsername(String text)
        {
            UsernameTextBox.SendKeys(text);
        }

        public void enterPassword(String text)
        {
            PasswordTextBox.SendKeys(text);
        }

        public T ClickLoginButton<T>()
        {
            LogInButton.Click();
            return (T)Activator.CreateInstance(typeof(T), new object[] { _driver });
        }

        public bool ErrorMessageIsVisible()
        {
            Thread.Sleep(1000);
            return _driver.FindElement(By.CssSelector("a.need-help:nth-child(1)"), 10).Displayed;
        }
    }
}
