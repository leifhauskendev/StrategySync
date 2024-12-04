using StrategySync.Classes.DA;
using StrategySync.Classes.Strategy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StrategySync.Pages.Stratagies.StrategyScreen
{
    public class SelectStrategyVM : INotifyPropertyChanged
    {
        public SelectStrategyVM()
        {
            GetStrategies();
        }

        private ObservableCollection<StrategyListItem> _source;

        public ObservableCollection<StrategyListItem> Source
        {
            get { return _source; }
            set
            {
                if (_source != value)
                {
                    _source = value;
                    OnPropertyChanged(nameof(Source));
                }
            }
        }

        private Visibility _strategyListVisiblity = Visibility.Hidden;

        public Visibility StrategyListVisiblity
        {
            get { return _strategyListVisiblity; }
            set
            {
                if (_strategyListVisiblity != value)
                {
                    _strategyListVisiblity = value;
                    OnPropertyChanged(nameof(StrategyListVisiblity));
                }
            }
        }

        private Visibility _loadingTextVisiblity = Visibility.Visible;

        public Visibility LoadingTextVisiblity
        {
            get { return _loadingTextVisiblity; }
            set
            {
                if (_loadingTextVisiblity != value)
                {
                    _loadingTextVisiblity = value;
                    OnPropertyChanged(nameof(LoadingTextVisiblity));
                }
            }
        }

        private Visibility _noDataTextVisiblity = Visibility.Hidden;

        public Visibility NoDataTextVisiblity
        {
            get { return _noDataTextVisiblity; }
            set
            {
                if (_noDataTextVisiblity != value)
                {
                    _noDataTextVisiblity = value;
                    OnPropertyChanged(nameof(NoDataTextVisiblity));
                }
            }
        }

        public void GetStrategies ()
        {
            var app = (App)Application.Current;
            var strategies = StrategiesDA.GetStrategyListItemsByUser(app.User);
            LoadingTextVisiblity = Visibility.Hidden;
            if (strategies != null)
            {
                Source = strategies;
                NoDataTextVisiblity = Visibility.Visible;
            }

            if (strategies.Count > 0)
            {
                NoDataTextVisiblity = Visibility.Hidden;
                StrategyListVisiblity = Visibility.Visible;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
