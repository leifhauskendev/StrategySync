using StrategySync.Classes.DA;
using System;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;

namespace StrategySync.Pages.Account
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Page
    {
        public LoginScreenVM ViewModel;
        public LoginScreen()
        {
            InitializeComponent();
            ViewModel = new LoginScreenVM();
            this.DataContext = ViewModel;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the username and password from the TextBoxes
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;
            string email = "test"; // Assume there's a TextBox for email
            string usersFriends = "test"; // Assume there's a TextBox for users' friends

            // Generate a random salt for this user
            byte[] salt = User.GenerateSalt();

            // Generate AES encryption key and IV (you can customize these methods)
            byte[] aesKey = User.GenerateKey(password);
            byte[] aesIV = User.GenerateIV();

            // Create a new User instance with all properties
            User newUser = new User
            {
                Username = username,
                Email = email,
                Salt = salt,
                AESKey = aesKey,
                AESIV = aesIV,
                UsersFriends = usersFriends
            };

            // Hash the password using Argon2
            newUser.PasswordString = newUser.HashPasswordWithArgon2(password);
            newUser.EncryptedPassword = User.EncryptStringToBytes(newUser.PasswordString, newUser.AESKey, newUser.AESIV);
            // Insert the user into the database
            UserDA.CreateRecord(newUser);

            MessageBox.Show("User created successfully.");
        }
    }
}