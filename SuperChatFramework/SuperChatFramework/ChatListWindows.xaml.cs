using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;
using SuperChat.Data;
using SuperChat.Domain;

namespace SuperChatFramework
{
    /// <summary>
    /// Interaction logic for ChatListWindows.xaml
    /// </summary>
    public partial class ChatListWindows : Window
    {
        private RSACryptoServiceProvider _privateKey;
        private User _logedInUser;

        public ChatListWindows(RSACryptoServiceProvider privateKey, User logedInUser)
        {
            _privateKey = privateKey;
            _logedInUser = logedInUser;
            InitializeComponent();

            SuperChatContext context = new SuperChatContext();

            var chats = context.Chats.ToList();
            DataGrid.ItemsSource = chats;
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Chat selectedChat = (Chat) sender;
        }

        private void NieuweChatButton_Click(object sender, RoutedEventArgs e)
        {
            NewChatWindow window = new NewChatWindow(_logedInUser);
            window.Show();

        }
    }
}
