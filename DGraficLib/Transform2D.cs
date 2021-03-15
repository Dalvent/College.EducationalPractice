using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGraficLib
{
    public class Transform2D
    {
        public Transform2D()
        {
        }
        public PointF Position { get; set; }
        public float Rotation { get; set; }
        public PointF Scale { get; set; } = new PointF(1, 1);
    }
}
