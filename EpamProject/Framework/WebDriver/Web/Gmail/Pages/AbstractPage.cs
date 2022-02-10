using OpenQA.Selenium;
using System;
using System.Runtime.CompilerServices;
using OpenQA.Selenium.Support.UI;

[assembly: InternalsVisibleTo("Tests")]
namespace Epam.WebDriver.Gmail.Pages
{
    abstract class AbstractPage
    {
        protected IWebDriver Driver;

        protected WebDriverWait Wait;

        protected AbstractPage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(90));
            Wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            Wait.IgnoreExceptionTypes(typeof(UnhandledAlertException));
        }
    }
}
