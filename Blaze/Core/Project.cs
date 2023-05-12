using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Blaze.Core
{
    class Project
    {
		private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value;}
		}

		private string _projectPath;
		public string ProjectPath
		{
			get { return _projectPath; }
			set { _projectPath = value; }
		}

		public static implicit operator MenuItem(Project project) => new MenuItem() { Header = project.Name };
    }
}
