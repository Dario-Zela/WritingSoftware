using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for SnappingGrid.xaml
    /// </summary>
    public partial class SnappingGrid : UserControl
    {

        public SnappingGrid()
        {
            InitializeComponent();
            Resolution = 10;
            AddNewPannel();
            AddNewPannel();
        }

        public int Resolution { get; set; }

        private double Clamp(double value, double max, double min)
        {
            return (value > max) ? max : (value < min) ? min : value;
        }

        public Point Round(Point value, int resolution)
        {
            Point point = new Point();
            point.X = ((int)value.X) / resolution * resolution;
            point.Y = ((int)value.Y) / resolution * resolution;
            return point;
        }

        private void BringToFront(UIElement element)
        {
            Container.Children.Remove(element);
            Container.Children.Add(element);
        }

        public void AddNewPannel()
        {
            var pannel = new TextPannel();
            pannel.MovementStarted += (s, e) =>
            {
                if(!pannel.AllowMovement)
                    BringToFront(pannel);

                var position = Mouse.GetPosition(this);
                var offset = pannel.Move.TransformToAncestor(pannel).Transform(new Point(0, 0));

                position.Offset(-offset.X, -offset.Y);
                position.Offset(-pannel.Move.Width / 2, - pannel.Move.Height / 2);
                position = Round(position, Resolution);

                position.X = Clamp(position.X, ActualWidth / 2, -ActualWidth / 2);
                position.Y = Clamp(position.Y, ActualHeight / 2, -ActualHeight / 2);

                pannel.Margin = new Thickness(position.X, position.Y, 0, 0);

            };
            Container.Children.Add(pannel);
        }
    }
}
