using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.Core
{
    public static class Constants
    {
        // Location of source folder
        public static string FOLDER_LOCATION = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Blaze";

        // Location of application
        public static string BASE_LOCATION = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

        public static List<string> IMAGE_EXTENTIONS = string.Join("", ImageCodecInfo.GetImageEncoders().Select((codec)
            => { return codec.FilenameExtension.ToLower().Replace(";", string.Empty); })).Split('*').Skip(1).ToList();
    }
}
