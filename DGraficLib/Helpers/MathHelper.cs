using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGraficLib
{
    public static class MathHelper
    {
        public static PointF Multiplay(PointF point1, PointF point2)
        {
            var result = new PointF();
            result.X = point1.X * point2.X;
            result.Y = point1.Y * point2.Y;
            return result;
        }

        public static PointF Add(PointF point1, PointF point2)
        {
            var result = new PointF();
            result.X = point1.X + point2.X;
            result.Y = point1.Y + point2.Y;
            return result;
        }

        public static PointF Rotate(PointF point1, float rotation)
        {
            var result = new PointF();
            var radiansRotation = DegreeToRadians(rotation);
            result.X = (((float)Math.Cos(radiansRotation)) * point1.X) - (((float)Math.Sin(radiansRotation)) * point1.Y);
            result.Y = (((float)Math.Sin(radiansRotation)) * point1.X) + (((float)Math.Cos(radiansRotation)) * point1.Y);
            return result;
        }

        public static PointF TranslateX(PointF point, float rotation, float translateLength)
        {
            var result = new PointF();
            var radiansRotation = DegreeToRadians(rotation);
            result.X = (((float)Math.Cos(radiansRotation)) * translateLength);
            result.Y = (((float)Math.Sin(radiansRotation)) * translateLength);
            result = Add(point, result);
            return result;
        }

        public static PointF TranslateY(PointF point, float rotation, float translateLength)
        {
            var result = Rotate(point, rotation);
            result = Add(result, new PointF(0, translateLength));
            return result;
        }

        public static float DegreeToRadians(float degree)
        {
            return (float)(degree * Math.PI / 180);
        }

        public static float RadiansToDegree(float radians)
        {
            return (float)(radians * 180 / Math.PI);
        }
    }
}
