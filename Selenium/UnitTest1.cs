using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using Xunit;
//asserts на перевірку групи,вчителя,h1
namespace TestProject
{
    public class SuiteTests : IDisposable
    {
        public IWebDriver driver { get; private set; }
        public IDictionary<String, Object> vars { get; private set; }
        public IJavaScriptExecutor js { get; private set; }
        public SuiteTests()
        {
            driver = new ChromeDriver();
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<String, Object>();
        }
        public void Dispose()
        {
            driver.Quit();
        }
        [Fact]
        public void CheckHeader()
        {
            driver.Navigate().GoToUrl("http://develop-softserve.herokuapp.com/schedule?semester=53");
            driver.Manage().Window.Size = new System.Drawing.Size(1920, 1040);
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 1);
            driver.FindElement(By.Id("group")).Click();
            {
                var dropdown = driver.FindElement(By.Id("group"));
                dropdown.FindElement(By.XPath("//option[. = '32 (302)']")).Click();
            }
            {
                var element = driver.FindElement(By.CssSelector(".svg-btn"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Perform();
            }
            driver.FindElement(By.CssSelector(".svg-btn")).Click();
            {
                var element = driver.FindElement(By.TagName("body"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element, 0, 0).Perform();
            }
            vars["2 (01/02/2022-19/05/2022) : 32 (302) Download PDF"] = driver.FindElement(By.CssSelector("h1")).Text;
            Assert.Equal(driver.FindElement(By.CssSelector("h1")).Text, "Download PDF");
            //Assert.True(driver.FindElement(By.CssSelector("h1")).Text.Contains("32 (302)"));
        }

        [Fact]
        public void CheckGroup()
        {
            driver.Navigate().GoToUrl("http://develop-softserve.herokuapp.com/");
            driver.Manage().Window.Size = new System.Drawing.Size(844, 725);
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 1);
            {
                var element = driver.FindElement(By.LinkText("Login"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Perform();
            }
            driver.FindElement(By.Id("group")).Click();
            {
                var dropdown = driver.FindElement(By.Id("group"));
                dropdown.FindElement(By.XPath("//option[. = '32 (302)']")).Click();
            }
            driver.FindElement(By.CssSelector(".MuiButtonBase-root:nth-child(4) > .MuiButton-label")).Click();
            {
                var element = driver.FindElement(By.Id("group"));
                String value = element.GetAttribute("value");
                String locator = $"option[@value='{value}']";
                String selectedText = element.FindElement(By.XPath(locator)).Text;
                Assert.Equal(selectedText, "32 (302)");
            }
        }

        [Fact]
        public void CheckTeacher()
        {
            driver.Navigate().GoToUrl("http://develop-softserve.herokuapp.com/schedule?semester=53");
            driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 2);
            driver.FindElement(By.Id("teacher")).Click();
                var dropdown = driver.FindElement(By.Id("teacher"));
                dropdown.FindElement(By.XPath("//option[. = 'Романенко Наталія Вікторівна']")).Click();
            driver.FindElement(By.CssSelector(".MuiButtonBase-root:nth-child(4) > .MuiButton-label")).Click();
            driver.FindElement(By.Id("teacher")).Click();
                var element = driver.FindElement(By.Id("teacher"));
                String value = element.GetAttribute("value");
                String locator = $"option[@value='{value}']";
                String selectedText = element.FindElement(By.XPath(locator)).Text;
                Assert.Equal(selectedText, "Романенко Наталія Вікторівна");
        }
    }
}