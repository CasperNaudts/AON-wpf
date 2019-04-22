using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;
using SuperChat.Data;
using SuperChat.Domain;
using Key = SuperChat.Domain.Key;

namespace SuperChatFramework
{
    /// <summary>
    /// Interaction logic for ChatListWindow.xaml
    /// </summary>
    public partial class ChatListWindow
    {
        private RSACryptoServiceProvider _privateKey;
        private User _loggedInUser;
        private List<Chat> _chats;

        public ChatListWindow(RSACryptoServiceProvider privateKey, User loggedInUser)
        {
            _privateKey = privateKey;
            _loggedInUser = loggedInUser;
            InitializeComponent();

            ChatToUsersListView.MouseDoubleClick += listBoxItem_MouseDoubleClick;

            LoadData();
        }

        private void NieuweChatButton_Click(object sender, RoutedEventArgs e)
        {
            NewChatWindow window = new NewChatWindow(_loggedInUser);
            window.Show();

        }

        private void LoadData()
        {
            SuperChatContext context = new SuperChatContext();

            var loggedInUserKeyLijst = context.Keys.Where(key => key.User == _loggedInUser).ToList();
            _chats = new List<Chat>();

            foreach (var key in loggedInUserKeyLijst)
            {
                var chat = context.Chats.Find(key.ChatId);
                _chats.Add(chat);
            }

            var otherUserInChatList = new List<Key>();

            foreach (var chat in _chats)
            {
                var sleutels = context.Keys.Where(key => key.ChatId == chat.Id);
                var var = sleutels.First(key => key.UserId != _loggedInUser.Id);
                otherUserInChatList.Add(var);
            }

            ChatToUsersListView.Items.Clear();
            foreach (var key in otherUserInChatList)
            {
                ChatToUsersListView.Items.Add(context.Users.Find(key.UserId));
            }

        }

        void listBoxItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var selectedUser = (User) ChatToUsersListView.SelectedItem;
            
            SuperChatContext context = new SuperChatContext();

            Chat chat = _chats[ChatToUsersListView.SelectedIndex];
            
            ChatWindow window = new ChatWindow(_loggedInUser, selectedUser, _privateKey, chat);
            window.Show();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            LoadData();
        }
    }
}
