using StrategySync.Classes.DA;
using System.Windows;
using System.Windows.Controls;

namespace StrategySync.Pages.Account.CreateAccount
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password; 
            string email = EmailTextBox.Text;
            string usersFriends = string.Empty;

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.");
                return;
            }

            byte[] salt = User.GenerateSalt();

            byte[] aesKey = User.GenerateKey(password);
            byte[] aesIV = User.GenerateIV();

            User newUser = new User
            {
                Username = username,
                Email = email,
                Salt = salt,
                AESKey = aesKey,
                AESIV = aesIV,
                UsersFriends = usersFriends
            };

            newUser.PasswordString = newUser.HashPasswordWithArgon2(password);
            newUser.EncryptedPassword = User.EncryptStringToBytes(newUser.PasswordString, newUser.AESKey, newUser.AESIV);

            UserDA.CreateRecord(newUser);

            MessageBox.Show("User created successfully.");
            this.Close();
        }
    }
}