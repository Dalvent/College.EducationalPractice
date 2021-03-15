using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaratoryWork3
{
    public interface IPanel
    {
        PointF Postion { get; }
        float Width { get; }
        float Height { get; }
    }
}
