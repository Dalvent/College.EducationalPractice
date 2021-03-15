using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DGraficLib.WPF
{
    public class WpfRender : IRender
    {
        private readonly Panel panel;

        public WpfRender(Panel panel)
        {
            this.panel = panel;
        }
        public void Show(IRenderable2D renderable2D)
        {

            var polygon = new Polygon();
            polygon.Points = ConvertToPointCollection(renderable2D.VerticesOnPosition);
            polygon.Fill = new SolidColorBrush(ConvertToWpfColor(renderable2D.BackgroundColor));
            polygon.Stretch = Stretch.None;
            panel.Children.Add(polygon);
        }

        private PointCollection ConvertToPointCollection(IEnumerable<PointF> points)
        {
            var pointCollection = new PointCollection();
            foreach (var point in points)
            {
                pointCollection.Add(ConvertToWpfPoint(point));
            }
            return pointCollection;
        }

        private System.Windows.Point ConvertToWpfPoint(PointF point)
        {
            return new System.Windows.Point(point.X, point.Y);
        }

        private System.Windows.Media.Color ConvertToWpfColor(System.Drawing.Color color)
        {
            return new System.Windows.Media.Color() { 
                R = color.R, 
                G = color.G, 
                B = color.B,
                A = 255
            };
        }

        public void Clear()
        {
            panel.Children.Clear();
        }
    }
}
