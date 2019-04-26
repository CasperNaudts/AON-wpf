using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using SuperChat.Business;
using SuperChat.Data;
using SuperChat.Domain;

namespace SuperChatFramework
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow
    {
        private SuperChatContext context;
        public RegisterWindow()
        {
            context = new SuperChatContext();
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var usersWithSameName = context.Users.Where(u => u.Name == UsernameTextbox.Text).ToList();
            if(usersWithSameName.Count > 0)
            {
                MessageBox.Show("De gebruikersnaam is al in gebruik");
                return;
            }

            if (PasswordBox.Password != PasswordcheckBox.Password)
            {
                MessageBox.Show("De twee wachtwoorden komen niet overeen");
                return;
            }

            var user = new User();
            user.Name = UsernameTextbox.Text;
            user.Salt = Guid.NewGuid().ToString();
            user.Password = Hash.HashInput(PasswordBox.Password, user.Salt);

            CspParameters cp = new CspParameters();
            cp.KeyContainerName = "SuperChat" + user.Name;

            var rsa = new RSACryptoServiceProvider(cp);
            user.PublicKey = rsa.ToXmlString(false);

            context.Users.Add(user);
            context.SaveChanges();

            ChatListWindow window = new ChatListWindow(rsa, user);
            window.Show();
            Close();
        }
    }
}
