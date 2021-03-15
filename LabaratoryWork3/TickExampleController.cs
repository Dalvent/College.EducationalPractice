using DGraficLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LabaratoryWork3
{
    public class TickExampleController
    {
        private readonly float startAngle;
        private float currentMoveAngle;
        private readonly IPanel panel;
        private readonly IRenderable2D figure;
        private readonly float speed;

        public TickExampleController(IPanel panel, IRenderable2D figure, float speed)
        {
            this.panel = panel;
            this.figure = figure;
            this.speed = speed;
            this.startAngle = 0;
        }

        public void OnTick()
        {
            var currentPosition = figure.Transform.Position;
            figure.Transform.Position = GetNextFramePointPosition();

            if (IsMovingRight() && !IsAllVertecies(figure.VerticesOnPosition, IsVertexMaxX))
            {
                SwicthDirection(currentPosition);
            }
            else if (IsMovingBottom() && !IsAllVertecies(figure.VerticesOnPosition, IsVertexMaxY))
            {
                SwicthDirection(currentPosition);
            }
            else if (IsMovingLeft() && !IsAllVertecies(figure.VerticesOnPosition, IsVertexMinX))
            {
                SwicthDirection(currentPosition);
            }
            else if (IsMovingUp() && !IsAllVertecies(figure.VerticesOnPosition, IsVertexMinY))
            {
                SwicthDirection(currentPosition);
            }
        }

        private void SwicthDirection(PointF currentPosition)
        {
            figure.Transform.Position = currentPosition;
            currentMoveAngle += 90;
            figure.Transform.Position = GetNextFramePointPosition();
        }

        private PointF GetNextFramePointPosition()
        {
            return MathHelper.TranslateX(figure.Transform.Position, currentMoveAngle, speed);
        }

        private bool IsMovingRight()
        {
            return currentMoveAngle % 360 == startAngle;
        }

        private bool IsMovingBottom()
        {
            return currentMoveAngle % 360 == startAngle + 90;
        }

        private bool IsMovingLeft()
        {
            return currentMoveAngle % 360 == startAngle + 180;
        }

        private bool IsMovingUp()
        {
            return currentMoveAngle % 360 == startAngle + 270;
        }

        private bool IsAllVertecies(IEnumerable<PointF> vertecies, Func<PointF, bool> condition)
        {
            foreach (var vertex in vertecies)
            {
                if (!condition.Invoke(vertex))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsVertexMinX(PointF vertex)
        {
            return vertex.X > panel.Postion.X;
        }
        private bool IsVertexMaxX(PointF vertex)
        {
            return vertex.X < panel.Width;
        }
        private bool IsVertexMinY(PointF vertex)
        {
            return vertex.Y > panel.Postion.Y;
        }
        private bool IsVertexMaxY(PointF vertex)
        {
            return vertex.Y < panel.Height;
        }
    }
}
