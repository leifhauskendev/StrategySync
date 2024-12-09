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
        private CreateAccountVM createAccountVm;

        public CreateAccount()
        {
            InitializeComponent();
            createAccountVm = new CreateAccountVM();
            DataContext = createAccountVm;
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            createAccountVm.Password = PasswordBox.Password;
            createAccountVm.ConfirmPassword = ConfirmPasswordBox.Password;

            bool userCreated = createAccountVm.CreateUser();
            if (userCreated)
            {
                MessageBox.Show("User created successfully.");
                this.Close();
            }
        }
    }
}