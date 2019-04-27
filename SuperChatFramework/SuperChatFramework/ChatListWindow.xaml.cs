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
    public partial class ChatListWindow
    {
        private RSACryptoServiceProvider _privateKey;
        private User _loggedInUser;
        private List<Chat> _chats;
        private List<Key> _otherUserInChat;
        private List<Window> windows;

        public ChatListWindow(RSACryptoServiceProvider privateKey, User loggedInUser)
        {
            windows = new List<Window>();
            _privateKey = privateKey;
            _loggedInUser = loggedInUser;
            InitializeComponent();

            ChatToUsersListView.MouseDoubleClick += listBoxItem_MouseDoubleClick;

            LoadData();
        }

        private void NieuweChatButton_Click(object sender, RoutedEventArgs e)
        {
            NewChatWindow window = new NewChatWindow(_loggedInUser, _otherUserInChat);
            windows.Add(window);
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

            _otherUserInChat = new List<Key>();

            foreach (var chat in _chats)
            {
                var sleutels = context.Keys.Where(key => key.ChatId == chat.Id);
                var var = sleutels.First(key => key.UserId != _loggedInUser.Id);
                _otherUserInChat.Add(var);
            }

            ChatToUsersListView.Items.Clear();
            foreach (var key in _otherUserInChat)
            {
                ChatToUsersListView.Items.Add(context.Users.Find(key.UserId));
            }

        }

        void listBoxItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var selectedUser = (User) ChatToUsersListView.SelectedItem;

            Chat chat = _chats[ChatToUsersListView.SelectedIndex];
            
            ChatWindow window = new ChatWindow(_loggedInUser, selectedUser, _privateKey, chat);
            windows.Add(window);
            window.Show();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            LoadData();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();

            foreach (var w in windows)
            {
                if (w.IsLoaded)
                {
                    w.Close();
                }
            }

            Close();
        }
    }
}
