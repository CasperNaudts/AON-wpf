using System.Linq;
using System.Windows;
using SuperChat.Data;
using SuperChat.Domain;

namespace SuperChatFramework
{
    /// <summary>
    /// Interaction logic for NewChatWindow.xaml
    /// </summary>
    public partial class NewChatWindow : Window
    {
        private User _logedInUser;
        private SuperChatContext _context;

        public NewChatWindow(User logedInUser)
        {
            _logedInUser = logedInUser;
            InitializeComponent();

            _context = new SuperChatContext();
            var users = context.Users.Where(user => user != _logedInUser).ToList();
            DataContext = users;
        }

        private void CreateChatButton_Click(object sender, RoutedEventArgs e)
        {
            Chat chat = new Chat();
            _context.Chats.Add(chat);
            _context.SaveChanges();
        }
    }
}
