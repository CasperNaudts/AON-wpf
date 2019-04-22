using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using SuperChat.Data;
using SuperChat.Domain;
using Key = SuperChat.Domain.Key;

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

            var loggedInUserKeyLijst = context.Keys.Where(key => key.User == logedInUser).ToList();
            var chatLijst = new List<Chat>();

            foreach (var key in loggedInUserKeyLijst)
            {
                var chat = context.Chats.Find(key.ChatId);
                chatLijst.Add(chat);
            }

            var otherUserInChatList = new List<Key>();

            foreach (var chat in chatLijst)
            {
                var sleutels = context.Keys.Where(key => key.ChatId == chat.Id);
                var var = sleutels.First(key => key.UserId != logedInUser.Id);
                otherUserInChatList.Add(var);
            }

            foreach (var key in otherUserInChatList)
            {
                var item = new ListBoxItem();
                item.Content = context.Users.Find(key.UserId);
                item.MouseDoubleClick += new MouseButtonEventHandler(listBoxItem_MouseDoubleClick);

                ChatToUsersListView.Items.Add(item);
            }

        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Chat selectedChat = (Chat)sender;
        }

        private void NieuweChatButton_Click(object sender, RoutedEventArgs e)
        {
            NewChatWindow window = new NewChatWindow(_logedInUser);
            window.Show();

        }

        void listBoxItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var selectedChat = (Chat) sender;
        }
    }
}
