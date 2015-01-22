using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace BangUIDesigner
{
    class MessageG : GraphicDriver
    {
        string _text;
        Font _font;
        Brush _brush;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }

        public Brush Brush
        {
            get { return _brush; }
            set { _brush = value; }
        }

        public MessageG(Graphics gr) : base(gr)
        {
            _text = "";
            _font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular);
            _brush = new SolidBrush(Color.Black);
        }

        public override void Draw()
        {
            _graphic.DrawString(_text, _font, _brush, Rect);
        }
    }
}
