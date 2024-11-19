using StrategySync.Classes.DA;
using StrategySync.Pages.Stratagies.StrategyScreen;
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
            if (ViewModel.ValidateLoginInfo())
            {
                NavigationService.Navigate(new StrategyScreen());
            } else {
                MessageBox.Show("Username or password is incorrect.");
            }
        }
        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var createAccountWindow = new CreateAccount.CreateAccount();
            createAccountWindow.ShowDialog();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.Source.PasswordString = PasswordBox.Password;
        }
    }
}