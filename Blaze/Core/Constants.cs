using System;
using System.Collections.Generic;
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
    }
}
