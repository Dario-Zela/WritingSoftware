using Blaze.Core;
using Blaze.View.Windows.Theme;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Windows.Devices.Printers;

namespace Blaze.Model
{
    public class Theme : Observable
    {
        // Colors
        public Color PrimaryColor
        {
            get { return (ThemeDictionary["PrimaryBrush"] as SolidColorBrush).Color; }
            set
            {
                ThemeDictionary["PrimaryBrush"] = new SolidColorBrush(value);
                OnPropertyChanged();
            }
        }
        public Color SecondaryColor
        {
            get { return (ThemeDictionary["SecondaryBrush"] as SolidColorBrush).Color; }
            set
            {
                ThemeDictionary["SecondaryBrush"] = new SolidColorBrush(value);
                OnPropertyChanged();
            }
        }
        public Color SurfaceColor
        {
            get { return (ThemeDictionary["SurfaceBrush"] as SolidColorBrush).Color; }
            set
            {
                ThemeDictionary["SurfaceBrush"] = new SolidColorBrush(value);
                OnPropertyChanged();
            }
        }
        public Color BackgroundColor
        {
            get { return (ThemeDictionary["BackgroundBrush"] as SolidColorBrush).Color; }
            set
            {
                ThemeDictionary["BackgroundBrush"] = new SolidColorBrush(value);
                OnPropertyChanged();
            }
        }
        public Color ErrorColor
        {
            get { return (ThemeDictionary["ErrorBrush"] as SolidColorBrush).Color; }
            set
            {
                ThemeDictionary["ErrorBrush"] = new SolidColorBrush(value);
                OnPropertyChanged();
            }
        }

        //On Colors

        public Color OnPrimaryColor
        {
            get { return (ThemeDictionary["OnPrimaryBrush"] as SolidColorBrush).Color; }
            set
            {
                ThemeDictionary["OnPrimaryBrush"] = new SolidColorBrush(value);
                OnPropertyChanged();
            }
        }
        public Color OnSecondaryColor
        {
            get { return (ThemeDictionary["OnSecondaryBrush"] as SolidColorBrush).Color; }
            set
            {
                ThemeDictionary["OnSecondaryBrush"] = new SolidColorBrush(value);
                OnPropertyChanged();
            }
        }
        public Color OnSurfaceColor
        {
            get { return (ThemeDictionary["OnSurfaceBrush"] as SolidColorBrush).Color; }
            set
            {
                ThemeDictionary["OnSurfaceBrush"] = new SolidColorBrush(value);
                OnPropertyChanged();
            }
        }
        public Color OnBackgroundColor
        {
            get { return (ThemeDictionary["OnBackgroundBrush"] as SolidColorBrush).Color; }
            set
            {
                ThemeDictionary["OnBackgroundBrush"] = new SolidColorBrush(value);
                OnPropertyChanged();
            }
        }
        public Color OnErrorColor
        {
            get { return (ThemeDictionary["OnErrorBrush"] as SolidColorBrush).Color; }
            set
            {
                ThemeDictionary["OnErrorBrush"] = new SolidColorBrush(value);
                OnPropertyChanged();
            }
        }


        private string currentlyActiveBrush = "PrimaryBrush";
        private string currentlyActiveColor => currentlyActiveBrush.Substring(0, currentlyActiveBrush.Length - 5) + "Color";
        public Color CurrentlyActiveColor
        {
            get { return (ThemeDictionary[currentlyActiveBrush] as SolidColorBrush).Color; }
            set
            {
                ThemeDictionary[currentlyActiveBrush] = new SolidColorBrush(value);
                OnPropertyChanged(currentlyActiveColor);
                OnPropertyChanged();
            }
        }

        // Fonts
        public FontFamily HeaderFont
        {
            get { return (ThemeDictionary["HeaderFont"] as FontFamily); }
            set
            {
                ThemeDictionary["HeaderFont"] = value;
                OnPropertyChanged();
            }
        }
        public string HeaderFontSize
        {
            get { return ((Double)ThemeDictionary["HeaderFontSize"]).ToString(); }
            set
            {
                Double possibleSize;
                if (Double.TryParse(value, out possibleSize))
                {
                    ThemeDictionary["HeaderFontSize"] = Math.Clamp(possibleSize, 10, 60);
                    OnPropertyChanged();
                }
            }
        }

        public FontFamily BodyFont
        {
            get { return (ThemeDictionary["BodyFont"] as FontFamily); }
            set
            {
                ThemeDictionary["BodyFont"] = value;
                OnPropertyChanged();
            }
        }
        public string BodyFontSize
        {
            get { return ((Double)ThemeDictionary["BodyFontSize"]).ToString(); }
            set
            {
                Double possibleSize;
                if (Double.TryParse(value, out possibleSize))
                {
                    ThemeDictionary["BodyFontSize"] = Math.Clamp(possibleSize, 10, 60);
                    OnPropertyChanged();
                }
            }
        }

        public FontFamily ButtonFont
        {
            get { return (ThemeDictionary["ButtonFont"] as FontFamily); }
            set
            {
                ThemeDictionary["ButtonFont"] = value;
                OnPropertyChanged();
            }
        }
        public string ButtonFontSize
        {
            get { return ((Double)ThemeDictionary["ButtonFontSize"]).ToString(); }
            set
            {
                Double possibleSize;
                if (Double.TryParse(value, out possibleSize))
                {
                    ThemeDictionary["ButtonFontSize"] = Math.Clamp(possibleSize, 10, 60);
                    OnPropertyChanged();
                }
            }
        }

        // Images
        public Image BackgroundImage
        {
            get { return (ThemeDictionary["BackgroundImage"] as Image); }
            set
            {
                ThemeDictionary["BackgroundImage"] = value;
                OnPropertyChanged();
            }
        }
        public Image SurfaceImage
        {
            get { return (ThemeDictionary["SurfaceImage"] as Image); }
            set
            {
                ThemeDictionary["SurfaceImage"] = value;
                OnPropertyChanged();
            }
        }

        // Dictionary
        private ResourceDictionary ThemeDictionary;
        private string Path;

        // Commands
        public RelayCommand Save { get; set; }
        public RelayCommand Back { get; set; }

        public Theme(string path)
        {
            Path = path;
            LoadDictionary();
            Save = new RelayCommand(o => { SaveDictionary(); MainWindow.ChangeScene(new ThemeLibrary()); });
            Back = new RelayCommand(o => MainWindow.ChangeScene(new ThemeLibrary()));
        }

        public void ChangeActiveColor(string brush)
        {
            currentlyActiveBrush = brush;
            OnPropertyChanged("CurrentlyActiveColor");
        }

        public void LoadDictionary()
        {
            using (FileStream stream = new FileStream(Path, FileMode.Open))
            {
                ThemeDictionary = (ResourceDictionary)XamlReader.Load(stream);
            }
        }

        public void SaveDictionary()
        {
            using (FileStream stream = new FileStream(Path, FileMode.Create))
            {
                XamlWriter.Save(ThemeDictionary, stream);
            }
        }
    }
}
