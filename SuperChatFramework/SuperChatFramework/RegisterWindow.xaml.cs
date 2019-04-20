using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
                cp.KeyContainerName = "SuperChat";

                var RSA = new RSACryptoServiceProvider(cp);
                user.PublicKey = RSA.ToXmlString(false);

                SuperChatContext context = new SuperChatContext();
                context.Add(user);
            }
            else
            {
                //wachtwoord niet dubbel gelijk
            }
        }
    }
}
