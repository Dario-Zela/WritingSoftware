using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blaze.CustomControls
{
    /// <summary>
    /// Interaction logic for ContentPannel.xaml
    /// </summary>
    public partial class ContentPannel : UserControl
    {
        public ContentPannel()
        {
            InitializeComponent();
            Title = "Title";
        }

        public string Title { get; set; }

        private void AllowMoving(object sender, MouseEventArgs e)
        {
            Move.Visibility = Visibility.Visible;
        }

        private void DisallowMoving(object sender, MouseEventArgs e)
        {
            Move.Visibility = Visibility.Hidden;
        }
    }
}
