using OpenQA.Selenium;
using System;
using NLog;
using System.IO;
using NUnit.Framework;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]

namespace Utilities
{
    public class Utilies
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public static void MakeScreenShot(IWebDriver driver)
        {
            try
            {
                string nameForScreenshot = GetNameForScreenshot();
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string screenshotFile = Path.Combine($@"{TestContext.CurrentContext.WorkDirectory}\logs\screenshots",
                 $"{nameForScreenshot}");
                screenshot.SaveAsFile(screenshotFile, ScreenshotImageFormat.Jpeg);

                TestContext.AddTestAttachment(screenshotFile, "Screenshot made");
                logger.Info($"Screenshot with name: {nameForScreenshot.Replace(".jpg", "")} was made");
            }
            catch
            {
                logger.Error("Screenshot didn't make, something wrong");
            }

        }

        private static string GetNameForScreenshot() => DateTime.Now.ToLongTimeString().Replace(":", "-") + " " + DateTime.Now.ToLongDateString() + ".jpg";
    }
}
