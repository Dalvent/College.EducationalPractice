using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LabaratoryWork3
{
    public class WpfPanel : IPanel
    {
        private readonly Panel panel;

        public WpfPanel(Panel panel)
        {
            this.panel = panel;
        }

        public PointF Postion => new PointF(0, 0);

        public float Width => (float)panel.ActualWidth;

        public float Height => (float)panel.ActualHeight;
    }
}
