using Blaze.Core;
using Blaze.Model;
using Blaze.UIElements;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Blaze.View.ContentLibrary
{
    /// <summary>
    /// Interaction logic for AlbumDisplay.xaml
    /// </summary>
    public partial class AlbumDisplay : UserControl
    {
        public SolidColorBrush AccentColor
        {
            get { return (SolidColorBrush)GetValue(AccentColorProperty); }
            set { SetValue(AccentColorProperty, value); }
        }

        public static readonly DependencyProperty AccentColorProperty =
            DependencyProperty.Register("AccentColor", typeof(SolidColorBrush), typeof(AlbumDisplay));

        public SolidColorBrush ErrorColor
        {
            get { return (SolidColorBrush)GetValue(ErrorColorProperty); }
            set { SetValue(ErrorColorProperty, value); }
        }

        public static readonly DependencyProperty ErrorColorProperty =
            DependencyProperty.Register("ErrorColor", typeof(SolidColorBrush), typeof(AlbumDisplay));

        public SolidColorBrush OnErrorColor
        {
            get { return (SolidColorBrush)GetValue(OnErrorColorProperty); }
            set { SetValue(OnErrorColorProperty, value); }
        }

        public static readonly DependencyProperty OnErrorColorProperty =
            DependencyProperty.Register("OnErrorColor", typeof(SolidColorBrush), typeof(AlbumDisplay));

        public FontFamily HeaderFont
        {
            get { return (FontFamily)GetValue(HeaderFontProperty); }
            set { SetValue(HeaderFontProperty, value); }
        }

        public static readonly DependencyProperty HeaderFontProperty =
            DependencyProperty.Register("HeaderFont", typeof(FontFamily), typeof(AlbumDisplay));

        public Double HeaderFontSize
        {
            get { return (Double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register("HeaderFontSize", typeof(Double), typeof(AlbumDisplay));

        public FontFamily ButtonFont
        {
            get { return (FontFamily)GetValue(ButtonFontProperty); }
            set { SetValue(ButtonFontProperty, value); }
        }

        public static readonly DependencyProperty ButtonFontProperty =
            DependencyProperty.Register("ButtonFont", typeof(FontFamily), typeof(AlbumDisplay));

        public Double ButtonFontSize
        {
            get { return (Double)GetValue(ButtonFontSizeProperty); }
            set { SetValue(ButtonFontSizeProperty, value); }
        }

        public static readonly DependencyProperty ButtonFontSizeProperty =
            DependencyProperty.Register("ButtonFontSize", typeof(Double), typeof(AlbumDisplay));

        public bool GalleryMode
        {
            get { return (bool)GetValue(GalleryModeProperty); }
            set { SetValue(GalleryModeProperty, value); }
        }

        public static readonly DependencyProperty GalleryModeProperty =
            DependencyProperty.Register("GalleryMode", typeof(bool), typeof(AlbumDisplay));


        public RelayCommand ChangeDirectory;
        public string[] deleteText = ["Are you sure you want to delete this album containing ", " images?"];

        private ObservableCollection<AlbumDisplayItem> DisplayItems = AlbumDisplayItem.GetDisplayItems("Gallery");
        public ICollectionView DisplayView { get; }
        private string search = String.Empty;
        public string Search
        {
            get => search;
            set
            {
                if (value == search) return;
                search = value;
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
                {
                    Refresh();
                });
                CurrentPath = "Gallery";
                GeneratePathButtons();
            }
        }
        public string CurrentPath;

        public AlbumDisplay()
        {
            DisplayView = CollectionViewSource.GetDefaultView(DisplayItems);
            DisplayView.Filter = (item) =>
            {
                if (item is AlbumDisplayItem album)
                {
                    if (Search == String.Empty) return album.PathFilter == CurrentPath + "\\";
                    else
                    {
                        return album.Name.Contains(Search, StringComparison.InvariantCultureIgnoreCase);
                    }
                }
                return false;
            };

            ChangeDirectory = new RelayCommand((directory) =>
            {
                CurrentPath = (string)directory;
                GeneratePathButtons();
                Refresh();
            });
            InitializeComponent();
            Loaded += (s, e) => { Display.SelectionMode = GalleryMode ? SelectionMode.Extended : SelectionMode.Single; };
            DataContext = this;
            ChangeDirectory.Execute("Gallery");
        }

        public void Refresh()
        {
            DisplayView.Refresh();
        }

        private void GeneratePathButtons()
        {
            PathDisplay.Children.Clear();
            if (CurrentPath == "Gallery")
            {
                PathContainer.Visibility = Visibility.Collapsed;
                return;
            }
            else
            {
                PathContainer.Visibility = Visibility.Visible;
            }
            var ParentPathSegments = CurrentPath.Split("\\").ToList();
            for (int i = 0; i < ParentPathSegments.Count(); i++)
            {
                if (i != 0)
                {
                    PathDisplay.Children.Add(new TextBox() { Style = (Style)Resources["PathFactorySeparator"] });
                }
                PathDisplay.Children.Add(new CustomButton()
                {
                    Style = (Style)Resources["PathFactoryButton"],
                    Content = ParentPathSegments[i],
                    Command = ChangeDirectory,
                    CommandParameter = string.Join("\\", ParentPathSegments.GetRange(0, i + 1))
                });
            }
        }

        private void HandleSelection(object sender, RoutedEventArgs e)
        {
            var selected = (sender as Button).DataContext as AlbumDisplayItem;

            if (selected.IsAlbum)
            {
                Search = String.Empty;
                ChangeDirectory.Execute(selected.PathFilter + selected.Name);
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            var ParentPathSegments = string.Join("\\", CurrentPath.Split("\\").ToList()[0..^1]);
            ChangeDirectory.Execute(ParentPathSegments);
        }

        private void UpdateName(object sender, TextChangedEventArgs e)
        {
            var NewName = (sender as TextBox).Text;
            var selected = (sender as TextBox).DataContext as AlbumDisplayItem;
            var OldName = selected.Name;

            if (OldName == NewName) return;

            string filePath = selected.PathTo;
            try
            {
                if (NewName.Contains('\\') || NewName.Contains('/')) throw new Exception();

                string oldFilePath = System.IO.Path.GetFullPath(filePath + OldName + selected.Extention);
                string newFilePath = System.IO.Path.GetFullPath(filePath + NewName + selected.Extention);

                if (selected.Rename(NewName, oldFilePath, newFilePath))
                {
                    ((sender as TextBox).Parent as Border).BorderBrush = Brushes.Transparent;
                    return;
                }
            }
            catch { }

            // In case of error
            ((sender as TextBox).Parent as Border).BorderBrush = ErrorColor;
            (sender as TextBox).Text = selected.Name;
        }

        private void OpenDeletePopup(object sender, RoutedEventArgs e)
        {
            var selected = (sender as CustomButton).DataContext as AlbumDisplayItem;
            DeleteWarning.IsOpen = true;
            DeleteWarning.DataContext = new[] { selected };
            DeleteText.Text = deleteText[0] + CountImages(selected) + deleteText[1];
        }

        public int CountImages(AlbumDisplayItem item)
        {
            if (!item.IsAlbum) return 1;
            return Directory.EnumerateFiles(item.PathTo + item.Name, "*.*", SearchOption.AllDirectories).Count();
        }

        private void ExecuteDelete(object sender, RoutedEventArgs e)
        {
            if (DeleteWarning.DataContext == null) { DeleteWarning.IsOpen = false; return; }
            var deletion = DeleteWarning.DataContext as AlbumDisplayItem[];
            foreach (var del in deletion)
            {
                DeleteItem(del);
            }
            Refresh();
            DeleteWarning.DataContext = null;
            DeleteWarning.IsOpen = false;
        }

        private void CancelDelete(object sender, RoutedEventArgs e)
        {
            DeleteWarning.DataContext = null;
            DeleteWarning.IsOpen = false;
        }

        public void DeleteItem(AlbumDisplayItem item)
        {
            if (!DisplayItems.Contains(item)) return;

            DisplayItems.Remove(item);
            if (item.IsAlbum)
            {
                Directory.Delete(item.PathTo + item.Name, true);
            }
            else
            {
                File.Delete(item.PathTo + item.Name + item.Extention);
            }
        }

        public void MoveItem(AlbumDisplayItem item, string newPath)
        {
            try
            {
                if (item.IsAlbum)
                {
                    Directory.Move(item.PathTo + item.Name + item.Extention, Constants.FOLDER_LOCATION + "\\Gallery\\" +
                        newPath + item.Name + item.Extention);
                }
                else
                {
                    File.Move(item.PathTo + item.Name + item.Extention, Constants.FOLDER_LOCATION + "\\Gallery\\" +
                        newPath + item.Name + item.Extention);
                }

                item.PathTo = Constants.FOLDER_LOCATION + "\\Gallery\\" + newPath;
            }
            catch { }
        }

        public void CreateAlbum(string path)
        {
            Directory.CreateDirectory(path);
            DisplayItems.Add(new AlbumDisplayItem(path, true));
        }

        public void CopyFile(string path, string newPath)
        {
            File.Copy(path, newPath);
            DisplayItems.Add(new AlbumDisplayItem(newPath, false));
        }
    }
}
