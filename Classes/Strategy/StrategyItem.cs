using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace StrategySync.Classes.Strategy
{
    public class StrategyItem : INotifyPropertyChanged
    {
        private int _itemID;
        private int _strategyID;
        private int _itemType;
        private string _description;
        private float _xCoordinate;
        private float _yCoordinate;
        private Image _image;



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

        public int ItemType
        {
            get { return _itemType; }
            set
            {
                if (_itemType != value)
                {
                    _itemType = value;
                    OnPropertyChanged(nameof(ItemType));
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

        public Image Image
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(nameof(Image));
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
