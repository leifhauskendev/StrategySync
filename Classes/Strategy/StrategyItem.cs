using System;
using System.ComponentModel;

namespace StrategySync.Classes.Strategy
{
    public class StrategyItem : INotifyPropertyChanged
    {
        private int _itemID;
        private int _strategyID;
        private int _roleID;
        private int _grenadeID;
        private string _description;
        private float _xCoordinate;
        private float _yCoordinate;



        public int ItemID
        {
            get { return _itemID; }
            set
            {
                if (_itemID != value)
                {
                    _itemID = value;
                    OnPropertyChanged(nameof(ItemID));
                }
            }
        }

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

        public int RoleID
        {
            get { return _roleID; }
            set
            {
                if (_roleID != value)
                {
                    _roleID = value;
                    OnPropertyChanged(nameof(RoleID));
                }
            }
        }

        public int GrenadeID
        {
            get { return _grenadeID; }
            set
            {
                if (_grenadeID != value)
                {
                    _grenadeID = value;
                    OnPropertyChanged(nameof(GrenadeID));
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

        public float XCoordinate
        {
            get { return _xCoordinate; }
            set
            {
                if (_xCoordinate != value)
                {
                    _xCoordinate = value;
                    OnPropertyChanged(nameof(XCoordinate));
                }
            }
        }

        public float YCoordinate
        {
            get { return _yCoordinate; }
            set
            {
                if (_yCoordinate != value)
                {
                    _yCoordinate = value;
                    OnPropertyChanged(nameof(YCoordinate));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
