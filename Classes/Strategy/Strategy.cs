using StrategySync.Enumerations.StrategyEnums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StrategySync.Classes.Strategy
{
    public class Strategy : INotifyPropertyChanged
    {
        public Strategy() { 
            IsNew = false;
        }   

        private int _strategyID;
        private int _mapID;
        private string _name;
        private string _description;
        private DateTime _lastOpened;
        private bool _isCheckedOut;
        private string _userIDs;
        private byte[] _drawing;
        private ObservableCollection<StrategyItem> _strategyItems;
        private string _checkedOutTo;

        public int StrategyID
        {
            get { return _strategyID; }
            set
            {
                if (_strategyID != value)
                {
                    _strategyID = value;
                    OnPropertyChanged(nameof(StrategyID));
                }
            }
        }

        public int MapID
        {
            get { return _mapID; }
            set
            {
                if (_mapID != value)
                {
                    _mapID = value;
                    OnPropertyChanged(nameof(MapID));
                }
                Map = (Map)value;
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public DateTime LastOpened
        {
            get { return _lastOpened; }
            set
            {
                if (_lastOpened != value)
                {
                    _lastOpened = value;
                    OnPropertyChanged(nameof(LastOpened));
                }
            }
        }

        public bool IsCheckedOut
        {
            get { return _isCheckedOut; }
            set
            {
                if (_isCheckedOut != value)
                {
                    _isCheckedOut = value;
                    OnPropertyChanged(nameof(IsCheckedOut));
                }
            }
        }

        public string UserIds
        {
            get { return _userIDs; }
            set
            {
                if (_userIDs != value)
                {
                    _userIDs = value;
                    OnPropertyChanged(nameof(UserIds));
                }
            }
        }

        public byte[] Drawing
        {
            get { return _drawing; }
            set
            {
                if (_drawing != value)
                {
                    _drawing = value;
                    OnPropertyChanged(nameof(Drawing));
                }
            }
        }

        public ObservableCollection<StrategyItem> StrategyItems
        {
            get { return _strategyItems; }
            set
            {
                if (_strategyItems != value)
                {
                    _strategyItems = value;
                    OnPropertyChanged(nameof(StrategyItems));
                }
            }
        }

        public string CheckedOutTo
        {
            get { return _checkedOutTo; }
            set
            {
                if (_checkedOutTo != value)
                {
                    _checkedOutTo = value;
                    OnPropertyChanged(nameof(CheckedOutTo));
                }
            }
        }

        public Map Map { get; set; }

        public bool IsNew { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
