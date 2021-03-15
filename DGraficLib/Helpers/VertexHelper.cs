using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGraficLib
{
    public static class VertexHelper
    {
        public static IEnumerable<PointF> GenerateHexagon(int verticesCount, float radius)
        {
            var vertices = new PointF[verticesCount];
            var angle = Math.PI * 2 / verticesCount;
            for (int i = 1; i <= verticesCount; i++)
            {
                vertices[i - 1].X = (float)Math.Round((float)Math.Sin(i * angle) * radius, 6);
                vertices[i - 1].Y = (float)Math.Round((float)Math.Cos(i * angle) * radius, 6);
            }
            return vertices;
        }
    }
}
