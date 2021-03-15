using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGraficLib
{
    public interface IRenderable2D
    {
        IEnumerable<PointF> Vertices { get; }
        Color BackgroundColor { get; }
        Transform2D Transform { get; set; }
        IEnumerable<PointF> VerticesOnPosition { get; }
    }
}
