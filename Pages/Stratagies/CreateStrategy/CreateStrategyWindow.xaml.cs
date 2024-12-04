using StrategySync.Pages.Stratagies.CreateStrategy;
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
    /// Interaction logic for CreateStrategy.xaml
    /// </summary>
    public partial class CreateStrategyWindow : Window
    {
        public CreateStrategyWindow()
        {
            InitializeComponent();
            var ViewModel = new CreateStrategyVM();
            this.DataContext = ViewModel;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
