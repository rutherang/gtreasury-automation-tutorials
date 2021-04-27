using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

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
            // Arrange
            _driver.Navigate().GoToUrl(sauceDemoSite);
            IWebElement username = _driver.FindElement(By.Id("user-name"));
            IWebElement password = _driver.FindElement(By.Id("password"));
            IWebElement loginButton = _driver.FindElement(By.Id("login-button"));

            // Act
            username.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            loginButton.Click();

            // Assert
            IWebElement productsHeader = _driver.FindElement(By.CssSelector("span.title"));
            productsHeader.Text.Should().Be("PRODUCTS");
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
        }

        [TestMethod]
        public void Shop_Add2ItemsToCart_CartHas2Items()
        {
            // Arrange
            _driver.Navigate().GoToUrl(sauceDemoSite);
            IWebElement username = _driver.FindElement(By.Id("user-name"));
            IWebElement password = _driver.FindElement(By.Id("password"));
            IWebElement loginButton = _driver.FindElement(By.Id("login-button"));

            // Act
            username.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            loginButton.Click();

            // we want to add these items to cart
            IWebElement backpack = _driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
            IWebElement bikeLight = _driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light"));
            backpack.Click();
            bikeLight.Click();
            // Assert
            // We want to check that the cart has 2 items
            IWebElement cartItemsNumber = _driver.FindElement(By.CssSelector(".shopping_cart_badge"));
            cartItemsNumber.Text.Should().Be("2");
        }

        [TestMethod]
        public void VRLogin_Login_ReturnsHomepage()
        {
            _driver.Navigate().GoToUrl("https://localhost");

            IWebElement username = _driver.FindElement(By.Id("Username"));
            username.Clear();
            username.SendKeys("sa");
            IWebElement password = _driver.FindElement(By.Id("Password"));
            password.Clear();
            password.SendKeys("vr");

            IWebElement loginButton = _driver.FindElement(By.Id("login--login"));
            loginButton.Click();

            IList<IWebElement> widgets = _driver.FindElements(By.CssSelector(".widget-host--header-text")).ToList();
            var commonDataWidget = widgets.FirstOrDefault(widget => widget.Text.Contains("COMMON DATA"));
            commonDataWidget.Should().NotBeNull();
            commonDataWidget.Text.Should().Be("COMMON DATA");

            IWebElement vrHomepage = _driver.FindElement(By.CssSelector(".vr-home-page"));
            vrHomepage.Text.Should().Contain("COMMON DATA");
        }

        [TestCleanup]
        public void cleanup()
        {
            _driver.Quit();
        }
    }
}
