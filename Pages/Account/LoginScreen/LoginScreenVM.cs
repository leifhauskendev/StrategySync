using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrategySync.Classes;
using StrategySync.Classes.DA;

namespace StrategySync
{
    public class LoginScreenVM : INotifyPropertyChanged
    {
        public LoginScreenVM() {
            this.Source = new User();
        }

        private User _source;

        public User Source {
            get { return _source; }
            set
            {
                if (_source != value)
                {
                    _source = value;
                    OnPropertyChanged(nameof(Source));
                }
            }
        }

        public bool ValidateLoginInfo()
        {
            var retrievedUser = UserDA.ReadRecord(Source.Username);
            var decryptedPassword = User.DecryptStringFromBytes(retrievedUser.EncryptedPassword, retrievedUser.AESKey, retrievedUser.AESIV);
            Source.Salt = retrievedUser.Salt;

            if (decryptedPassword == Source.HashPasswordWithArgon2(Source.PasswordString)) {
                return true;
            } else
            {
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
