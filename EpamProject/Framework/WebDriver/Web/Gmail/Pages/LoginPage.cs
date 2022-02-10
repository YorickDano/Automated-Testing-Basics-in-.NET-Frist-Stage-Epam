using Epam.WebDriver.Model;
using OpenQA.Selenium;
using Utilities;
using System.Runtime.CompilerServices;
using SeleniumExtras.WaitHelpers;

[assembly: InternalsVisibleTo("Tests")]

namespace Epam.WebDriver.Gmail.Pages
{
    class LoginPage : AbstractPage
    {
        private const string Url = "https://accounts.google.com/signin/v2/identifier?continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&service=mail&sacu=1&rip=1&flowName=GlifWebSignIn&flowEntry=ServiceLogin";
        private By MailFieldLocator = By.Name("identifier");
        private By GoNextButtonLocator = By.ClassName("VfPpkd-LgbsSe-OWXEXe-k8QpJ");
        private By PasswordFieldLocator = By.Name("password");
        private By LoginButtonLocator = By.XPath("//button[@jscontroller='soHxf']");
        private By LetterWriteButtonLocator = By.ClassName("T-I-KE");

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

        public void InitializeFirstStage()
        {
            MailFieldElement = Wait.Until(ExpectedConditions.ElementToBeClickable(MailFieldLocator));
            GoNextButtonElement = Driver.FindElement(GoNextButtonLocator);
        }

        public void InitializeSecondStage()
        {
            PasswordFieldElement = Wait.Until(ExpectedConditions.ElementToBeClickable(PasswordFieldLocator));
            LoginButtonElement = Wait.Until(ExpectedConditions.ElementIsVisible(LoginButtonLocator));
        }

        public void WaitForLogin()
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
