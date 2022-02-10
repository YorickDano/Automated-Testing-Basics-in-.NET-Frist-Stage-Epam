using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
namespace Epam.WebDriver.Model
{
    class User
    {
        private string Mail;
        private string Password;
        public User(string mail, string password)
        {
            Mail = mail;
            Password = password;
        }
        public void SetUserNMail(string mail)
        {
            Mail = mail;
        }
        public void SetUserPassword(string password)
        {
            Password = password;
        }
        public string GetUserMail() => Mail;
        public string GetUserPassword() => Password;
        public static bool operator ==(User user1, User user2)
        {
            return user1.Mail == user2.Mail && user1.Password == user2.Password;
        }
        public static bool operator !=(User user1, User user2)
        {
            return !(user1 == user2);
        }
    }
}
