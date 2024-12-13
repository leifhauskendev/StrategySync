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
        CreateStrategyVM ViewModel = new CreateStrategyVM();
        public CreateStrategyWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            var app = (App)Application.Current;
            ViewModel.Source.IsNew = true;
            ViewModel.Source.IsCheckedOut = true;
            ViewModel.Source.CheckedOutTo = app.User;
            app.CurrentStrategy = ViewModel.Source;
            this.Close();
        }

        private void Map_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Source.Map = (Enumerations.StrategyEnums.Map)MapComboBox.SelectedIndex;
            ViewModel.Source.MapID = MapComboBox.SelectedIndex;
        }
    }
}
