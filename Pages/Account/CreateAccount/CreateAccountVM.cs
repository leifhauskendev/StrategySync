using StrategySync.Classes.DA;
using System;
using System.ComponentModel;
using System.Windows;

namespace StrategySync.Pages.Account.CreateAccount
{
    internal class CreateAccountVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _username;
        private string _email;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public bool CreateUser()
        {
            // Validate passwords
            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.");
                return false;
            }

            byte[] salt = User.GenerateSalt();
            byte[] aesKey = User.GenerateKey(Password);
            byte[] aesIV = User.GenerateIV();

            User newUser = new User
            {
                Username = Username,
                Email = Email,
                Salt = salt,
                AESKey = aesKey,
                AESIV = aesIV,
                UsersFriends = string.Empty
            };

            newUser.PasswordString = newUser.HashPasswordWithArgon2(Password);
            newUser.EncryptedPassword = User.EncryptStringToBytes(newUser.PasswordString, newUser.AESKey, newUser.AESIV);

            UserDA.CreateRecord(newUser);
            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
