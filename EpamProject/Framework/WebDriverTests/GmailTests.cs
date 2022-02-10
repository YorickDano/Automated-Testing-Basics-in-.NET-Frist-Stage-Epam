using Epam.WebDriver.Model;
using NUnit.Framework;
using EpamProject.WebDriver.Driver;
using EpamProject.WebDriver.Utilities.UserCreator;

namespace Tests
{
    public class GmailTests : BaseTest
    {
        [SetUp]
        public new void Initialize()
        {
            base.Initialize();
        }

        [Test]
        [Category("Smoke")]
        public void TestsOnLetterMassege()
        {
            var expectedMessage = "Message";
            User user = UserCreator.GmailUserWithCorrectData();
            new Epam.WebDriver.MailRu.Pages.LoginPage(Driver).Login(new User("epamwebdrivertest2@mail.ru", ""))
                .SendLetter(user.GetUserMail(), "topic", expectedMessage);

            DriverSingleton.CloseDriver();
            Driver = DriverSingleton.GetWebDriver();
            var actualmessage = new Epam.WebDriver.Gmail.Pages.LoginPage(Driver).Login(user).GetTextFromNewLetter();
            Assert.AreEqual(expectedMessage, actualmessage);
        }

        [Test]
        [Category("Smoke")]
        public void TestsOnNicknameChange()
        {
            var expectedNickname = "zxcGhoul";
            var user = UserCreator.GmailUserWithCorrectData();
            var Mail = new Epam.WebDriver.Gmail.Pages.LoginPage(Driver).Login(user).ChangeNickname(expectedNickname);
            Driver.Navigate().Refresh();
            var actualNickname = Mail.GetNicknameOfAccaount();
            Assert.AreEqual(expectedNickname, actualNickname);
        }

        [TearDown]
        public new void TearDown()
        {
            base.TearDown();
        }
    }
}
