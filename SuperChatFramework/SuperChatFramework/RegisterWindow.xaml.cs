using System.Security.Cryptography;
using System.Windows;
using SuperChat.Data;
using SuperChat.Domain;

namespace SuperChatFramework
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == PasswordcheckBox.Password)
            {
                var user = new User();
                user.Name = UsernameTextbox.Text;
                user.Password = PasswordBox.Password;

                CspParameters cp = new CspParameters();
                cp.KeyContainerName = "SuperChat" + user.Name;

                var RSA = new RSACryptoServiceProvider(cp);
                user.PublicKey = RSA.ToXmlString(false);

                SuperChatContext context = new SuperChatContext();
                context.Users.Add(user);
                context.SaveChanges();

                ChatListWindow window = new ChatListWindow(RSA, user);
                window.Show();
                Close();
            }
            else
            {
                //wachtwoord niet dubbel gelijk
            }
        }
    }
}
