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

            // Create a new User instance
            User newUser = new User
            {
                Username = username,
                Salt = GenerateSalt() // Generate a random salt for this user
            };

            // Hash the password using Argon2
            string hashedPassword = newUser.HashPasswordWithArgon2(password);

            // Insert the user into the database
            UserDA.CreateRecord(newUser, hashedPassword);

            MessageBox.Show("User created successfully.");
        }

        private byte[] GenerateSalt()
        {
            // Generates a secure random salt
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}