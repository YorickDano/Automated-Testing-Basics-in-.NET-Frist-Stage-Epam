using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Linq;
using Utilities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
namespace Epam.WebDriver.MailRu.Pages
{
    class MainPage : AbstractPage
    {
        private string Url = "https://e.mail.ru/inbox/";
        private string UrlSettings = "https://id.mail.ru/profile?utm_campaign=mailid&utm_medium=ph&from=headline";

        private By NicknameLocator = By.ClassName("input-0-2-133");

        private By WriteLetterButtonLocator = By.ClassName("compose-button_base");

        private By MailFieldLocator = By.ClassName("container--H9L5q");
        private By TopicFieldLocator = By.Name("Subject");
        private By MessageFieldLocator = By.ClassName("cke_contents_true");
        private By LetterSendButonLocator = By.ClassName("button2_primary");

        private By NewLetterLocator = By.ClassName("ll-rs_is-active");
        private By AllLetterLocator = By.ClassName("llc_normal");
        private By LetterTextLocator = By.XPath("//div[@dir='ltr']");

        private By NicknameChangeFieldLocator = By.ClassName("input-0-2-133");
        private By SubmitNicknameChangeLocator = By.ClassName("primary-0-2-103");
        private By LetterSenderLocator = By.ClassName("letter-contact");

        private IWebElement WriteLetterButtonElement;
        private IWebElement NicknameElement;

        private IWebElement NicknameFieldElement;
        private IWebElement SubmitNicknameChangeButtonElement;

        private IWebElement MailFieldElement;
        private IWebElement TopicFieldElement;
        private IWebElement TextFieldElement;
        private IWebElement SendLetterButtonElement;

        private IWebElement NewLetterElement;
        private IWebElement LetterTextElement;
        private IWebElement LetterSenderElement;

        public MainPage(IWebDriver driver) : base(driver)
        {
            OpenPage();
        }

        public MainPage OpenPage()
        {
            Driver.Navigate().GoToUrl(Url);
            Utilies.logger.Info($"Openned page with {Url} url");

            return this;
        }

        public void InitializeOnLogin()
        {
            WriteLetterButtonElement = Wait.Until(ExpectedConditions.ElementToBeClickable(WriteLetterButtonLocator));
        }

        public void InitializeOnLetterWrite()
        {
            MailFieldElement = Wait.Until(ExpectedConditions.ElementToBeClickable(MailFieldLocator));
            TopicFieldElement = Driver.FindElement(TopicFieldLocator);
            TextFieldElement = Driver.FindElement(MessageFieldLocator);
            SendLetterButtonElement = Driver.FindElement(LetterSendButonLocator);
        }

        public void InitializeOnNicknameChange()
        {
            NicknameFieldElement = Wait.Until(ExpectedConditions.ElementToBeClickable(NicknameChangeFieldLocator));
            SubmitNicknameChangeButtonElement = Driver.FindElement(SubmitNicknameChangeLocator);
        }
        public void InitializeOnNewLetter() => NewLetterElement = Wait.Until(ExpectedConditions.ElementToBeClickable(NewLetterLocator));

        public void InitializeOnInsideLetterText()
        {
            LetterTextElement = Wait.Until(ExpectedConditions.ElementToBeClickable(LetterTextLocator));
            LetterSenderElement = Wait.Until(ExpectedConditions.ElementToBeClickable(LetterSenderLocator));
        }

        public void InitializeOnGetNickname() => NicknameElement = Wait.Until(ExpectedConditions.ElementToBeClickable(NicknameLocator));

        public MainPage SendLetter(string mailTo, string topic, string text)
        {
            InitializeOnLogin();
            WriteLetterButtonElement.Click();

            InitializeOnLetterWrite();
            MailFieldElement.SendKeys(mailTo);
            TopicFieldElement.SendKeys(topic);
            TextFieldElement.SendKeys(text);
            SendLetterButtonElement.Click();

            Utilies.logger.Info($@"User send message on {mailTo} with message ""{text}"" ");

            return this;
        }

        public void WaitForNewLetter()
        {
            InitializeOnNewLetter();
        }

        public string GetMassegeFromNewLetter()
        {
            WaitForNewLetter();
            var allLetters = Driver.FindElements(AllLetterLocator).ToList();
            NewLetterElement = allLetters.Find(x => x.FindElement(NewLetterLocator) != null);
            NewLetterElement.Click();
            InitializeOnInsideLetterText();

            return LetterTextElement.Text; ;
        }

        public string GetLetterSender()
        {
            WaitForNewLetter();
            var allLetters = Driver.FindElements(AllLetterLocator).ToList();
            NewLetterElement = allLetters.Find(x => x.FindElement(NewLetterLocator) != null);
            NewLetterElement.Click();
            InitializeOnInsideLetterText();
            return LetterSenderElement.Text;
        }

        public MainPage ChangeNickname(string nickname)
        {
            Driver.Navigate().GoToUrl(UrlSettings);

            InitializeOnNicknameChange();
            NicknameFieldElement.Clear();
            NicknameFieldElement.SendKeys(nickname);
            SubmitNicknameChangeButtonElement.Click();

            Driver.Navigate().Back();

            return this;
        }

        public string GetNickname()
        {
            Driver.Navigate().GoToUrl(UrlSettings);
            InitializeOnGetNickname();
            var nickname = NicknameElement.GetAttribute("value");
            Driver.Navigate().Back();

            return nickname;
        }
    }
}
