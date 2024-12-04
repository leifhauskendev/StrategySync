using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        private void EraserBtn_Click(object sender, RoutedEventArgs e)
        {
            SetEditingMode(EditingMode.Eraser);
        }

        private void PenBtn_Click(object sender, RoutedEventArgs e)
        {
            SetEditingMode(EditingMode.Pen);
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
            this.dragObject = null;
            this.ItemCanvas.ReleaseMouseCapture();
        }

        private void NewItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image clickedItem = sender as Image;
            Image newItem = new Image();
            newItem.Width = 50;
            newItem.Height = 50;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            switch (clickedItem.Name)
            {
                case "Flashbang":
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/f/f3/Flashbanghud_csgo.png/revision/latest?cb=20211113165814", UriKind.Absolute);
                    break;
                case "SmokeGrenade":
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/7/76/Smokegrenadehud_csgo.png/revision/latest?cb=20211113165620", UriKind.Absolute);
                    break;
                case "Molotov":
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/5/56/Molotovhud.png/revision/latest?cb=20211113171930", UriKind.Absolute);
                    break;
                case "HeGrenade":
                    bitmap.UriSource = new Uri("https://static.wikia.nocookie.net/cswikia/images/c/ce/Hegrenadehud_csgo.png/revision/latest/thumbnail/width/360/height/360?cb=20211113165930", UriKind.Absolute);
                    break;
                case "Friendly":
                    bitmap.UriSource = new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/8/8e/Pan_Blue_Circle.png/640px-Pan_Blue_Circle.png", UriKind.Absolute);
                    newItem.Width = 20;
                    newItem.Height = 20;
                    break;
                case "Enemy":
                    bitmap.UriSource = new Uri("https://upload.wikimedia.org/wikipedia/commons/b/b1/Red-Circle-Transparent.png", UriKind.Absolute);
                    newItem.Width = 20;
                    newItem.Height = 20;
                    break;

            }
            bitmap.EndInit();
            newItem.Source = bitmap;
            Canvas.SetTop(newItem, 20);
            Canvas.SetLeft(newItem, 20);
            newItem.PreviewMouseDown += item_PreviewMouseDown;
            ItemCanvas.Children.Add(newItem);
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
        }
    }
}
