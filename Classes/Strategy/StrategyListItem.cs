﻿using StrategySync.Enumerations.StrategyEnums;
using System;
using System.ComponentModel;
using System.Windows;

namespace StrategySync.Classes.Strategy
{
    public class StrategyListItem : INotifyPropertyChanged
    {
        private int _strategyID;
        private int _mapID;
        private string _name;
        private string _description;
        private DateTime _lastOpened;
        private bool _isCheckedOut;
        private Visibility _isOwner;

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
                MapName = Enum.GetName(typeof(Map), MapID);
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

        public Visibility IsOwner
        {
            get { return _isOwner; }
            set
            {
                if (_isOwner != value)
                {
                    _isOwner = value;
                    OnPropertyChanged(nameof(IsOwner));
                }
            }
        }

        public string MapName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
