using Epam.WebDriver.Model;
using NUnit.Framework;
using EpamProject.WebDriver.Driver;
using EpamProject.WebDriver.Utilities.UserCreator;
using Epam.WebDriver.MailRu.Pages;

namespace Tests
{
    public class MailruTests : BaseTest
    {
        [SetUp]
        public new void Initialize()
        {
            base.Initialize();
        }

        [Test]
        [Category("Smoke")]
        public void TestsOnNicknameChange()
        {
            var newNickname = "ZXC";
            User user = UserCreator.GmailUserWithCorrectData();
            var nickname = new LoginPage(Driver).Login(new User("epamwebdrivertest2@mail.ru", ""))
                .GetNickname();
            var actualNickname = new MainPage(Driver).ChangeNickname(newNickname).GetNickname();
            Assert.AreEqual(nickname, actualNickname);
        }

        [Test]
        [Category("Smoke")]
        public void TestsOnLetterMassege()
        {
            var expectedText = "zxcGhoul";
            var user = UserCreator.GmailUserWithCorrectData();
            var Mail = new Epam.WebDriver.Gmail.Pages.LoginPage(Driver).Login(user).SendLetter("epamwebdrivertest2@mail.ru", "topic", expectedText);
            DriverSingleton.CloseDriver();
            Driver = DriverSingleton.GetWebDriver();
            var actualText = new LoginPage(Driver).Login(new User("epamwebdrivertest2@mail.ru", "")).GetMassegeFromNewLetter();
            Assert.AreEqual(expectedText, actualText);
        }

        [TearDown]
        public new void TearDown()
        {
            base.TearDown();
        }
    }
}
