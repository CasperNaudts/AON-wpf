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

namespace SuperChatFramework
{
    /// <summary>
    /// Interaction logic for ChatListWindows.xaml
    /// </summary>
    public partial class ChatListWindows : Window
    {
        private RSACryptoServiceProvider _privateKey;
        public ChatListWindows(RSACryptoServiceProvider privateKey)
        {
            _privateKey = privateKey;
            InitializeComponent();
        }
    }
}
