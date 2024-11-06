using System;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace StrategySync
{
    public class AES : INotifyPropertyChanged
    {
        #region Private Fields
        private string _passwordString;
        private byte[] _encryptedPassword;
        private byte[] _aesKey;
        private byte[] _aesIV;
        private static readonly byte[] _salt = GenerateSalt();
        #endregion

        #region Public Properties
        public string PasswordString
        {
            get { return _passwordString; }
            set
            {
                if (_passwordString != value)
                {
                    _passwordString = value;
                    OnPropertyChanged(nameof(PasswordString));
                }
            }
        }

        public byte[] EncryptedPassword
        {
            get { return _encryptedPassword; }
            set
            {
                if (_encryptedPassword != value)
                {
                    _encryptedPassword = value;
                    OnPropertyChanged(nameof(EncryptedPassword));
                }
            }
        }

        public byte[] AESKey
        {
            get { return _aesKey; }
            set
            {
                if (_aesKey != value)
                {
                    _aesKey = value;
                    OnPropertyChanged(nameof(AESKey));
                }
            }
        }

        public byte[] AESIV
        {
            get { return _aesIV; }
            set
            {
                if (_aesIV != value)
                {
                    _aesIV = value;
                    OnPropertyChanged(nameof(AESIV));
                }
            }
        }
        #endregion

        #region Methods
        public static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            if (key == null || key.Length == 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length == 0)
                throw new ArgumentNullException(nameof(iv));

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                    return ms.ToArray();
                }
            }
        }

        public static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length == 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (key == null || key.Length == 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length == 0)
                throw new ArgumentNullException(nameof(iv));

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                using (MemoryStream ms = new MemoryStream(cipherText))
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static byte[] GenerateKey(string password)
        {
            using (var rfc = new Rfc2898DeriveBytes(password, _salt))
            {
                return rfc.GetBytes(32); // 32 bytes = 256 bits
            }
        }

        public static byte[] GenerateIV()
        {
            using (Aes aes = Aes.Create())
            {
                aes.GenerateIV();
                return aes.IV;
            }
        }

        public static byte[] GenerateSalt()
        {
            int saltLength = 16;
            byte[] salt = new byte[saltLength];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
