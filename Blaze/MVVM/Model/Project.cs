using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

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
            Project project = d as Project;

            string value = (string)e.NewValue;

            if (project == null) { return; }
            if (project._name == null)
            {
                project._name = value;
                return;
            }

            if (value == null || string.IsNullOrWhiteSpace(value))
                return;

            if (File.Exists(project._projectPath + "\\ProjectMetadata.bz"))
            {
                using (var stream = File.Open(project._projectPath + "\\ProjectMetadata.bz", FileMode.Open))
                {
                    BinaryWriter writer = new BinaryWriter(stream);
                    project._name = value;
                    writer.Write(project._name);
                    writer.Write(project._linkedProjects);
                    writer.Write(project._dateCreated.ToBinary());
                    writer.Write(project._manualSortingPosition);
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
            get { return _projectImage; }
            set
            {
                _projectImage = value;
                ProjectLibrary.Sort();
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
            ProjectImage = path + "\\ProjectImage.jpg";

            if (File.Exists(path + "\\ProjectMetadata.bz"))
            {
                using (var stream = File.Open(path + "\\ProjectMetadata.bz", FileMode.Open))
                {
                    BinaryReader reader = new BinaryReader(stream);
                    Name = reader.ReadString();
                    _linkedProjects = reader.ReadInt32();
                    _dateCreated = DateTime.FromBinary(reader.ReadInt64());
                    _manualSortingPosition = reader.ReadInt32();
                }
            }
            else
            {
                using (var stream = File.Open(path + "\\ProjectMetadata.bz", FileMode.Create))
                {
                    BinaryWriter writer = new BinaryWriter(stream);
                    Name = path.Remove(0, ProjectLibrary.PATH.Length);
                    
                    writer.Write(Name);
                    writer.Write(0);
                    _linkedProjects = 0;
                    
                    _dateCreated = DateTime.Now;
                    writer.Write(DateCreated.ToBinary());

                    string Position = Name.Remove(0, "Project".Length);
                    Position = Position != "" ? Position : "0";

                    _manualSortingPosition = int.Parse(Position);
                    writer.Write(int.Parse(Position));
                }
            }
        }
    }
}
