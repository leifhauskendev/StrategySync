using StrategySync.Classes.DA;
using StrategySync.Classes.Email;
using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;

namespace StrategySync.Pages.Account.ForgotPassword
{
    public class ForgotPasswordVM
    {
        private readonly EmailService _emailService = new EmailService();
        private string _email;
        private string _verificationCode;
        private string _newPassword;
        private string _confirmPassword;
        private string _generatedVerificationCode;
        private string _username;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string VerificationCode
        {
            get => _verificationCode;
            set
            {
                _verificationCode = value;
                OnPropertyChanged(nameof(VerificationCode));
            }
        }

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public bool VerifyCode()
        {
            if (VerificationCode == _generatedVerificationCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> SendVerificationEmailAsync()
        {
            _generatedVerificationCode = GenerateVerificationCode();
            _username = UserDA.GetUsernameByEmail(Email);

            if (string.IsNullOrEmpty(_username))
            {
                return false;
            }

            bool emailSent = await _emailService.SendEmailAsync(Email, "Your Verification Code",
                $"Your verification code is: {_generatedVerificationCode}");

            if (!emailSent)
            {

                return false;
            }

            return true;
        }


        public void ChangePassword(string newPassword, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                return;
            }

            if (newPassword != confirmPassword)
            {
                return;
            }
              
            User user = UserDA.ReadRecord(_username);
            if (user == null)
            {
                return;
            }

            
            string hashedPassword = user.HashPasswordWithArgon2(newPassword);

            
            byte[] newKey = AES.GenerateKey(newPassword);
            byte[] newIV = AES.GenerateIV();

            
            byte[] encryptedPassword = User.EncryptStringToBytes(hashedPassword, newKey, newIV);


            user.EncryptedPassword = encryptedPassword;
            user.AESKey = newKey;
            user.AESIV = newIV;

            UserDA.UpdatePassword(user);
            
        }


        private string GenerateVerificationCode()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[4];
                rng.GetBytes(bytes);
                return (BitConverter.ToUInt32(bytes, 0) % 1000000).ToString("D6");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
