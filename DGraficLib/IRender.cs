using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGraficLib
{
    public interface IRender
    {
        void Show(IRenderable2D renderable2D);
        void Clear();
    }
}
