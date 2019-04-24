using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json;
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
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                sentMessage(InputTextbox.Text);
            }
        }

        private void sentMessage(string input)
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

        }
    }
}
