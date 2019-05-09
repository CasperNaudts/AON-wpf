using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using SuperChat.Data;
using SuperChat.Domain;

namespace SuperChatFramework
{
    public partial class NewChatWindow
    {
        private User _logedInUser;
        private readonly SuperChatContext _context;

        public NewChatWindow(User logedInUser, List<Key> otherUserInChat)
        {
            _logedInUser = logedInUser;
            InitializeComponent();

            _context = new SuperChatContext();
            var users = _context.Users.Where(user => user != _logedInUser).ToList();

            foreach (var key in otherUserInChat)
            {
                var currentUser = _context.Users.Find(key.UserId);
                users.Remove(currentUser);
            }

            SelectedUserComboBox.ItemsSource = users;
        }

        private void CreateChatButton_Click(object sender, RoutedEventArgs e)
        {
            Chat chat = new Chat();
            User selectedUser = _context.Users.Find(((User)SelectedUserComboBox.SelectionBoxItem).Id);

            Aes aesAlg = Aes.Create();

            if (aesAlg != null)
            {
                aesAlg.GenerateKey();
                var key = aesAlg.Key;

                Key loggedInKey = new Key();
                RSACryptoServiceProvider loggedInRsa = new RSACryptoServiceProvider();
                loggedInRsa.FromXmlString(_logedInUser.PublicKey);
                loggedInKey.KeyBytes = loggedInRsa.Encrypt(key, false);
                loggedInKey.Chat = chat;
                loggedInKey.UserId = _logedInUser.Id;

                chat.Keys.Add(loggedInKey);

                Key selectedUserKey = new Key();
                RSACryptoServiceProvider selectedUserRsa = new RSACryptoServiceProvider();
                selectedUserRsa.FromXmlString(selectedUser.PublicKey);
                selectedUserKey.KeyBytes = selectedUserRsa.Encrypt(key, false);
                selectedUserKey.Chat = chat;
                selectedUserKey.UserId = selectedUser.Id;

                chat.Keys.Add(selectedUserKey);

                _context.Keys.Add(loggedInKey);
                _context.Keys.Add(selectedUserKey);
            }

            _context.Chats.Add(chat);
            _context.SaveChanges();
            Close();
        }
    }
}
