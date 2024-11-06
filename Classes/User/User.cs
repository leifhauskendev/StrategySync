using System;
using System.ComponentModel;
using System.Text;
using Konscious.Security.Cryptography;
using StrategySync;

public class User : AES, INotifyPropertyChanged
{
    #region Private Fields
    private string _username;
    private string _password;
    private byte[] _salt;
    #endregion

    #region Public Properties
    public string Username
    {
        get { return _username; }
        set
        {
            if (_username != value)
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
    }

    public string Password
    {
        get { return _password; }
        set
        {
            if (_password != value)
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }

    public byte[] Salt
    {
        get { return _salt; }
        set
        {
            if (_salt != value)
            {
                _salt = value;
                OnPropertyChanged(nameof(Salt));
            }
        }
    }
    #endregion

    #region Methods
    public string HashPasswordWithArgon2()
    {
        var argon2 = new Argon2i(Encoding.UTF8.GetBytes(Password))
        {
            Salt = Salt,
            DegreeOfParallelism = 8,
            Iterations = 4,
            MemorySize = 1024 * 1024 // 1 GB
        };

        var hash = argon2.GetBytes(32);
        return Convert.ToBase64String(hash);
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
