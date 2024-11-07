using System;
using System.ComponentModel;
using System.Text;
using Konscious.Security.Cryptography;
using StrategySync;

public class User : AES, INotifyPropertyChanged
{
    #region Private Fields
    private string _username;
    private byte[] _salt;
    private string _email;
    private string _usersFriends;
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

    public string Email
    {
        get { return _email; }
        set
        {
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
    }

    public string UsersFriends
    {
        get { return _usersFriends; }
        set
        {
            if (_usersFriends != value)
            {
                _usersFriends = value;
                OnPropertyChanged(nameof(UsersFriends));
            }
        }
    }
    #endregion

    #region Methods
    public string HashPasswordWithArgon2(string passwordPlainText)
    {
        var argon2 = new Argon2i(Encoding.UTF8.GetBytes(passwordPlainText))
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