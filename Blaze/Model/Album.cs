using Blaze.Core;
using Blaze.View.ContentLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Xceed.Wpf.AvalonDock.Themes;

namespace Blaze.Model
{
    public class AlbumDisplayItem
    {
        public Uri AvailableImage { get; set; }
        public int ImageCount { get; set; }
        public bool IsAlbum { get; set; }
        public string AlbumFilter => IsAlbum ? "A" : "Z";
        public string DateModified { get; set; }

        public string Name { get; set; }
        public string PathTo { get; set; }
        public string PathFilter => PathTo.Replace(Constants.FOLDER_LOCATION + "\\", "");
        public string Extention;

        public AlbumDisplayItem(string root, bool isDirectory)
        {
            // Get Name and check if it is root
            Name = root.Split("\\").Last();
            IsAlbum = isDirectory;
            PathTo = Path.GetDirectoryName(root) + "\\";

            if (isDirectory)
            {
                // Get directory info
                DirectoryInfo dir = new DirectoryInfo(root);
                DateModified = dir.CreationTime.ToString();

                // Get images available
                FileInfo file = dir.GetFiles().FirstOrDefault(file => Constants.IMAGE_EXTENTIONS.Contains(file.Extension));
                // Set the image to the first available
                if (file != null)
                    AvailableImage = new Uri(file.FullName);
                //Get Image Count
                ImageCount =  dir.GetFiles().Count(file => Constants.IMAGE_EXTENTIONS.Contains(file.Extension));

                Extention = "";
            }
            else
            {
                // Set the image
                FileInfo file = new FileInfo(root);
                DateModified = file.CreationTime.ToString();
                AvailableImage = new Uri(file.FullName);
                ImageCount = 1;
                Extention = file.Extension;
            }
        }

        public static ObservableCollection<AlbumDisplayItem> GetDisplayItems(string root)
        {
            List<AlbumDisplayItem> rootCollection = new List<AlbumDisplayItem>();

            rootCollection.Add(new AlbumDisplayItem(Constants.FOLDER_LOCATION + "\\" + root, true));

            DirectoryInfo dir = new DirectoryInfo(Constants.FOLDER_LOCATION + "\\" + root);
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (Constants.IMAGE_EXTENTIONS.Contains(file.Extension))
                {
                    rootCollection.Add(new AlbumDisplayItem(file.FullName, false));
                }
            }
            // Add other albums
            DirectoryInfo[] albums = dir.GetDirectories();
            foreach (DirectoryInfo album in albums)
            {
                rootCollection.AddRange(GetDisplayItems(root + "\\" + album.Name));
            }

            return new(rootCollection);
        }

        public bool Rename(string newName, string oldPath, string newPath)
        {
            try
            {
                // If it is an album
                if (IsAlbum)
                {
                    // Make sure it remains an album,
                    // does not already exist
                    if (!Directory.Exists(newPath))
                    {
                        Directory.Move(oldPath, newPath);
                        Name = newName;
                        return true;
                    }
                }
                else
                {
                    // Make sure file does not exist
                    if (!File.Exists(newPath))
                    {
                        File.Move(oldPath, newPath);
                        Name = newName;
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
