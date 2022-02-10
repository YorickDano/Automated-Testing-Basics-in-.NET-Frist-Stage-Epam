using OpenQA.Selenium;
using System;
using System.Runtime.CompilerServices;
using OpenQA.Selenium.Support.UI;

[assembly: InternalsVisibleTo("Tests")]
namespace Epam.WebDriver.MailRu.Pages
{
    abstract class AbstractPage
    {
        protected IWebDriver Driver;

        protected WebDriverWait Wait;

        protected AbstractPage(IWebDriver driver)
        {
            Driver = driver;
            try
            {
                Driver.Manage().Window.Maximize();
            }
            catch { }
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            Wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            Wait.IgnoreExceptionTypes(typeof(UnhandledAlertException));
        }
    }
}
