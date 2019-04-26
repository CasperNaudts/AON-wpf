﻿using System;
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

            try
            {
             User user = context.Users.First(u => u.Name == UsernameTextBox.Text && u.Password == PasswordPasswordBox.Password);

               
            
          

            CspParameters cp = new CspParameters();
            cp.KeyContainerName = "superChat" + user.Name;

            var rsa = new RSACryptoServiceProvider(cp);

            ChatListWindow window = new ChatListWindow(rsa, user);
            window.Show();
            Close();

            }


            catch (Exception)
            {
                MessageBox.Show("foutieve inloggegevens");
                UsernameTextBox.Text = "";
                PasswordPasswordBox.Password = "";
            }



        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow();
            window.Show();
            Close();
        }
    }
}
