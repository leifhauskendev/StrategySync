using StrategySync.Classes.Strategy;
using StrategySync.Pages.Stratagies.StrategyScreen;
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

namespace StrategySync.Pages.Stratagies
{
    /// <summary>
    /// Interaction logic for SelectStrategy.xaml
    /// </summary>
    public partial class SelectStrategy : Window
    {
        SelectStrategyVM ViewModel;
        public SelectStrategy()
        {
            InitializeComponent();
            ViewModel = new SelectStrategyVM();
            this.DataContext = ViewModel;
        }

        private void CreateStrategy_Click(object sender, RoutedEventArgs e)
        {
            var createStrategyWindow = new CreateStrategyWindow();
            createStrategyWindow.ShowDialog();
            var app = (App)Application.Current;
            if (app.CurrentStrategy != null)
            {
                this.Close();
            }
        }

        private void Strategy_Click(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            var strategy = grid.DataContext as StrategyListItem;
            if (!strategy.IsCheckedOut)
            {
                if (ViewModel.GetSelectedStrategyById(strategy.StrategyID))
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Unable to retrieve strategy.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else 
            {
                MessageBox.Show("Cannot open a strategy that is checked out", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
