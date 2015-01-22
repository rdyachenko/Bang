using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using Textures;
using System.Windows.Forms;

namespace BangUIDesigner
{
    public class Lifes : GraphicDriver
    {
        public const float LIFES_HEIGHT_DIM = 0.32F;

        private Image _life;
        private Image _life_none;
        private bool IsInitialised;

        public int Count
        {
            get;
            set;
        }

        public int Maximum
        {
            get;
            set;
        }

        public new int Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                Init();
            }
        }

        public new int Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                Init();
            }
        }

        public Lifes(Graphics gr) : base(gr)
        {
            IsInitialised = false;
        }

        private void Init()
        {
            if (Width != 0 && Height != 0 && !IsInitialised)
            {
                _life = Picture.CartBitmap("life").GetThumbnailImage(Width, Height, Callback.ThumbnailCallback, IntPtr.Zero);
                _life_none = Picture.CartBitmap("life_none").GetThumbnailImage(Width, Height, Callback.ThumbnailCallback, IntPtr.Zero);
                IsInitialised = true;
            }
        }

        public void Reinit()
        {
            IsInitialised = false;
            Init();
        }

        public override void Draw()
        {
            if (_life != null && _life_none != null)
            {
                for (int i = 0; i < Maximum; i++)
                {
                    if (i < Count)
                        _graphic.DrawImage(_life, new Rectangle(X, Y + i*Height, Width, Height));
                    else
                        _graphic.DrawImage(_life_none, new Rectangle(X, Y + i * Height, Width, Height));
                }
            }
        }
    }
}
