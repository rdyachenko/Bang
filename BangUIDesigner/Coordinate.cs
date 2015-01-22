using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace BangUIDesigner
{
    public class Coordinate
    {
        private Rectangle _rect;
        private Point _center;

        public Coordinate()
        {
            _rect = new Rectangle();
            _center = new Point();
        }

        public int Left
        {
            get { return _rect.Left; }
        }

        public int Right
        {
            get { return _rect.Right; }
        }

        public Point Location
        {
            get { return _rect.Location; }
            set { _rect.Location = value; }
        }

        public Size Size
        {
            get { return _rect.Size; }
            set { _rect.Size = value; }
        }

        public int Top
        {
            get { return _rect.Top; }
        }

        public int Bottom
        {
            get { return _rect.Bottom; }
        }

        public int Width
        {
            get { return _rect.Width; }
            set { _rect.Width = value; }
        }

        public int Height
        {
            get { return _rect.Height; }
            set { _rect.Height = value; }
        }

        public int X
        {
            get { return _rect.X; }
            set { _rect.X = value; }
        }

        public int Y
        {
            get { return _rect.Y; }
            set { _rect.Y = value; }
        }

        public Point Center
        {
            get
            {
                _center.X = _rect.Left + _rect.Width / 2;
                _center.Y = _rect.Top + _rect.Height / 2;
                return _center;
            }
            set
            {
                _center = value;
                _rect.Location = Point.Subtract(_center, new Size(_rect.Width / 2, _rect.Height / 2));
            }
        }

        public Rectangle Rect
        {
            get { return _rect; }
        }
    }
}
