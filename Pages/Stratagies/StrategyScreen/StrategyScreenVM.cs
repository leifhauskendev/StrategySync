using StrategySync.Classes.Strategy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using StrategySync.BL;
using System.Windows.Ink;
using System.Collections.ObjectModel;
using StrategySync.Classes.DA;
using System.Windows;
using Microsoft.Win32;

namespace StrategySync
{
    public class StrategyScreenVM : INotifyPropertyChanged
    {
        public StrategyScreenVM() { 

        }

        private string _user;
        private Strategy _source;
        private StrategyItem _selectedItem;
        private ObservableCollection<StrategyItem> _deletedItems = new ObservableCollection<StrategyItem>();
        private byte[] imageBytes;
        private BitmapImage _selectedImageSource;


        public string User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    OnPropertyChanged(nameof(User));
                }
            }
        }

        public Strategy Source
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

        public StrategyItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        public ObservableCollection<StrategyItem> DeletedItems
        {
            get { return _deletedItems; }
            set
            {
                if (_deletedItems != value)
                {
                    _deletedItems = value;
                    OnPropertyChanged(nameof(DeletedItems));
                }
            }
        }
        public BitmapImage SelectedImageSource
        {
            get { return _selectedImageSource; }
            set
            {
                if (_selectedImageSource != value)
                {
                    _selectedImageSource = value;
                    OnPropertyChanged(nameof(SelectedImageSource));
                }
            }
        }

        public bool SaveStrategy(InkCanvas inkCanvas, bool isBeingSavedWithCheckIn) 
        {
            if (isBeingSavedWithCheckIn)
            {
                Source.CheckedOutTo = string.Empty;
            }

            Source.Drawing = ConvertInkCanvasToByteArray(inkCanvas);
            if (Source.IsNew)
            {
                var result = StrategyBL.SaveNewStrategy(Source);
                if (result != -1)
                {
                    Source.IsNew = false;
                    Source.StrategyID = result;
                    return true;
                }
            } 
            else
            {
                var result = StrategyBL.SaveExistingStrategy(Source, DeletedItems);
                if (result)
                {
                    return true;
                }
            }

            return false; 
        }

        public void UpdateCheckedOut()
        {
            if (Source.IsCheckedOut)
            {
                Source.CheckedOutTo = (Application.Current as App).User;
            }
            else
            {
                Source.CheckedOutTo = string.Empty;
            }

            StrategiesDA.UpdateCheckedOut(Source.IsCheckedOut, Source.CheckedOutTo, Source.StrategyID);
        }

        public byte[] ConvertInkCanvasToByteArray(InkCanvas inkCanvas)
        {
            StrokeCollection strokes = inkCanvas.Strokes;

            if (strokes == null || strokes.Count == 0)
                return null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                strokes.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public void UploadPicture()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                SelectedImageSource = bitmap; 

                byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);

                if (SelectedItem != null)
                {
                    SelectedItem.MediaImage = imageBytes;

                    var updatedItem = new StrategyItem
                    {
                        ItemID = SelectedItem.ItemID,
                        StrategyID = SelectedItem.StrategyID,
                        ItemType = SelectedItem.ItemType,
                        Description = SelectedItem.Description,
                        XCoordinate = SelectedItem.XCoordinate,
                        YCoordinate = SelectedItem.YCoordinate,
                        Link = SelectedItem.Link,
                        MediaImage = imageBytes
                    };

                    StrategiesItemDA.EditRecord(updatedItem);
                }
            }
        }

        public void UploadLink(string updatedLink)
        {
            var updatedItem = new StrategyItem
            {
                ItemID = SelectedItem.ItemID, 
                StrategyID = SelectedItem.StrategyID,
                ItemType = SelectedItem.ItemType,
                Description = SelectedItem.Description,
                XCoordinate = SelectedItem.XCoordinate,
                YCoordinate = SelectedItem.YCoordinate,
                Link = updatedLink, 
                MediaImage = SelectedItem.MediaImage
            };

            StrategiesItemDA.EditRecord(updatedItem);

            SelectedItem.Link = updatedLink;
        }

        public BitmapImage ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using (var stream = new MemoryStream(byteArray))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                bitmap.Freeze(); 
                return bitmap;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
