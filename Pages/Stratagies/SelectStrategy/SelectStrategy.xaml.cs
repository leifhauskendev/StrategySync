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
        public SelectStrategy()
        {
            InitializeComponent();
            var ViewModel = new SelectStrategyVM();
            this.DataContext = ViewModel;
        }

        private void CreateStrategy_Click(object sender, RoutedEventArgs e)
        {
            var createStrategyWindow = new CreateStrategyWindow();
            createStrategyWindow.ShowDialog();
        }
    }
}
