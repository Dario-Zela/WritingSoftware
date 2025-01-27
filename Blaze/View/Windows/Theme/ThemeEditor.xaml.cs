using Blaze.Core;
using Blaze.Model;
using Blaze.UIElements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blaze.View.Windows.Theme
{
    /// <summary>
    /// Interaction logic for ThemeEditor.xaml
    /// </summary>
    public partial class ThemeEditor : UserControl
    {
        // Current Context
        public Blaze.Model.Theme ThemeContext;

        // Font Families Available
        public List<FontFamily> fontFamilies = new();
        private void LoadFonts()
        {
            // Enumerate the current set of system fonts,
            // and fill the combo box with the names of the fonts.
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                // FontFamily.Source contains the font family name.
                fontFamilies.Add(fontFamily);
            }
            fontFamilies.Sort(new Comparison<FontFamily>((f1, f2) => { return string.Compare(f1.Source, f2.Source); }));
        }
        
        // Which Preview Button is Active
        private ToggleButton currentlyCheckedPreview;

        public ThemeEditor(string path)
        {
            // Load Fonts and Theme
            LoadFonts();
            ThemeContext = new Blaze.Model.Theme(path);

            InitializeComponent();

            // Set Theme and Fonts
            DataContext = ThemeContext;

            HeaderFont.ItemsSource = fontFamilies;
            BodyFont.ItemsSource = fontFamilies;
            ButtonFont.ItemsSource = fontFamilies;

            // Set Header Preview To Active
            currentlyCheckedPreview = HeaderPreview;
            currentlyCheckedPreview.IsEnabled = false;

            // Preview Data Context
            Preview.DataContext = ThemeContext;

            // Keep Preview visible only if window is large enough
            SizeChanged += (s, e) =>
            {
                Preview.Visibility = ActualWidth > 1000 == true ? Visibility.Visible : Visibility.Collapsed;
                PreviewGridColumn.Width = ActualWidth > 1000 == true ? new GridLength(400) : GridLength.Auto;
            };
        }

        // Keep only one preview open
        private void Preview_Checked(object sender, RoutedEventArgs e)
        {
            if (currentlyCheckedPreview != null)
            {
                var button = (ToggleButton)sender;
                currentlyCheckedPreview.IsChecked = false;
                currentlyCheckedPreview.IsEnabled = true;

                currentlyCheckedPreview = button;
                currentlyCheckedPreview.IsEnabled = false;
            }
        }

        // Open Color Picker
        private void OpenColorPicker(object sender, RoutedEventArgs e)
        {
            ThemeContext.ChangeActiveColor(((Button)sender).Name);
            ColorPopup.IsOpen = true;
        }
    }
}
