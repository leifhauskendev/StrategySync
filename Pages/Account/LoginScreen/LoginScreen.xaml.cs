using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            ViewModel.ValidateLoginInfo();
        }
    }
}
