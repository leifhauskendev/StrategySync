using StrategySync.Classes.Email;
using System.Timers;
using System.Windows;

namespace StrategySync.Pages.Account.ForgotPassword
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        private readonly Timer _resendTimer;
        private int _resendCooldown;
        public ForgotPasswordVM ViewModel { get; }

        public ForgotPassword()
        {
            InitializeComponent();
            ViewModel = new ForgotPasswordVM();
            DataContext = ViewModel;

            _resendTimer = new Timer(1000);
            _resendTimer.Elapsed += ResendTimer_Elapsed;
            _resendCooldown = 30; 
        }

        private void ResendTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _resendCooldown--;
                if (_resendCooldown <= 0)
                {
                    _resendTimer.Stop();
                    ResendEmailButton.IsEnabled = true;
                    ResendCountdownTextBlock.Text = string.Empty;
                }
                else
                {
                    ResendCountdownTextBlock.Text = $"Resend available in {_resendCooldown}s";
                }
            });
        }

        private void ShowEmailSection()
        {
            EmailSection.Visibility = Visibility.Visible;
            VerificationSection.Visibility = Visibility.Collapsed;
            PasswordSection.Visibility = Visibility.Collapsed;
        }

        private void ShowVerificationSection()
        {
            EmailSection.Visibility = Visibility.Collapsed;
            VerificationSection.Visibility = Visibility.Visible;
            PasswordSection.Visibility = Visibility.Collapsed;

            ResendEmailButton.IsEnabled = false;
            _resendCooldown = 30;
            _resendTimer.Start();
        }

        private void ShowPasswordSection()
        {
            EmailSection.Visibility = Visibility.Collapsed;
            VerificationSection.Visibility = Visibility.Collapsed;
            PasswordSection.Visibility = Visibility.Visible;
        }

        private async void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            SendEmail.IsEnabled = false;
            bool emailSent = await ViewModel.SendVerificationEmailAsync();
            if (emailSent)
            {
                ShowVerificationSection();
            }
        }

        private void SubmitCode_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.VerifyCode())
            {
                ShowPasswordSection();
            }
        }

        private async void ResendEmail_Click(object sender, RoutedEventArgs e)
        {
            SendEmail.IsEnabled = false;
            bool emailSent = await ViewModel.SendVerificationEmailAsync();
            if (emailSent)
            {
                ShowVerificationSection();
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ChangePassword(NewPasswordBox.Password, ConfirmPasswordBox.Password); 
            Close();
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.NewPassword = NewPasswordBox.Password;
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.ConfirmPassword = ConfirmPasswordBox.Password;
        }
    }
}
