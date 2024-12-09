using StrategySync.Classes.DA;
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
using System.Windows.Shapes;

namespace StrategySync.Pages.Stratagies.AddUser
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private int _strategyID;
        public AddUser(int strategyID)
        {
            InitializeComponent();
            _strategyID = strategyID;
        }

        public void ShareStrategyButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;

            if(string.IsNullOrWhiteSpace(username))
            {
                return;
            }

            bool share = StrategiesDA.ShareStrategy(_strategyID, username);
            if (share)
            {
                this.Close();
            } 
            else
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}