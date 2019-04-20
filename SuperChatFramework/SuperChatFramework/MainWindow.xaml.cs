using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Media;
using SuperChat.Data;
using SuperChat.Domain;

namespace SuperChatFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
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

            User user = context.Users.Where(u => u.Name == UsernameTextBox.Text).First();

            if (user == null)
            {
                return;
            }

            CspParameters cp = new CspParameters();
            cp.KeyContainerName = "SuperChat";

            var rsa = new RSACryptoServiceProvider(cp);

            ChatListWindows window = new ChatListWindows(rsa);
            window.Show();
            Close();

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow();
            window.Show();
            Close();
        }
    }
}
