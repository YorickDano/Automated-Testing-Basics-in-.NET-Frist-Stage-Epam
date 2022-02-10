using EpamProject.WebDriver.Driver;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Utilities;

namespace Tests
{
    public class BaseTest
    {
        protected IWebDriver Driver;

        public void Initialize()
        {
            Driver = DriverSingleton.GetWebDriver();
        }

        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                Utilies.MakeScreenShot(Driver);
                Utilies.logger.Info($"Test {TestContext.CurrentContext.Test.Name} faild");
            }

            DriverSingleton.CloseDriver();
        }
    }
}
