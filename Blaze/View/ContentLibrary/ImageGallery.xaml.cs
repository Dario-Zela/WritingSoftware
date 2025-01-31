using Blaze.Core;
using Blaze.Model;
using Blaze.UIElements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.Primitives;

namespace Blaze.View.ContentLibrary
{
    /// <summary>
    /// Interaction logic for ImageGallery.xaml
    /// </summary>
    public partial class ImageGallery : Popup
    {
        // Surface Color of Gallery
        public SolidColorBrush SurfaceColor
        {
            get { return (SolidColorBrush)GetValue(SurfaceColorProperty); }
            set { SetValue(SurfaceColorProperty, value); }
        }

        public static readonly DependencyProperty SurfaceColorProperty =
            DependencyProperty.Register("SurfaceColor", typeof(SolidColorBrush), typeof(ImageGallery));

        // On Surface Color of Gallery
        public SolidColorBrush OnSurfaceColor
        {
            get { return (SolidColorBrush)GetValue(OnSurfaceColorProperty); }
            set { SetValue(OnSurfaceColorProperty, value); }
        }

        public static readonly DependencyProperty OnSurfaceColorProperty =
            DependencyProperty.Register("OnSurfaceColor", typeof(SolidColorBrush), typeof(ImageGallery));

        // Secondary Color of Gallery
        public SolidColorBrush SecondaryColor
        {
            get { return (SolidColorBrush)GetValue(SecondaryColorProperty); }
            set { SetValue(SecondaryColorProperty, value); }
        }

        public static readonly DependencyProperty SecondaryColorProperty =
            DependencyProperty.Register("SecondaryColor", typeof(SolidColorBrush), typeof(ImageGallery));

        // On Secondary Color of Gallery
        public SolidColorBrush OnSecondaryColor
        {
            get { return (SolidColorBrush)GetValue(OnSecondaryColorProperty); }
            set { SetValue(OnSecondaryColorProperty, value); }
        }

        public static readonly DependencyProperty OnSecondaryColorProperty =
            DependencyProperty.Register("OnSecondaryColor", typeof(SolidColorBrush), typeof(ImageGallery));

        // Primary Color of Gallery 
        public SolidColorBrush PrimaryColor
        {
            get { return (SolidColorBrush)GetValue(PrimaryColorProperty); }
            set { SetValue(PrimaryColorProperty, value); }
        }

        public static readonly DependencyProperty PrimaryColorProperty =
            DependencyProperty.Register("PrimaryColor", typeof(SolidColorBrush), typeof(ImageGallery));

        public SolidColorBrush ErrorColor
        {
            get { return (SolidColorBrush)GetValue(ErrorColorProperty); }
            set { SetValue(ErrorColorProperty, value); }
        }

        public static readonly DependencyProperty ErrorColorProperty =
            DependencyProperty.Register("ErrorColor", typeof(SolidColorBrush), typeof(ImageGallery));

        // Button Font and size
        public FontFamily ButtonFont
        {
            get { return (FontFamily)GetValue(ButtonFontProperty); }
            set { SetValue(ButtonFontProperty, value); }
        }

        public static readonly DependencyProperty ButtonFontProperty =
            DependencyProperty.Register("ButtonFont", typeof(FontFamily), typeof(ImageGallery));

        public Double ButtonFontSize
        {
            get { return (Double)GetValue(ButtonFontSizeProperty); }
            set { SetValue(ButtonFontSizeProperty, value); }
        }

        public static readonly DependencyProperty ButtonFontSizeProperty =
            DependencyProperty.Register("ButtonFontSize", typeof(Double), typeof(ImageGallery));

        // Body Font and size
        public FontFamily BodyFont
        {
            get { return (FontFamily)GetValue(BodyFontProperty); }
            set { SetValue(BodyFontProperty, value); }
        }

        public static readonly DependencyProperty BodyFontProperty =
            DependencyProperty.Register("BodyFont", typeof(FontFamily), typeof(ImageGallery));

        public Double BodyFontSize
        {
            get { return (Double)GetValue(BodyFontSizeProperty); }
            set { SetValue(BodyFontSizeProperty, value); }
        }

        public static readonly DependencyProperty BodyFontSizeProperty =
            DependencyProperty.Register("BodyFontSize", typeof(Double), typeof(ImageGallery));

        // Header Font and size
        public FontFamily HeaderFont
        {
            get { return (FontFamily)GetValue(HeaderFontProperty); }
            set { SetValue(HeaderFontProperty, value); }
        }

        public static readonly DependencyProperty HeaderFontProperty =
            DependencyProperty.Register("HeaderFont", typeof(FontFamily), typeof(ImageGallery));

        public Double HeaderFontSize
        {
            get { return (Double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register("HeaderFontSize", typeof(Double), typeof(ImageGallery));

        public bool GalleryMode
        {
            get { return (bool)GetValue(GalleryModeProperty); }
            set { SetValue(GalleryModeProperty, value); }
        }

        public static readonly DependencyProperty GalleryModeProperty =
            DependencyProperty.Register("GalleryMode", typeof(bool), typeof(ImageGallery), new PropertyMetadata(false));

        public RelayCommand SaveAction
        {
            get { return (RelayCommand)GetValue(SaveActionProperty); }
            set { SetValue(SaveActionProperty, value); }
        }

        public static readonly DependencyProperty SaveActionProperty =
            DependencyProperty.Register("SaveAction", typeof(RelayCommand), typeof(ImageGallery));

        public RelayCommand CancelAction
        {
            get { return (RelayCommand)GetValue(CancelActionProperty); }
            set { SetValue(CancelActionProperty, value); }
        }

        public static readonly DependencyProperty CancelActionProperty =
            DependencyProperty.Register("CancelAction", typeof(RelayCommand), typeof(ImageGallery));

        public Visibility ButtonVisibility => GalleryMode ? Visibility.Collapsed : Visibility.Visible;
        public RelayCommand StartSearch { get; set; }
        public AlbumDisplayItem SelectedItem => SelectedItem as AlbumDisplayItem;

        public ImageGallery()
        {
            SurfaceColor = new SolidColorBrush(Colors.White);
            OnSurfaceColor = new SolidColorBrush(Colors.Black);
            SecondaryColor = new SolidColorBrush(Colors.DarkBlue);
            OnSecondaryColor = new SolidColorBrush(Colors.White);
            PrimaryColor = new SolidColorBrush(Colors.Orange);
            ErrorColor = new SolidColorBrush(Colors.Red);

            ButtonFont = Fonts.SystemFontFamilies.First();
            BodyFont = Fonts.SystemFontFamilies.First();
            HeaderFont = Fonts.SystemFontFamilies.First();

            ButtonFontSize = 14;
            BodyFontSize = 14;
            HeaderFontSize = 40;
            StartSearch = new RelayCommand((o) => { AlbumDisplayer.Search = Search; });
            InitializeComponent();
            
            DataContext = this;
            UpdateContext();
            SortSelector.SelectedIndex = 0;
        }

        public string Search { get; set; }

        public void OpenFileSelector(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = ""; // Default file name
            dlg.Filter = "Image Files|*" + string.Join(";*", Constants.IMAGE_EXTENTIONS);

            dlg.Multiselect = true;


            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result.Value)
            {
                foreach (var file in dlg.FileNames)
                {
                    var name = System.IO.Path.GetFileNameWithoutExtension(file);
                    string newDir = FindNewName(AlbumDisplayer.CurrentPath, name, System.IO.Path.GetExtension(file));
                    AlbumDisplayer.CopyFile(file, newDir);
                }
            }

            ((Button)sender).IsEnabled = true;
        }

        public void DropFile(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files != null && files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        var valid = Constants.IMAGE_EXTENTIONS.Contains(System.IO.Path.GetExtension(file));
                        if (valid)
                        {
                            var name = System.IO.Path.GetFileNameWithoutExtension(file);
                            string newDir = FindNewName(AlbumDisplayer.CurrentPath, name, System.IO.Path.GetExtension(file));
                            AlbumDisplayer.CopyFile(file, newDir);

                            ImageAdderButton.Background = SurfaceColor;
                            ImageAdderButton.BorderBrush = SecondaryColor;
                        }
                        else
                        {
                            ImageAdderButton.Background = ErrorColor;
                            ImageAdderButton.BorderBrush = ErrorColor;
                        }
                    }
                }
            }
        }

        public void DragFile(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        public void UpdateContext()
        {
            var root = Constants.FOLDER_LOCATION + "\\Gallery";
            var context = Directory.EnumerateDirectories(root, "*", SearchOption.AllDirectories)
                .Select((dir) => dir.Replace(root + "\\", string.Empty)).ToList();
            context.Insert(0, "Image Gallery");
            MoveButton.ItemsSource = context;
        }

        public void AlphabeticalSort(object sender, RoutedEventArgs e)
        {
            AlbumDisplayer.DisplayView.SortDescriptions.Clear();
            AlbumDisplayer.DisplayView.SortDescriptions.Add(new SortDescription(
                nameof(AlbumDisplayItem.Name), ListSortDirection.Ascending));
        }

        public void RecentSort(object sender, RoutedEventArgs e)
        {
            AlbumDisplayer.DisplayView.SortDescriptions.Clear();
            AlbumDisplayer.DisplayView.SortDescriptions.Add(new SortDescription(
                nameof(AlbumDisplayItem.DateModified), ListSortDirection.Descending));
        }

        public void AlbumsSort(object sender, RoutedEventArgs e)
        {
            AlbumDisplayer.DisplayView.SortDescriptions.Clear();
            AlbumDisplayer.DisplayView.SortDescriptions.Add(new SortDescription(
                nameof(AlbumDisplayItem.AlbumFilter), ListSortDirection.Ascending));
        }

        // Find available name for new theme
        private string FindNewName(string folderPath, string defaultName = "New Album", string extention = "")
        {
            int counter = 0;
            string fileName;
            folderPath = Constants.FOLDER_LOCATION + "\\" + folderPath;
            do
            {
                fileName = counter == 0
                    ? $"{defaultName}{extention}"                 // First try without a counter
                    : $"{defaultName} ({counter}){extention}";    // Add a counter if needed
                counter++;
            }
            while (Directory.Exists(System.IO.Path.Combine(folderPath, fileName))
                    || File.Exists(System.IO.Path.Combine(folderPath, fileName)));

            return System.IO.Path.Combine(folderPath, fileName);
        }

        private void CreateNewAlbum(object sender, RoutedEventArgs e)
        {
            string newDir = FindNewName(AlbumDisplayer.CurrentPath);
            AlbumDisplayer.CreateAlbum(newDir);
            AlbumDisplayer.Refresh();
            UpdateContext();
        }

        private void DeleteSelected(object sender, RoutedEventArgs e)
        {
            var ItemList = AlbumDisplayer.Display.SelectedItems.Cast<AlbumDisplayItem>().ToArray();

            AlbumDisplayer.DeleteWarning.IsOpen = true;

            AlbumDisplayer.DeleteWarning.DataContext = ItemList;

            int TotalFiles = ItemList
                .Select((item) => AlbumDisplayer.CountImages(item)).ToArray().Sum();

            AlbumDisplayer.DeleteText.Text = AlbumDisplayer.deleteText[0] + 
                TotalFiles +
                AlbumDisplayer.deleteText[1];
        }

        private void MoveSelected(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as CustomComboBox).SelectedIndex == -1) return;

            var selection = (sender as CustomComboBox).SelectedItem as string;

            selection = selection == "Image Gallery" ? "" : selection + "\\";

            var ItemList = AlbumDisplayer.Display.SelectedItems.Cast<AlbumDisplayItem>().ToArray();
            foreach (var item in ItemList)
            {
                AlbumDisplayer.MoveItem(item, selection);
            }

            AlbumDisplayer.Refresh();
            UpdateContext();
            (sender as CustomComboBox).SelectedIndex = -1;
        }
    }
}
