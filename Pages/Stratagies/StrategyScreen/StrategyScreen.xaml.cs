using Google.Protobuf.WellKnownTypes;
using StrategySync.Classes.Strategy;
using StrategySync.Enumerations.StrategyEnums;
using StrategySync.Pages.Account.CreateAccount;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;

namespace StrategySync.Pages.Stratagies.StrategyScreen
{
    /// <summary>
    /// Interaction logic for StrategyScreen.xaml
    /// </summary>
    public partial class StrategyScreen : Page
    {
        public StrategyScreenVM ViewModel;
        public bool penEnabled = false;
        public bool eraserEnabled = false;

        private readonly DrawingAttributes PenAttributes = new DrawingAttributes()
        {
            Color = Colors.Red,
            Height = 2,
            Width = 2
        };

        public StrategyScreen()
        {
            InitializeComponent();
            ViewModel = new StrategyScreenVM();
            this.DataContext = ViewModel;
            DrawingCanvas.EditingMode = InkCanvasEditingMode.None;

            var selectStrategyWindow = new SelectStrategy();
            selectStrategyWindow.ShowDialog();
            SetStrategy();

            var user = (Application.Current as App).User;
            if (user == "leif" || user == "Andy" || user == "Trevor")
            {
                TestingButton.Visibility = Visibility.Visible;
            }
        }

        private void EraserBtn_Click(object sender, RoutedEventArgs e)
        {
            SetEditingMode(EditingMode.Eraser);
        }

        private void PenBtn_Click(object sender, RoutedEventArgs e)
        {
            SetEditingMode(EditingMode.Pen);
        }

        private void ShareStrategyIcon_Click(object sender, RoutedEventArgs e)
        {
            var addUserWindow = new AddUser.AddUser(ViewModel.Source.StrategyID);
            addUserWindow.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ItemCanvas.Children.Clear();
            var selectStrategyWindow = new SelectStrategy();
            selectStrategyWindow.ShowDialog();
            SetStrategy();
        }

        private void ExitButton_Click(Object sender, RoutedEventArgs e)
        {
            var logOut = new LogOut.LogOut();
            logOut.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemLinkEditor.Visibility == Visibility.Collapsed)
            {
                ItemLinkEditor.Visibility = Visibility.Visible;
                ItemLinkLabel.Visibility = Visibility.Visible;
                HyperLinkText.Visibility = Visibility.Hidden;
                EditButton.Content = "Save Link";
            }
            else
            {
                string updatedLink = ItemLinkEditor.Text;

                ViewModel.UploadLink(updatedLink);

                ItemLinkEditor.Visibility = Visibility.Hidden;
                ItemLinkLabel.Visibility = Visibility.Hidden;
                HyperLinkText.Visibility = Visibility.Visible;
                EditButton.Content = "Edit Link";
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });
            e.Handled = true;
        }
        private void SelectedImage_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Image image && ImagePopup != null)
            {
                PopupImage.Source = image.Source;
                ImagePopup.IsOpen = true;         
            }
        }

        private void SelectedImage_MouseLeave(object sender, MouseEventArgs e)
        {
            if (ImagePopup != null)
            {
                ImagePopup.IsOpen = false;
            }
        }

        private void SelectedImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ImagePopup != null)
            {
                ImagePopup.IsOpen = !ImagePopup.IsOpen;
            }
        }

        private void UploadPicture_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.UploadPicture();
            SelectedImage.Source = null;
            SelectedImage.Source = ViewModel.ByteArrayToImage(ViewModel.SelectedItem.MediaImage);
        }

        private void SetEditingMode(EditingMode mode)
        {
            PenBtn.IsChecked = false;
            EraserBtn.IsChecked = false;

            ItemCanvas.IsHitTestVisible = false;
            DrawingCanvas.IsHitTestVisible = true;

            switch (mode)
            {
                case EditingMode.Pen:
                    if (penEnabled)
                    {
                        ItemCanvas.IsHitTestVisible = true;
                        DrawingCanvas.IsHitTestVisible = false;
                        penEnabled = false;
                    }
                    else
                    {
                        PenBtn.IsChecked = true;
                        DrawingCanvas.EditingMode = InkCanvasEditingMode.Ink;
                        DrawingCanvas.DefaultDrawingAttributes = PenAttributes;
                        penEnabled = true;
                    }
                    break;
                case EditingMode.Eraser:
                    if (eraserEnabled)
                    {
                        ItemCanvas.IsHitTestVisible = true;
                        DrawingCanvas.IsHitTestVisible = false;
                        eraserEnabled = false;
                    }
                    else
                    {
                        EraserBtn.IsChecked = true;
                        DrawingCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                        eraserEnabled = true;
                    }
                    break;

                default:
                    break;
            }
        }

        public enum EditingMode
        {
            Pen, Eraser
        }

        private void ItemCanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (this.dragObject == null)
                return;

            var position = e.GetPosition(sender as IInputElement);
            Canvas.SetTop(this.dragObject, position.Y - this.offset.Y);
            Canvas.SetLeft(this.dragObject, position.X - this.offset.X);
        }

        private void ItemCanvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)this.dragObject;
            if (image != null)
            {
                if (image.Tag != null)
                {
                    var position = e.GetPosition(sender as IInputElement);
                    StrategyItem strategyItem = image.Tag as StrategyItem;
                    strategyItem.XCoordinate = (float)position.X - (float)this.offset.X;
                    strategyItem.YCoordinate = (float)position.Y - (float)this.offset.Y;
                }
            }

            this.dragObject = null;
            this.ItemCanvas.ReleaseMouseCapture();
        }

        private void NewItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image clickedItem = sender as Image;
            StrategyItem newItem = new StrategyItem();
            ViewModel.Source.StrategyItems.Add(newItem);
            SetItemVisuals(newItem, clickedItem.Name, 20, 20);
            ItemCanvas.IsHitTestVisible = true;
            DrawingCanvas.IsHitTestVisible = false;
            PenBtn.IsChecked = false;
            EraserBtn.IsChecked = false;
            penEnabled = false;
            eraserEnabled = false;
        }

        UIElement dragObject = null;
        Point offset;

        private void item_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.dragObject = sender as UIElement;
            this.offset = e.GetPosition(this.ItemCanvas);
            this.offset.Y -= Canvas.GetTop(this.dragObject);
            this.offset.X -= Canvas.GetLeft(this.dragObject);
            this.ItemCanvas.CaptureMouse();

            var image = sender as Image;
            if (image != null)
            {
                SetSelectedItem((StrategyItem)image.Tag);
                ItemDescription.Visibility = Visibility.Visible;
                ItemDescriptionLabel.Visibility = Visibility.Visible;
                NoneSelected.Visibility = Visibility.Hidden;
                ItemDescription.CaretIndex = ItemDescription.Text.Length;
                DeleteButton.Visibility = Visibility.Visible;
                EditButton.Visibility = Visibility.Visible;
                UploadPicture.Visibility = Visibility.Visible;
                SelectedImage.Visibility = Visibility.Visible;

                if (!string.IsNullOrWhiteSpace(ViewModel?.SelectedItem?.Link) && Uri.TryCreate(ViewModel.SelectedItem.Link, UriKind.Absolute, out _))
                {
                    ItemLinkLabel.Visibility = Visibility.Hidden;
                    HyperLinkText.Visibility = Visibility.Visible;
                    ItemLinkEditor.Visibility = Visibility.Hidden;
                    EditButton.Content = "Edit Link";
                }
                else
                {
                    ItemLinkLabel.Visibility = Visibility.Visible;
                    HyperLinkText.Visibility = Visibility.Hidden;
                    ItemLinkEditor.Visibility = Visibility.Visible;
                    EditButton.Content = "Save Link";
                }

                if (ViewModel.SelectedItem.MediaImage != null)
                {
                    SelectedImage.Source = null;
                    SelectedImage.Source = ViewModel.ByteArrayToImage(ViewModel.SelectedItem.MediaImage);
                }
                else
                {
                    SelectedImage.Source = null;
                }
            }
        }

        private void SetOnscreenItems ()
        {
            ItemCanvas.IsHitTestVisible = true;
            DrawingCanvas.IsHitTestVisible = false;
            PenBtn.IsChecked = false;
            EraserBtn.IsChecked = false;
            penEnabled = false;
            eraserEnabled = false;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();

            switch (ViewModel.Source.Map)
            {
                case Map.Mirage:
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/9/95/Cs2_mirage_radar.png/revision/latest/scale-to-width-down/1000?cb=20231020111431");
                    break;
                case Map.Inferno:
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/1/11/CS2_inferno_radar.png/revision/latest/scale-to-width-down/1000?cb=20230901123108");
                    break;
                case Map.Dust2:
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/0/0f/Cs2_dust2_overview.png/revision/latest/scale-to-width-down/1000?cb=20230323162128");
                    break;
                case Map.Ancient:
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/9/9a/Ancient_Radar.png/revision/latest/scale-to-width-down/1000?cb=20221216234111");
                    break;
                case Map.Nuke:
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/8/8f/Cs2_nuke_radar.png/revision/latest/scale-to-width-down/1000?cb=20231020111944");
                    break;
                case Map.Vertigo:
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/3/35/De_vertigo_radar.png/revision/latest/scale-to-width-down/1000?cb=20240524233147");
                    break;
                case Map.Anubis:
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/a/ae/De_anubis_radar.png/revision/latest/scale-to-width-down/1000?cb=20220823205108");
                    break;

                default:
                    break;
            }

            bitmap.EndInit();
            MapImage.Source = bitmap;

            if (!ViewModel.Source.IsNew)
            {
                foreach (StrategyItem item in ViewModel.Source.StrategyItems)
                {
                    SetItemVisuals(item, System.Enum.GetName(typeof(ItemType), item.ItemType), item.XCoordinate, item.YCoordinate);
                }

                SetInkCanvasFromByteArray(ViewModel.Source.Drawing);
            }
        }

        private void SetItemVisuals(StrategyItem item, string itemTypeName, double x, double y)
        {
            item.Image = new Image();
            item.Image.Width = 50;
            item.Image.Height = 50;
            item.Image.Tag = item;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            switch (itemTypeName)
            {
                case "Flashbang":
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/f/f3/Flashbanghud_csgo.png/revision/latest?cb=20211113165814", UriKind.Absolute);
                    item.ItemType = (int)ItemType.Flashbang;
                    break;
                case "SmokeGrenade":
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/7/76/Smokegrenadehud_csgo.png/revision/latest?cb=20211113165620", UriKind.Absolute);
                    item.ItemType = (int)ItemType.SmokeGrenade;
                    break;
                case "Molotov":
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/5/56/Molotovhud.png/revision/latest?cb=20211113171930", UriKind.Absolute);
                    item.ItemType = (int)ItemType.Molotov;
                    break;
                case "HeGrenade":
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/c/ce/Hegrenadehud_csgo.png/revision/latest/thumbnail/width/360/height/360?cb=20211113165930", UriKind.Absolute);
                    item.ItemType = (int)ItemType.HeGrenade;
                    break;
                case "Friendly":
                    bitmap.UriSource = new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/8/8e/Pan_Blue_Circle.png/640px-Pan_Blue_Circle.png", UriKind.Absolute);
                    item.Image.Width = 20;
                    item.Image.Height = 20;
                    item.ItemType = (int)ItemType.Friendly;
                    break;
                case "Enemy":
                    bitmap.UriSource = new Uri("https://upload.wikimedia.org/wikipedia/commons/b/b1/Red-Circle-Transparent.png", UriKind.Absolute);
                    item.Image.Width = 20;
                    item.Image.Height = 20;
                    item.ItemType = (int)ItemType.Enemy;
                    break;

            }
            bitmap.EndInit();
            item.Image.Source = bitmap;
            Canvas.SetTop(item.Image, y);
            Canvas.SetLeft(item.Image, x);
            item.Image.PreviewMouseDown += item_PreviewMouseDown;
            ItemCanvas.Children.Add(item.Image);
        }

        private void SetCheckInOutVisibilities(string user)
        {
            // Ensuring the check in/out buttons work as intented every time a new strat is opened
            Save.Visibility = Visibility.Hidden;
            CheckInOutButton.Content = "Check Out";

            if (ViewModel.Source.IsCheckedOut)
            {
                if (ViewModel.Source.CheckedOutTo == user)
                {
                    CheckInOutButton.Content = "Check In";
                    Save.Visibility = Visibility.Visible;
                }
                else
                {
                    CheckInOutButton.Visibility = Visibility.Hidden;
                }
            }
        }

        private void SetInkCanvasFromByteArray(byte[] strokeData)
        {
            if (strokeData == null || strokeData.Length == 0)
                return;

            using (var memoryStream = new MemoryStream(strokeData))
            {
                StrokeCollection strokes = new StrokeCollection(memoryStream);

                DrawingCanvas.Strokes = strokes;
            }
        }

        private int GetIndexOfStrategyItem ()
        {
            return ViewModel.Source.StrategyItems
                .Select((item, idx) => new { item, idx })
                .Where(x => x.item == ViewModel.SelectedItem)
                .Select(x => x.idx)
                .FirstOrDefault();
        }

        private void SetSelectedItem (StrategyItem item)
        {
            ViewModel.SelectedItem = item; 
            ItemDescription.Text = item.Description;
        }

        private void Save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.SaveStrategy(DrawingCanvas, false);
        }

        private void ItemDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.SelectedItem.Description = ItemDescription.Text;

            ViewModel.Source.StrategyItems[GetIndexOfStrategyItem()] = ViewModel.SelectedItem;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int index = GetIndexOfStrategyItem();
            ViewModel.DeletedItems.Add(ViewModel.Source.StrategyItems[index]);
            for (int i = 0; i < ItemCanvas.Children.Count; i++)
            {
                var child = ItemCanvas.Children[i];
                if (child is Image image && Equals((image.Tag as StrategyItem), ViewModel.Source.StrategyItems[index]))
                {
                    ItemCanvas.Children.RemoveAt(i);
                    break;
                }
            }
            ViewModel.Source.StrategyItems.RemoveAt(index);
            ViewModel.SelectedItem = null;
            ItemDescription.Visibility = Visibility.Hidden;
            ItemDescriptionLabel.Visibility = Visibility.Hidden;
            NoneSelected.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Hidden;
            ItemLinkEditor.Visibility = Visibility.Hidden;
            HyperLinkText.Visibility = Visibility.Hidden;
            EditButton.Visibility = Visibility.Hidden;
            ItemLinkLabel.Visibility = Visibility.Hidden;
            UploadPicture.Visibility = Visibility.Hidden;
            SelectedImage.Visibility = Visibility.Hidden;
        }

        private void CheckInOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Source.IsCheckedOut)
            {
                MessageBoxResult result = MessageBox.Show(
                "Would you like to save any unsaved changes when you check in?", 
                "Warning",            
                MessageBoxButton.YesNo,   
                MessageBoxImage.Question  
                );

                ViewModel.Source.IsCheckedOut = false;
                CheckInOutButton.Content = "Check Out";
                if (result == MessageBoxResult.Yes)
                {
                    ViewModel.SaveStrategy(DrawingCanvas, true);
                }
                else
                {
                    ViewModel.UpdateCheckedOut();
                }
                Save.Visibility = Visibility.Hidden;
            } 
            else
            {
                ViewModel.Source.IsCheckedOut = true;
                ViewModel.UpdateCheckedOut();
                CheckInOutButton.Content = "Check In";
                Save.Visibility = Visibility.Visible;
            }
        }

        private void SetStrategy()
        {
            var app = (App)Application.Current;
            ViewModel.Source = app.CurrentStrategy;
            if (ViewModel.Source.StrategyItems == null)
            {
                ViewModel.Source.StrategyItems = new ObservableCollection<StrategyItem>();
            }
            SetOnscreenItems();
            SetCheckInOutVisibilities(app.User);
        }

        private void TestingButton_Click(object sender, RoutedEventArgs e)
        {
            TestingPanel.Visibility = Visibility.Visible;
        }

        private void TestingBackButton_Click(object sender, RoutedEventArgs e)
        {
            TestingPanel.Visibility = Visibility.Collapsed;
        }

        private void RunTestsButton_Click(object sender, RoutedEventArgs e)
        {
            bool result = false;
            //Put methods for testing in this. Send output to console

            result = TestingMethods.CreateStratTest();
            if (result == false)
                Console.WriteLine("Failed: Ids do not match");
            else
                Console.WriteLine("Passed");
            result = TestingMethods.DeleteStratTest();
            if (result == false)
                Console.WriteLine("\nPassed");
            else
                Console.WriteLine("\nFailed: Strat was not deleted");
            result = TestingMethods.ShareRecordTest();
            if (result == false)
                Console.WriteLine("\nFailed: User was not shared with");
            else
                Console.WriteLine("\nPassed");
            result = TestingMethods.GetUsernameByEmailTest();
            if (result == false)
                Console.WriteLine("\nFailed: Username was not correct");
            else
                Console.WriteLine("\nPassed");
        }
    }
}