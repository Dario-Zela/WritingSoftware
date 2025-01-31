using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0) return;
        string root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Blaze";

        if (args[0] == "installer")
        {
            Directory.CreateDirectory(root);
            Directory.CreateDirectory(root + "\\Gallery");
        }
        if (args[0] == "uninstaller")
        {
            if (Directory.Exists(root) && Directory.Exists(root + "\\Gallery"))
            {
                Directory.Delete(root, true);
            }
        }

    }
}
