using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace EpamProject.WebDriver.Driver
{
    class DriverSingleton
    {
        private static IWebDriver Driver;
        private DriverSingleton() { }

        public static IWebDriver GetWebDriver()
        {
            if (Driver == null)
            {
                switch (TestContext.Parameters["browser"])
                {
                    case "Firefox":
                        {
                            var options = new FirefoxOptions();
                            options.AddArgument("no-sandbox");
                            options.AddArgument("--disable-popup-blocking");
                            new DriverManager().SetUpDriver(new ChromeConfig());
                            Driver = new FirefoxDriver(options);
                            break;
                        }
                    default:
                        {
                            var options = new ChromeOptions();
                            options.AddArgument("no-sandbox");
                            options.AddArgument("--disable-popup-blocking");
                            new DriverManager().SetUpDriver(new ChromeConfig());
                            Driver = new ChromeDriver(options);
                            break;
                        }
                }
                Driver.Manage().Window.Maximize();
            }
            return Driver;
        }

        public static void CloseDriver()
        {
            Driver.Quit();
            Driver = null;
        }
    }
}
