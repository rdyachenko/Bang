using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace BangUIDesigner
{

    public interface IGraphicDriver
    {
        void Init(Graphics gr, Size size);
        void Draw();
    }

    public class GraphicDriver : Coordinate
    {
        protected Graphics _graphic;
        private Graphics _parentGraphic;
        private BufferedGraphicsContext context;
        private BufferedGraphics grafx;

        public GraphicDriver() : base()
        {
            context = BufferedGraphicsManager.Current;
        }

        public GraphicDriver(Graphics gr) : base()
        {
            _graphic = gr;
        }

        public virtual void Init(Graphics gr, Size size)
        {
            context.MaximumBuffer = new Size(size.Width + 1, size.Height + 1);
            _parentGraphic = gr;

            if (grafx != null)
            {
                grafx.Dispose();
                grafx = null;
            }
            grafx = context.Allocate(gr, new Rectangle(Point.Empty, size));
            _graphic = grafx.Graphics;
        }

        public virtual void Draw()
        {
            grafx.Render(_parentGraphic);
        }
    }
}
