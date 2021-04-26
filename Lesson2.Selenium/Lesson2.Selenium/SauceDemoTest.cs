using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using FluentAssertions;

namespace Lesson2.Selenium
{
    [TestClass]
    public class SauceDemoTest
    {
        IWebDriver _driver;
        private const string sauceDemoSite = "https://www.saucedemo.com/";

        public SauceDemoTest()
        {
            var cOptions = new ChromeOptions();
            cOptions.AddArguments("-no-sandbox");
            cOptions.AddArguments("ignore-certificate-errors");

            _driver = new ChromeDriver(cOptions);
            _driver.Manage().Window.Size = new System.Drawing.Size(1440, 900);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            _driver.Url = "https://www.saucedemo.com/";
        }

        [TestMethod]
        public void Login_AsStandardUser_AccessWebsite()
        {
            _driver.Navigate().GoToUrl(sauceDemoSite);
            IWebElement username = _driver.FindElement(By.Id("user-name"));
            IWebElement password = _driver.FindElement(By.Id("password"));
            IWebElement loginButton = _driver.FindElement(By.Id("login-button"));

            username.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            loginButton.Click();

            IWebElement productsHeader = _driver.FindElement(By.CssSelector("span.title"));
            productsHeader.Text.Should().Be("Products");
            _driver.Quit();
        }

        [TestMethod]
        public void Login_AsLockedOutUser_LockoutError()
        {
            _driver.Navigate().GoToUrl(sauceDemoSite);
            IWebElement username = _driver.FindElement(By.Id("user-name"));
            IWebElement password = _driver.FindElement(By.Id("password"));
            IWebElement loginButton = _driver.FindElement(By.Id("login-button"));

            username.SendKeys("locked_out_user");
            password.SendKeys("secret_sauce");
            loginButton.Click();

            IWebElement errorLabel = _driver.FindElement(By.CssSelector("[data-test='error']"));
            errorLabel.Text.Should().Be("Epic sadface: Sorry, this user has been locked out.");
            _driver.Quit();
        }

    }
}
