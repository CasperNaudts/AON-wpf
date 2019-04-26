using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Timers;
using Newtonsoft.Json;
using SuperChat.Business;
using SuperChat.Data;
using SuperChat.Domain;
using Key = System.Windows.Input.Key;

namespace SuperChatFramework
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow
    {
        private User _loggedInUser;
        private User _targetUser;
        private Aes _symKeyAes;
        private RSACryptoServiceProvider _loggedinRsa;
        private Chat _currentChat;

        public ChatWindow(User loggedInUser, User targetUser, RSACryptoServiceProvider loggedinRsa, Chat chat)
        {
            _loggedInUser = loggedInUser;
            _targetUser = targetUser;
            _loggedinRsa = loggedinRsa;
            _currentChat = chat;
            _symKeyAes = Aes.Create();

            InitializeComponent();
            TargetUserLabel.Content = _targetUser.Name;

            _symKeyAes.Key = _loggedinRsa.Decrypt((chat.Keys.First(key => key.UserId == loggedInUser.Id)).KeyBytes, false);
            _symKeyAes.GenerateIV();

            ListMessages(_loggedInUser, _targetUser);
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            ListMessages(_loggedInUser, _targetUser);
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SentMessage(InputTextbox.Text);
            }
        }

        private void SentMessage(string input)
        {
            byte[] encrypted;

            ICryptoTransform encryptor = _symKeyAes.CreateEncryptor(_symKeyAes.Key, _symKeyAes.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(input);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            Message message = new Message();
            message.Content = JsonConvert.SerializeObject(encrypted);
            message.Iv = _symKeyAes.IV;
            message.RecieverId = _targetUser.Id;
            message.SenderId = _loggedInUser.Id;
            message.TimeSend = DateTime.Now;

            SuperChatContext context = new SuperChatContext();

            context.Messages.Add(message);
            context.SaveChanges();

            InputTextbox.Text = "";
            ListMessages(_loggedInUser, _targetUser);
        }

        private void ListMessages(User senderUser, User ReceiverUser)
        {
            SuperChatContext context = new SuperChatContext();

            MessagesListView.Items.Clear();
            var messageList = context.Messages
                .Where(message => message.RecieverId == ReceiverUser.Id || message.RecieverId == senderUser.Id)
                .Where(message => message.SenderId == ReceiverUser.Id || message.SenderId == senderUser.Id)
                .OrderBy(message => message.TimeSend).ToList();

            foreach (var message in messageList)
            {
                _symKeyAes.IV = message.Iv;
                message.Content = SymmetricEncryption.DecryptStringFromBytes_Aes(JsonConvert.DeserializeObject<byte[]>(message.Content), _symKeyAes);
                message.Content = context.Users.Find(message.SenderId).Name + ":\t" + message.Content;
                MessagesListView.Items.Add(message);
            }
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ListMessages(_loggedInUser, _targetUser);
        }
    }
}
