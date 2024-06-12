using System;
using System.IO;
using System.Windows;

namespace Blaze.Core
{
    public class Project : Observable
    {
        private string _name;
        private static DependencyProperty NameProperty = DependencyProperty.Register(
                        "Name", typeof(string), typeof(Project),
                        new PropertyMetadata(NamePropertyChanged));


        private static void NamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string value = (string)e.NewValue;
            if (!(d is Project project)) { return; }

            if (project._name == null)
            {
                project._name = value;
                return;
            }

            if (value == null || string.IsNullOrWhiteSpace(value))
                return;

            if (File.Exists(project._projectPath))
            {
                using (var stream = File.Open(project._projectPath, FileMode.Open))
                {
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(value);
                    writer.Write(project._linkedProjects);
                    writer.Write(project._dateCreated.ToBinary());
                    writer.Write(project._manualSortingPosition);
                    writer.Write(project._projectImage);
                }
            }

            project._name = value;
            ProjectLibrary.Sort();
        }

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        private string _projectPath;
        public string ProjectPath
        {
            get { return _projectPath; }
            set
            {
                _projectPath = value;
                ProjectLibrary.Sort();
            }
        }


        private int _linkedProjects;
        public int LinkedProjects
        {
            get { return _linkedProjects; }
            set
            {
                _linkedProjects = value;
                ProjectLibrary.Sort();
            }
        }


        private int _manualSortingPosition;
        public int ManualSortingPosition
        {
            get { return _manualSortingPosition; }
            set
            {
                _manualSortingPosition = value;
                OnPropertyChanged();
                ProjectLibrary.Sort();
            }
        }

        private string _projectImage;
        public string ProjectImage
        {
            get 
            {
                return File.Exists(_projectImage) ? _projectImage : null;
            }
            set
            {
                _projectImage = value;
            }
        }

        private DateTime _dateCreated;

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set
            {
                _dateCreated = value;
                OnPropertyChanged();
                ProjectLibrary.Sort();
            }
        }

        public Project(string path)
        {

            ProjectPath = path;

            if (File.Exists(path))
            {
                using (var stream = File.Open(path, FileMode.Open))
                {

                    BinaryReader reader = new BinaryReader(stream);
                    Name = reader.ReadString();
                    _linkedProjects = reader.ReadInt32();
                    _dateCreated = DateTime.FromBinary(reader.ReadInt64());
                    _manualSortingPosition = reader.ReadInt32();
                    ProjectImage = reader.ReadString();
                }
            }
            else
            {
                using (var stream = File.Open(path + $@"{Guid.NewGuid()}.bz", FileMode.Create))
                {
                    BinaryWriter writer = new BinaryWriter(stream);
                    Name = "New Project";

                    writer.Write(Name);
                    writer.Write(0);
                    _linkedProjects = 0;

                    _dateCreated = DateTime.Now;
                    writer.Write(DateCreated.ToBinary());

                    _manualSortingPosition = ProjectLibrary.projects.Count;
                    writer.Write(ProjectLibrary.projects.Count);

                    writer.Write("none");
                }
            }
        }
    }
}
