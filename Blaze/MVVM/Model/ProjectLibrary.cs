using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace Blaze.Core
{
    public class ProjectLibrary
    {
        private string PATH = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Blaze\\ExistingProjects\\";

        public ObservableCollection<Project> projects;

        public ProjectLibrary()
        {
            string[] existingProjects = Directory.GetDirectories(PATH);
            projects = new ObservableCollection<Project>();

            foreach (string projectPath in existingProjects)
            {
                projects.Add(new Project() { Name = projectPath.Remove(0, PATH.Length),  ProjectPath = projectPath});
            }
        }
    }
}
