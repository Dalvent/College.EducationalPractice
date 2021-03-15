using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGraficLib
{
    public class Figure2D : IRenderable2D
    {
        public Figure2D(IEnumerable<PointF> vertices, Transform2D transform, Color backgroundColor)
        {
            Vertices = vertices;
            Transform = transform;
            BackgroundColor = backgroundColor;
        }
        public IEnumerable<PointF> Vertices { get; set; }
        public Transform2D Transform { get; set; }
        public Color BackgroundColor { get; set; }
        public IEnumerable<PointF> VerticesOnPosition
        {
            get
            {
                var result = Vertices.ToList();
                for (int i = 0; i < result.Count; i++)
                {
                    result[i] = MathHelper.Multiplay(result[i], Transform.Scale);
                    result[i] = MathHelper.Rotate(result[i], Transform.Rotation);
                    result[i] = MathHelper.Add(result[i], Transform.Position);
                }
                return result;
            }
        }
    }
}
