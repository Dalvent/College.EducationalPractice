using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMath
{
    public static class TriangleFormulas
    {
        public static double Perimeter(double a, double b, double c)
        {
            return (a + b + c) / 2;
        }
        public static double Square(double a, double b, double c)
        {
            if (!IsTriangleExist(a, b, c))
                throw new TriangleNotExistException();

            var perimeter = Perimeter(a, b, c);
            return Math.Sqrt(perimeter * (perimeter - a) * (perimeter - b) * (perimeter - c));
        }
        public static bool IsTriangleExist(double a, double b, double c)
        {
            const int MIN_SIDE_INDEX = 0;
            const int MIDDLE_SIDE_INDEX = 1;
            const int MAX_SIDE_INDEX = 2;

            var sides = (new double[] { a, b, c })
                .OrderBy(side => side)
                .ToList();
            return sides[MIN_SIDE_INDEX] + sides[MIDDLE_SIDE_INDEX] > sides[MAX_SIDE_INDEX];
        }
    }


    [Serializable]
    public class TriangleNotExistException : Exception
    {
        public TriangleNotExistException() { }
        public TriangleNotExistException(string message) : base(message) { }
        public TriangleNotExistException(string message, Exception inner) : base(message, inner) { }
        protected TriangleNotExistException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
