﻿using System;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using Blaze.Core;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Blaze
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ObservableCollection<Uri> themes = new();
        public ObservableCollection<string> themeNames = new();

        public App() : base()
        {
            InitialiseThemes();
            AppContext.SetSwitch("Switch.System.Windows.Controls.Text.UseAdornerForTextboxSelectionRendering", false);
        }

        public void InitialiseThemes()
        {
            themes.Clear();
            themeNames.Clear();
            DirectoryInfo dir = new DirectoryInfo(Constants.FOLDER_LOCATION + "\\Themes");
            FileInfo[] Files = dir.GetFiles("*.xaml");
            foreach (FileInfo file in Files)
            {
                themes.Add(new Uri(file.FullName));
                themeNames.Add(Path.GetFileNameWithoutExtension(file.FullName));
            }
        }

        public ResourceDictionary ThemeDictionary
        {
            // You could probably get it via its name with some query logic as well.
            get { return Resources.MergedDictionaries[1]; }
        }

        public void ChangeTheme(Uri uri)
        {
            ThemeDictionary.MergedDictionaries.Remove(ThemeDictionary);
            ThemeDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        }

        public void ChangeTheme(int themeIdx)
        {
            ChangeTheme(themes[themeIdx]);
        }

    }
}

