using Epam.WebDriver.Model;
using OpenQA.Selenium;
using Utilities;
using System.Runtime.CompilerServices;
using SeleniumExtras.WaitHelpers;

[assembly: InternalsVisibleTo("Tests")]

namespace Epam.WebDriver.MailRu.Pages
{
    class LoginPage : AbstractPage
    {
        private const string Url = "https://mail.ru/";

        private By MailFieldLocator = By.Name("login");
        private By GoNextButtonLocator = By.ClassName("button");
        private By PasswordFieldLocator = By.Name("password");
        private By LoginButtonLocator = By.ClassName("second-button");
        private By LetterWriteButtonLocator = By.ClassName("compose-button_base");

        private IWebElement MailFieldElement;
        private IWebElement GoNextButtonElement;
        private IWebElement PasswordFieldElement;
        private IWebElement LoginButtonElement;

        public LoginPage(IWebDriver driver) : base(driver)
        {
            OpenPage();
        }

        public LoginPage OpenPage()
        {
            Driver.Navigate().GoToUrl(Url);
            Utilies.logger.Info($"Openned page with {Url} url");
            return this;
        }

        private void InitializeFirstStage()
        {
            MailFieldElement = Wait.Until(ExpectedConditions.ElementToBeClickable(MailFieldLocator));
            GoNextButtonElement = Driver.FindElement(GoNextButtonLocator);
        }

        private void InitializeSecondStage()
        {
            PasswordFieldElement = Wait.Until(ExpectedConditions.ElementToBeClickable(PasswordFieldLocator));
            LoginButtonElement = Wait.Until(ExpectedConditions.ElementIsVisible(LoginButtonLocator));
        }

        private void WaitForLogin()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(LetterWriteButtonLocator));
        }

        public MainPage Login(User user)
        {
            InitializeFirstStage();
            MailFieldElement.SendKeys(user.GetUserMail());
            GoNextButtonElement.Click();
            InitializeSecondStage();
            PasswordFieldElement.SendKeys(user.GetUserPassword());
            LoginButtonElement.Click();
            WaitForLogin();

            return new MainPage(Driver);
        }
    }
}
