using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace Book_Shop.AutomatedTests.Config
{
    public class SeleniumHelper : IDisposable
    {
        public IWebDriver WebDriver;
        public readonly ConfigurationHelper Configuration;
        public WebDriverWait Wait;

        public SeleniumHelper(ConfigurationHelper configuration, bool headless = true)
        {
            Configuration = configuration;
            WebDriver = WebDriverFactory.CreateChromeWebDriver(Configuration.WebDriversFolder, headless);
            WebDriver.Manage().Window.Maximize();
            Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30));
        }

        public string GetUrl()
        {
            return WebDriver.Url;
        }

        public void GoToUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public bool ValidateContentUrl(string content)
        {
            return Wait.Until(ExpectedConditions.UrlContains(content));
        }

        public void ClickOnLinkByText(string linkText)
        {
            var link = Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(linkText)));
            link.Click();
        }

        public void ClickOnButtonById(string buttonId)
        {
            var button = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(buttonId)));
            button.Click();
        }

        public void ClickByXPath(string xPath)
        {
            var element = Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xPath)));
            element.Click();
        }

        public IWebElement GetElementByClass(string classeCss)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(classeCss)));
        }

        public IWebElement GetElementByXPath(string xPath)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xPath)));
        }

        public IWebElement GetElementByXPathWaitingForTitle(string xPath, string title)
        {
            Wait.Until(ExpectedConditions.TitleContains(title));
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xPath)));
        }

        public void FillTextBoxById(string idField, string fieldValue)
        {
            var field = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(idField)));
            field.Clear();
            field.SendKeys(fieldValue);
            BlurElement(idField);
        }

        public void BlurElement(string idElement)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)WebDriver;
            js.ExecuteScript($"document.querySelector('#{idElement}').blur()");
        }

        public void FillDropDownById(string idField, string fieldValue)
        {
            var field = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(idField)));
            var selectElement = new SelectElement(field);
            selectElement.SelectByValue(fieldValue);
        }

        public string GetElementTextByCssClass(string className)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(className))).Text;
        }

        public string GetElementTextById(string id)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id))).Text;
        }

        public string GetTextBoxValueById(string id)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)))
                .GetAttribute("value");
        }

        public IEnumerable<IWebElement> GetListByClass(string className)
        {
            return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName(className)));
        }

        public bool CheckIfElementExistsById(string id)
        {
            return ElementExits(By.Id(id));
        }

        public void BackNavigation(int times = 1)
        {
            for (var i = 0; i < times; i++)
            {
                WebDriver.Navigate().Back();
            }
        }

        private bool ElementExits(By by)
        {
            try
            {
                WebDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void Dispose()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }
    }
}
