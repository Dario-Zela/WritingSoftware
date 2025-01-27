using Blaze.Core;
using Blaze.View.Windows.Theme;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.AvalonDock.Controls;

namespace Blaze.UIElements
{
    /// <summary>
    /// Interaction logic for ThemeListView.xaml
    /// </summary>
    public partial class ThemeListView : UserControl
    {
        // Dependency Proprieties of List View

        // Margin of List View Items
        public Thickness ItemMargin
        {
            get { return (Thickness)base.GetValue(ItemMarginProperty); }
            set { base.SetValue(ItemMarginProperty, value); }
        }

        public static DependencyProperty ItemMarginProperty = DependencyProperty.Register(
            name: "ItemMargin",
            propertyType: typeof(Thickness),
            ownerType: typeof(ThemeListView));


        // Mouse over color
        public SolidColorBrush ItemMouseOverColor
        {
            get { return (SolidColorBrush)base.GetValue(ItemMouseOverColorProperty); }
            set { base.SetValue(ItemMouseOverColorProperty, value); }
        }

        public static DependencyProperty ItemMouseOverColorProperty = DependencyProperty.Register(
            name: "ItemMouseOverColor",
            propertyType: typeof(SolidColorBrush),
            ownerType: typeof(ThemeListView),
            typeMetadata: new PropertyMetadata((SolidColorBrush)Application.Current.Resources["ApplicationPrimaryBrush"]));

        // Padding of items
        public Thickness ItemPadding
        {
            get { return (Thickness)GetValue(ItemPaddingProperty); }
            set { SetValue(ItemPaddingProperty, value); }
        }

        public static readonly DependencyProperty ItemPaddingProperty =
            DependencyProperty.Register("ItemPadding", typeof(Thickness), typeof(ThemeListView));

        // Selected color of active item
        public SolidColorBrush ItemSelectedActiveColor
        {
            get { return (SolidColorBrush)base.GetValue(ItemSelectedActiveColorProperty); }
            set { base.SetValue(ItemSelectedActiveColorProperty, value); }
        }

        public static DependencyProperty ItemSelectedActiveColorProperty = DependencyProperty.Register(
            name: "ItemSelectedActiveColor",
            propertyType: typeof(SolidColorBrush),
            ownerType: typeof(ThemeListView),
            typeMetadata: new PropertyMetadata((SolidColorBrush)Application.Current.Resources["ApplicationPrimaryBrush"]));

        // Surface Brush for list view
        public SolidColorBrush SurfaceBrush
        {
            get { return (SolidColorBrush)GetValue(SurfaceBrushProperty); }
            set { SetValue(SurfaceBrushProperty, value); }
        }

        public static readonly DependencyProperty SurfaceBrushProperty =
            DependencyProperty.Register("SurfaceBrush", typeof(SolidColorBrush), typeof(ThemeListView));

        // On Surface Brush for list view
        public SolidColorBrush OnSurfaceBrush
        {
            get { return (SolidColorBrush)GetValue(OnSurfaceBrushProperty); }
            set { SetValue(OnSurfaceBrushProperty, value); }
        }

        public static readonly DependencyProperty OnSurfaceBrushProperty =
            DependencyProperty.Register("OnSurfaceBrush", typeof(SolidColorBrush), typeof(ThemeListView));

        // Secondary Brush for list view
        public SolidColorBrush SecondaryBrush
        {
            get { return (SolidColorBrush)GetValue(SecondaryBrushProperty); }
            set { SetValue(SecondaryBrushProperty, value); }
        }

        public static readonly DependencyProperty SecondaryBrushProperty =
            DependencyProperty.Register("SecondaryBrush", typeof(SolidColorBrush), typeof(ThemeListView));

        // On Secondary Brush for list view
        public SolidColorBrush OnSecondaryBrush
        {
            get { return (SolidColorBrush)GetValue(OnSecondaryBrushProperty); }
            set { SetValue(OnSecondaryBrushProperty, value); }
        }

        public static readonly DependencyProperty OnSecondaryBrushProperty =
            DependencyProperty.Register("OnSecondaryBrush", typeof(SolidColorBrush), typeof(ThemeListView));


        // List View
        public ListView ListViewReference => ContainerListView;

        // Commands
        public RelayCommand EditTheme { get; set; }
        public RelayCommand CloseRenamePopup { get; set; }
        public RelayCommand SaveRename { get; set; }

        public ThemeListView()
        {
            InitializeComponent();
            DataContext = this;
            SaveRename = new RelayCommand(RenameThemeMethod);
            EditTheme = new RelayCommand(EditThemeMethod);
            CloseRenamePopup = new RelayCommand(CloseRenamePopupMethod);

            RenamePopup.Closed += (s, e) => RenameBox.Text = "";
        }

        // Force Close Rename Popup
        private void CloseRenamePopupMethod(object obj)
        {
            RenamePopup.IsOpen = false;
        }

        // Start Rename Popup
        private void StartRenameDialouge(object sender, RoutedEventArgs e)
        {
            object obj = (sender as ComboBoxItem).DataContext;
            RenameError.Visibility = Visibility.Collapsed;
            RenamePopup.IsOpen = true;
            OldNameBox.Text = obj.ToString();
        }

        // Open Theme Editor
        private void EditThemeMethod(object obj)
        {
            string filePath = System.IO.Path.GetFullPath(Constants.FOLDER_LOCATION) + "\\Themes\\";
            filePath += obj.ToString() + ".xaml";
            var editor = new ThemeEditor(filePath);
            MainWindow.ChangeScene(editor);
        }

        // Delete Theme c
        private void DeleteThemeMethod(object sender, RoutedEventArgs e)
        {
            object obj = (sender as ComboBoxItem).DataContext;
            string filePath = System.IO.Path.GetFullPath(Constants.FOLDER_LOCATION) + "\\Themes\\";
            filePath += obj.ToString() + ".xaml";
            File.Delete(filePath);
            ((App)Application.Current).InitialiseThemes();
        }

        // Rename Theme 
        private void RenameThemeMethod(object obj)
        {
            string filePath = System.IO.Path.GetFullPath(Constants.FOLDER_LOCATION) + "\\Themes\\";
            string oldFilePath = filePath + OldNameBox.Text + ".xaml";
            string newFilePath = filePath + RenameBox.Text + ".xaml";

            // Make sure file does not exist
            if (File.Exists(newFilePath))
            {
                RenameError.Visibility = Visibility.Visible;
                return;
            }
            
            File.Move(oldFilePath, newFilePath);
            ((App)Application.Current).InitialiseThemes();
            
            RenamePopup.IsOpen = false;
        }

        // Duplicate Theme 
        private void DuplicateThemeMethod(object sender, RoutedEventArgs e)
        {
            object obj = (sender as ComboBoxItem).DataContext;
            string filePath = System.IO.Path.GetFullPath(Constants.FOLDER_LOCATION) + "\\Themes\\";
            string oldFilePath = filePath + obj.ToString() + ".xaml";
            string newFilePath = FindNewName(obj.ToString());
            File.Copy(oldFilePath, newFilePath);
            ((App)Application.Current).InitialiseThemes();
        }

        // Find avaliable name for theme 
        private string FindNewName(string name)
        {
            int counter = 0;
            string fileName;
            string folderPath = System.IO.Path.GetFullPath(Constants.FOLDER_LOCATION) + "\\Themes\\";
            do
            {
                fileName = counter == 0
                    ? $"{name}.xaml"                 // First try without a counter
                    : $"{name} ({counter}).xaml";    // Add a counter if needed
                counter++;
            }
            while (File.Exists(System.IO.Path.Combine(folderPath, fileName)));

            return System.IO.Path.Combine(folderPath, fileName);
        }
    }
}
