using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using Utilities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
namespace Epam.WebDriver.Gmail.Pages
{
    class MainPage : AbstractPage
    {
        private string Url = "https://mail.google.com/mail/u/0/#inbox";
        private string UrlForChangeNickname = "https://myaccount.google.com/name?gar=1&pli=1&rapt=AEjHL4PYDWb6Wyt4vyTME6cnl40top8HnuzSgQxVRuB5mKv01cr1yRXfscJy_8cNXWAlsn7iPW5XSwEx3Yw7_u4FIOcQKJHPvQ";

        private By WriteLetterButtonLocator = By.XPath("//div[@gh='cm']");
        private By NicknameLocator = By.ClassName("gb_Ka");

        private By MailFieldLocator = By.Name("to");
        private By TopicFieldLocator = By.Name("subjectbox");
        private By MessageFieldLocator = By.ClassName("LW-avf");
        private By LetterSendButonLocator = By.ClassName("aoO");
        private By LetterSendedLocator = By.XPath(@"//span[@class='bAq']");

        private By NewLetterLocator = By.ClassName("zE");
        private By LetterTextLocator = By.XPath("//div[@class='a3s aiL ']//div[not(@territory='true')]/div[not(@territory='true')]");

        private By NicknameChangeFieldLocator = By.ClassName("VfPpkd-fmcmS-wGMbrd");
        private By SubmitNicknameChangeLocator = By.XPath("//button[@jsname='Pr7Yme']");

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

        private void InitializeOnLogin()
        {
            WriteLetterButtonElement = Wait.Until(ExpectedConditions.ElementToBeClickable(WriteLetterButtonLocator));
            NicknameElement = Wait.Until(ExpectedConditions.ElementIsVisible(NicknameLocator));
        }

        private void InitializeOnLetterWrite()
        {
            MailFieldElement = Wait.Until(ExpectedConditions.ElementToBeClickable(MailFieldLocator));
            TopicFieldElement = Driver.FindElement(TopicFieldLocator);
            TextFieldElement = Driver.FindElement(MessageFieldLocator);
            SendLetterButtonElement = Driver.FindElement(LetterSendButonLocator);
        }

        private void InitializeOnNicknameChange()
        {
            NicknameFieldElement = Wait.Until(ExpectedConditions.ElementToBeClickable(NicknameChangeFieldLocator));
            SubmitNicknameChangeButtonElement = Driver.FindElement(SubmitNicknameChangeLocator);
        }

        private void InitializeOnNewLetter() => NewLetterElement = Wait.Until(ExpectedConditions.ElementToBeClickable(NewLetterLocator));

        private void InitializeOnGetLetterText() => LetterTextElement = Wait.Until(ExpectedConditions.ElementToBeClickable(LetterTextLocator));

        private void WaitForLetterSended()
        {
            var defaultWait = new DefaultWait<IWebDriver>(Driver);
            defaultWait.Timeout = TimeSpan.FromSeconds(10);
            defaultWait.Until(ExpectedConditions.TextToBePresentInElementLocated(LetterSendedLocator, "Сообщение отправлено."));
        }

        public MainPage SendLetter(string mailTo, string topic, string text)
        {
            InitializeOnLogin();
            WriteLetterButtonElement.Click();

            InitializeOnLetterWrite();
            MailFieldElement.SendKeys(mailTo);
            TopicFieldElement.SendKeys(topic);
            TextFieldElement.SendKeys(text);
            SendLetterButtonElement.Click();

            WaitForLetterSended();

            Utilies.logger.Info($@"User send message on {mailTo} with message ""{text}"" ");

            return this;
        }

        public string GetTextFromNewLetter()
        {
            InitializeOnNewLetter();
            NewLetterElement.Click();

            InitializeOnGetLetterText();
            string text = LetterTextElement.Text;

            Driver.Navigate().Back();

            return text.Replace("\r\n  ", "");
        }

        public MainPage ChangeNickname(string newNickname)
        {
            string previousNickname = GetNicknameOfAccaount();
            Driver.Navigate().GoToUrl(UrlForChangeNickname);

            InitializeOnNicknameChange();
            NicknameFieldElement.Clear();
            NicknameFieldElement.SendKeys(newNickname);
            SubmitNicknameChangeButtonElement.Click();

            Driver.Navigate().Back();

            Utilies.logger.Info($@"User changed nickname on ""{GetNicknameOfAccaount()}"" previous was ""{previousNickname}"" ");

            return this;
        }

        public string GetNicknameOfAccaount()
        {
            InitializeOnLogin();

            return NicknameElement.GetAttribute("aria-label").Replace("Аккаунт Google: ", "")
                .Replace("  \r\n(epamwebdrivertest1@gmail.com)", "");
        }
    }
}
