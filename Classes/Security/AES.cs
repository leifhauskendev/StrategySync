using System;
using System.IO;
using System.Security.Cryptography;

namespace StrategySync
{
    public class AES
    {
        #region Private Fields
        private string _passwordString;
        private byte[] _encyrptedPassword;
        private byte[] _aesKey;
        private byte[] _aesIV;
        private static readonly byte[] salt = GenerateSalt();
        #endregion

        #region Public Properties
        public string PasswordString
        {
            get { return _passwordString; }
            set { _passwordString = value; }
        }

        public byte[] EncryptedPassword
        {
            get { return _encyrptedPassword; }
            set { _encyrptedPassword = value; }
        }

        public byte[] AESKey
        {
            get { return _aesKey; }
            set { _aesKey = value; }
        }

        public byte[] AESIV
        {
            get { return _aesIV; }
            set { _aesIV = value; }
        }
        #endregion

        #region Methods
        public static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("iv");

            byte[] encrypted;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }

        public static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("iv");

            string decrypted;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            decrypted = sr.ReadToEnd();
                        }
                    }
                }
            }
            return decrypted;
        }

        public static byte[] GenerateKey(string password)
        {
            using (var rfc = new Rfc2898DeriveBytes(password, salt))
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
    }
}