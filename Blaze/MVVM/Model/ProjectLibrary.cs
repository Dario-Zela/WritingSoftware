using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace Blaze.Core
{
    //Custom observable collection to allow for sorting
    public class ProjectCollection : ObservableCollection<Project>
    {
        //Sorting method 
        public void Sort<T>(Func<Project, T> keySelector) where T : IComparable
        {
            //Sort the items and add them one by one as Items of frozen
            var projects = Items.OrderBy(x => keySelector(x)).ToArray();
            Items.Clear();
            foreach (var project in projects)
            {
                Items.Add(project);
            }

            //Raise event
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        //Expose onCollectionChaged method
        public void RaiseCollectionChanged()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace));
        }
    }

    //A collection of all project metadata
    public static class ProjectLibrary
    {
        //To change
        //Location of file contatiner
        public static string PATH => Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Blaze\\ExistingProjects\\";

        //The observable collection of projects
        public static ProjectCollection projects;

        //The sorting type
        private static int _sortingMethod = 0;

        //Instantiator
        public static void InitiateLibrary()
        {
            //Check it hasn't already been done
            if (projects != null) return;

            //Get all of the projects
            string[] existingProjects = Directory.GetFiles(PATH);
            projects = new ProjectCollection();

            foreach (string projectPath in existingProjects)
            {
                projects.Add(new Project(projectPath));
            }

            //Get the sorting method and apply it
            _sortingMethod = int.Parse(Properties.Settings.Default.SortingMethod);
            Sort();
        }

        //Sorting router, it takes the thread to the correct sorting method
        public static void Sort()
        {
            switch (_sortingMethod)
            {
                case 0: SortAlphabetically(); return;
                case 1: SortDateCreated(); return;
                case 2: SortManualSorting(); return;
            }
        }

        //Method to create a new project
        public static void NewProject()
        {
            //Creates a new file
            projects.Add(new Project(PATH));

            //Sort the projects
            Sort();
        }

        //Separate sorting methods to be used as commands

        //Alphabetical
        public static void SortAlphabetically()
        {
            projects.Sort(project => project.Name);
            _sortingMethod = 0;
            Properties.Settings.Default.SortingMethod = _sortingMethod.ToString();
            Properties.Settings.Default.Save();
        }

        //By date created
        public static void SortDateCreated()
        {
            projects.Sort(project => project.DateCreated);
            _sortingMethod = 1;
            Properties.Settings.Default.SortingMethod = _sortingMethod.ToString();
            Properties.Settings.Default.Save();
        }

        //Using manual sorting
        //To be implemented
        public static void SortManualSorting()
        {
            projects.Sort(project => project.ManualSortingPosition);
            _sortingMethod = 2;
            Properties.Settings.Default.SortingMethod = _sortingMethod.ToString();
            Properties.Settings.Default.Save();
        }
    }
}
