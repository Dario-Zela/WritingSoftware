using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace Blaze.Core
{
    public class ProjectCollection : ObservableCollection<Project>
    {
        public void Sort<T>(Func<Project, T> keySelector) where T : IComparable
        {
            var projects = Items.OrderBy(x => keySelector(x)).ToArray();
            Items.Clear();
            foreach (var project in projects)
            {
                Items.Add(project);
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void RaiseCollectionChanged()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace));
        }
    }

    public static class ProjectLibrary
    {
        public static string PATH => Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Blaze\\ExistingProjects\\";

        public static ProjectCollection projects;

        private static int _sortingMethod = 0;

        private static int NewNumber = 0;

        public static void InitiateLibrary()
        {
            if (projects != null) return;

            string[] existingProjects = Directory.GetDirectories(PATH);
            projects = new ProjectCollection();

            foreach (string projectPath in existingProjects)
            {
                if (projectPath.Remove(0, PATH.Length + "Project".Length) == ((NewNumber > 0) ? NewNumber.ToString() : ""))
                {
                    NewNumber++;
                }

                projects.Add(new Project(projectPath));
            }

            _sortingMethod = int.Parse(Properties.Settings.Default.SortingMethod);
            Sort();
        }

        public static void Sort()
        {
            switch (_sortingMethod)
            {
                case 0: SortAlphabetically(); return;
                case 1: SortDateCreated(); return;
                case 2: SortManualSorting(); return;
            }
        }

        public static void NewProject()
        {
            var foulderName = Path.Combine(PATH, "Project" + ((NewNumber > 0) ? NewNumber.ToString() : ""));

            Directory.CreateDirectory(foulderName);
            projects.Add(new Project(foulderName));

            foreach (Project project in projects)
            {
                if (project.ProjectPath.Remove(0, PATH.Length + "Project".Length) == ((NewNumber > 0) ? NewNumber.ToString() : ""))
                {
                    NewNumber++;
                }
            }

            Sort();
        }

        public static void SortAlphabetically()
        {
            projects.Sort(project => project.Name);
            _sortingMethod = 0;
            Properties.Settings.Default.SortingMethod = _sortingMethod.ToString();
            Properties.Settings.Default.Save();
        }

        public static void SortDateCreated()
        {
            projects.Sort(project => project.DateCreated);
            _sortingMethod = 1;
            Properties.Settings.Default.SortingMethod = _sortingMethod.ToString();
            Properties.Settings.Default.Save();
        }

        public static void SortManualSorting()
        {
            projects.Sort(project => project.ManualSortingPosition);
            _sortingMethod = 2;
            Properties.Settings.Default.SortingMethod = _sortingMethod.ToString();
            Properties.Settings.Default.Save();
        }
    }
}
