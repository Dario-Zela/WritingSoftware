using Blaze.Core;
using Blaze.View.Windows.Theme;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        // UI Dependencies
        public static DependencyProperty ItemMarginProperty = DependencyProperty.Register(
            name: "ItemMargin",
            propertyType: typeof(Thickness),
            ownerType: typeof(ThemeListView));

        public Thickness ItemMargin
        {
            get { return (Thickness)base.GetValue(ItemMarginProperty); }
            set { base.SetValue(ItemMarginProperty, value); }
        }

        public static DependencyProperty ItemMouseOverColorProperty = DependencyProperty.Register(
            name: "ItemMouseOverColor",
            propertyType: typeof(SolidColorBrush),
            ownerType: typeof(ThemeListView),
            typeMetadata: new PropertyMetadata((SolidColorBrush)Application.Current.Resources["ApplicationPrimaryBrush"]));

        public Thickness ItemPadding
        {
            get { return (Thickness)GetValue(ItemPaddingProperty); }
            set { SetValue(ItemPaddingProperty, value); }
        }

        public static readonly DependencyProperty ItemPaddingProperty =
            DependencyProperty.Register("ItemPadding", typeof(Thickness), typeof(ThemeListView));

        public SolidColorBrush ItemMouseOverColor
        {
            get { return (SolidColorBrush)base.GetValue(ItemMouseOverColorProperty); }
            set { base.SetValue(ItemMouseOverColorProperty, value); }
        }

        public static DependencyProperty ItemSelectedActiveColorProperty = DependencyProperty.Register(
            name: "ItemSelectedActiveColor",
            propertyType: typeof(SolidColorBrush),
            ownerType: typeof(ThemeListView),
            typeMetadata: new PropertyMetadata((SolidColorBrush)Application.Current.Resources["ApplicationPrimaryBrush"]));

        public SolidColorBrush ItemSelectedActiveColor
        {
            get { return (SolidColorBrush)base.GetValue(ItemSelectedActiveColorProperty); }
            set { base.SetValue(ItemSelectedActiveColorProperty, value); }
        }

        public static DependencyProperty ItemInactiveColorProperty = DependencyProperty.Register(
            name: "ItemInactiveColor",
            propertyType: typeof(SolidColorBrush),
            ownerType: typeof(ThemeListView),
            typeMetadata: new PropertyMetadata((SolidColorBrush)Application.Current.Resources["ApplicationSurface"]));

        public SolidColorBrush SurfaceBrush
        {
            get { return (SolidColorBrush)GetValue(SurfaceBrushProperty); }
            set { SetValue(SurfaceBrushProperty, value); }
        }

        public static readonly DependencyProperty SurfaceBrushProperty =
            DependencyProperty.Register("SurfaceBrush", typeof(SolidColorBrush), typeof(ThemeListView));

        public SolidColorBrush OnSurfaceBrush
        {
            get { return (SolidColorBrush)GetValue(OnSurfaceBrushProperty); }
            set { SetValue(OnSurfaceBrushProperty, value); }
        }

        public static readonly DependencyProperty OnSurfaceBrushProperty =
            DependencyProperty.Register("OnSurfaceBrush", typeof(SolidColorBrush), typeof(ThemeListView));

        public SolidColorBrush SecondaryBrush
        {
            get { return (SolidColorBrush)GetValue(SecondaryBrushProperty); }
            set { SetValue(SecondaryBrushProperty, value); }
        }

        public static readonly DependencyProperty SecondaryBrushProperty =
            DependencyProperty.Register("SecondaryBrush", typeof(SolidColorBrush), typeof(ThemeListView));

        public SolidColorBrush OnSecondaryBrush
        {
            get { return (SolidColorBrush)GetValue(OnSecondaryBrushProperty); }
            set { SetValue(OnSecondaryBrushProperty, value); }
        }

        public static readonly DependencyProperty OnSecondaryBrushProperty =
            DependencyProperty.Register("OnSecondaryBrush", typeof(SolidColorBrush), typeof(ThemeListView));

        public SolidColorBrush ItemInactiveColor
        {
            get { return (SolidColorBrush)base.GetValue(ItemInactiveColorProperty); }
            set { base.SetValue(ItemInactiveColorProperty, value); }
        }

        // List View
        public ListView ListViewReference => ContainerListView;

        public RelayCommand DuplicateTheme {  get; set; }
        public RelayCommand RenameTheme {  get; set; }
        public RelayCommand DeleteTheme { get; set; }
        public RelayCommand EditTheme { get; set; }
        public RelayCommand CloseRenamePopup { get; set; }
        public RelayCommand SaveRename { get; set; }

        public ThemeListView()
        {
            InitializeComponent();
            DataContext = this;
            DuplicateTheme = new RelayCommand(DuplicateThemeMethod);
            RenameTheme = new RelayCommand(StartRenameDialouge);
            SaveRename = new RelayCommand(RenameThemeMethod);
            DeleteTheme = new RelayCommand(DeleteThemeMethod);
            EditTheme = new RelayCommand(EditThemeMethod);
            CloseRenamePopup = new RelayCommand(CloseRenamePopupMethod);

            RenamePopup.Closed += (s, e) => RenameBox.Text = "";
        }

        private void CloseRenamePopupMethod(object obj)
        {
            RenamePopup.IsOpen = false;
        }

        private void ClosePopup(object obj)
        {
            for (int i = 0; i < ContainerListView.Items.Count; i++)
            {
                if (ContainerListView.Items[i] == obj)
                {
                    ContainerListView.FindVisualChildren<ListViewItem>().ElementAt(i).FindVisualChildren<ButtonPopup>().First().IsChecked = false;
                }
            }
        }

        private void StartRenameDialouge(object obj)
        {
            ClosePopup(obj);
            RenamePopup.IsOpen = true;
            OldNameBox.Text = obj.ToString();
        }

        private void EditThemeMethod(object obj)
        {
            string filePath = System.IO.Path.GetFullPath(Constants.FOLDER_LOCATION) + "\\Themes\\";
            filePath += obj.ToString() + ".xaml";
            MainWindow.ChangeScene(new ThemeEditor(filePath));
        }

        private void DeleteThemeMethod(object obj)
        {
            ClosePopup(obj);
            string filePath = System.IO.Path.GetFullPath(Constants.FOLDER_LOCATION) + "\\Themes\\";
            filePath += obj.ToString() + ".xaml";
            File.Delete(filePath);
            ((App)Application.Current).InitialiseThemes();
        }

        private void RenameThemeMethod(object obj)
        {
            string filePath = System.IO.Path.GetFullPath(Constants.FOLDER_LOCATION) + "\\Themes\\";
            string oldFilePath = filePath + OldNameBox.Text + ".xaml";
            string newFilePath = filePath + RenameBox.Text + ".xaml";
            File.Move(oldFilePath, newFilePath);
            ((App)Application.Current).InitialiseThemes();
            RenamePopup.IsOpen = false;
        }

        private void DuplicateThemeMethod(object obj)
        {
            ClosePopup(obj);
            string filePath = System.IO.Path.GetFullPath(Constants.FOLDER_LOCATION) + "\\Themes\\";
            string oldFilePath = filePath + obj.ToString() + ".xaml";
            string newFilePath = FindNewName(obj.ToString());
            File.Copy(oldFilePath, newFilePath);
            ((App)Application.Current).InitialiseThemes();
        }

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

        private void PopupMenu_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Opened");
        }
    }
}
