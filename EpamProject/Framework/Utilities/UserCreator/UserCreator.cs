using Epam.WebDriver.Model;
using EpamProject.WebDriver.Utilities.DataWorker;
using System;

namespace EpamProject.WebDriver.Utilities.UserCreator
{
    class UserCreator
    {
        private static string userMailMailRu = "testdata.mailru.user.mail";
        private static string userPasswordMailRu = "testdata.mailru.user.password";
        private static string userMailGmail = "testdata.gmail.user.mail";
        private static string userPasswordGmail = "testdata.gmail.user.password";

        public static User MailRuUserWithCorrectData()
        {
            return new User(DataReader.GetTestData(userMailMailRu), DataReader.GetTestData(userPasswordMailRu));
        }

        public static User GmailUserWithCorrectData()
        {
            return new User(DataReader.GetTestData(userMailGmail), DataReader.GetTestData(userPasswordGmail));
        }

        public static User UserWithEmptyMail()
        {
            return new User(string.Empty, DataReader.GetTestData(userPasswordGmail));
        }

        public static User UserWithEmptyPassword()
        {
            return new User(DataReader.GetTestData(userMailGmail), string.Empty);
        }

        public static User UserWithInvalidMail()
        {
            return new User(DataReader.GetTestData(userMailGmail).Replace("@", ""), DataReader.GetTestData(userPasswordGmail));
        }

        public static User UserWithInvalidPassword()
        {
            return new User(DataReader.GetTestData(userMailGmail), DataReader.GetTestData(userPasswordGmail) + (char)new Random().Next('a', 'z'));
        }
    }
}
