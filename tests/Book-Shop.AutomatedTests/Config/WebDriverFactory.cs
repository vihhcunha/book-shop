using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Book_Shop.AutomatedTests.Config
{
    public class WebDriverFactory
    {
        public static IWebDriver CreateChromeWebDriver(string driverPath, bool headless)
        {
            var options = new ChromeOptions();
            if (headless)
            {
                options.AddArgument("--headless");
            }
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--allow-running-insecure-content");

            return new ChromeDriver(driverPath, options);
        }
    }
}
