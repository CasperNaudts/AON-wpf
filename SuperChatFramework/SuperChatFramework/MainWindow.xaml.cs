using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Media;
using SuperChat.Business;
using SuperChat.Data;
using SuperChat.Domain;
using Key = System.Windows.Input.Key;

namespace SuperChatFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow();
            window.Show();
            Close();
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void Login()
        {
            SuperChatContext context = new SuperChatContext();
            if (UsernameTextBox.Text == "" || PasswordPasswordBox.Password == "")
            {
                UsernameTextBox.BorderBrush = UsernameTextBox.Text == ""
                    ? new SolidColorBrush(Colors.Red)
                    : new SolidColorBrush(Colors.DarkGray);
                PasswordPasswordBox.BorderBrush = PasswordPasswordBox.Password == ""
                    ? new SolidColorBrush(Colors.Red)
                    : new SolidColorBrush(Colors.DarkGray);

                return;
            }

            User user = context.Users.First(u => u.Name == UsernameTextBox.Text.ToLower());
            if (Hash.HashInput(PasswordPasswordBox.Password, user.Salt) != user.Password)
            {
                MessageBox.Show("foutieve inloggegevens");
                UsernameTextBox.Text = "";
                PasswordPasswordBox.Password = "";

                return;
            }

            CspParameters cp = new CspParameters();
            cp.KeyContainerName = "superChat" + user.Name;

            var rsa = new RSACryptoServiceProvider(cp);

            ChatListWindow window = new ChatListWindow(rsa, user);
            window.Show();
            Close();

        }
    }
}
