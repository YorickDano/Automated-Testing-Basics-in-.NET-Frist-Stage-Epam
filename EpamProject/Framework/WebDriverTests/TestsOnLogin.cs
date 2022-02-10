using Epam.WebDriver.Gmail.Pages;
using Epam.WebDriver.Model;
using EpamProject.WebDriver.Utilities.UserCreator;
using NUnit.Framework;

namespace Tests
{
    public class TestsOnLogin : BaseTest
    {
        [SetUp]
        
        public new void Initialize()
        {
            base.Initialize();
        }

        [Test]
        [Category("All")]
        public void TestsOnLoginWithCorrectData()
        {
            User user = UserCreator.GmailUserWithCorrectData();
            var login = new LoginPage(Driver).Login(user);
            Assert.AreNotEqual(null, login);
        }

        [Test]
        [Category("All")]
        public void TestsOnLoginWithEmptyMail()
        {
            User user = UserCreator.UserWithEmptyMail();
            var login = new LoginPage(Driver).Login(user);
            Assert.AreNotEqual(null, login);
        }

        [Test]
        [Category("All")]
        public void TestsOnLoginWithEmptyPassword()
        {
            User user = UserCreator.UserWithEmptyPassword();
            var login = new LoginPage(Driver).Login(user);
            Assert.AreNotEqual(null, login);
        }

        [Test]
        [Category("All")]
        public void TestsOnLoginWithInvalidMail()
        {
            User user = UserCreator.UserWithInvalidMail();
            var login = new LoginPage(Driver).Login(user);
            Assert.AreNotEqual(null, login);
        }

        [Test]
        [Category("All")]
        public void TestsOnLoginWithInvalidPasswrod()
        {
            User user = UserCreator.UserWithInvalidPassword();
            var login = new LoginPage(Driver).Login(user);
            Assert.AreNotEqual(null, login);
        }

        [TearDown]
        public new void TearDown()
        {
            base.TearDown();
        }
    }
}
