using Blaze.Core;
using Blaze.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for ThemeLibrary.xaml
    /// </summary>
    public partial class ThemeLibrary : UserControl
    {
        public RelayCommand MakeNewTheme { get; set; }

        public ThemeLibrary()
        {
            ((App)Application.Current).InitialiseThemes();
            InitializeComponent();
            DataContext = this;
            AvailableThemes.ListViewReference.ItemsSource = ((App)Application.Current).themeNames;
            MakeNewTheme = new RelayCommand(o => { MakeNewThemeDictionary(); });

            AvailableThemes.ListViewReference.SelectionChanged += (s, e) =>
            {
                if (AvailableThemes.ListViewReference?.SelectedItem?.ToString() == null) return;
                
                string filePath = System.IO.Path.GetFullPath(Constants.FOLDER_LOCATION) + "\\Themes\\";
                filePath += AvailableThemes.ListViewReference.SelectedItem.ToString() + ".xaml";
                Preview.DataContext = new Blaze.Model.Theme(filePath);
            };

            if (AvailableThemes.ListViewReference.Items.Count > 0)
            {
                AvailableThemes.ListViewReference.SelectedIndex = 0;
            }
        }

        private void MakeNewThemeDictionary()
        {
            string newFileName = FindNewName();
            using (FileStream stream = new FileStream("../../../Template/ThemeTemplate.xaml", FileMode.Open))
            {
                using (FileStream newStream = new FileStream(newFileName, FileMode.CreateNew))
                {
                    XamlWriter.Save((ResourceDictionary)XamlReader.Load(stream), newStream);
                }
            }
            ((App)Application.Current).InitialiseThemes();
        }

        private string FindNewName()
        {
            int counter = 0;
            string fileName;
            string folderPath = System.IO.Path.GetFullPath(Constants.FOLDER_LOCATION) + "\\Themes\\";
            do
            {
                fileName = counter == 0
                    ? "New Theme.xaml"                 // First try without a counter
                    : $"New Theme ({counter}).xaml";    // Add a counter if needed
                counter++;
            }
            while (File.Exists(System.IO.Path.Combine(folderPath, fileName)));

            return System.IO.Path.Combine(folderPath, fileName);
        }
    }
}
