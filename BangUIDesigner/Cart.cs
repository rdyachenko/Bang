using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

using Textures;

namespace BangUIDesigner
{
    public class Card : GraphicDriver
    {
        public const float CARD_HEIGTH_DIM = 1.64F;

        private int _ID;
        private bool _show;
        private Image _view;
        private Image _view_back;
        private bool _selected;
        private bool IsPictureInit;

        public int ID
        {
            get { return _ID; }
        }

        public bool Show
        {
            get { return _show; }
            set
            {
                _show = value;
                Init();
            }
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

        public bool Thumbnail
        {
            get;
            set;
        }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                Select(value);
            }
        }

        public Card(Graphics gr, int ID) : base(gr)
        {
            _ID = ID;
            Thumbnail = true;
        }

        public Card(Card cr)
        {
            _ID = cr._ID;
            _graphic = cr._graphic;
            _show = cr._show;
            this.Location = cr.Location;
            this.Size = cr.Size;
            _view = cr._view;
            _view_back = cr._view_back;
            Selected = cr.Selected;
            Thumbnail = cr.Thumbnail;
        }

        private void Init()
        {
            if (Width != 0 && Height != 0 && !IsPictureInit)
            {
                if (Thumbnail)
                {
                    _view = Picture.CartBitmap(_ID).GetThumbnailImage(Width, Height, Callback.ThumbnailCallback, IntPtr.Zero);
                    _view_back = Picture.CartBitmap(-1).GetThumbnailImage(Width, Height, Callback.ThumbnailCallback, IntPtr.Zero);
                }
                else
                {
                    _view = Picture.CartBitmap(_ID);
                    _view_back = Picture.CartBitmap(-1);
                }
                IsPictureInit = true;
            }
        }

        public void Reinit()
        {
            IsPictureInit = false;
            Init();
        }

        public override void Draw()
        {
            if (_view != null)
            {
                if (Show)
                    _graphic.DrawImage(_view, Rect);
                else
                    _graphic.DrawImage(_view_back, Rect);
            }
        }

        bool IsMe(Point p)
        {
            if ((p.X > X && p.X <= Right) && (p.Y > Y && p.Y <= Bottom))
                return true;
            return false;
        }

        public ObjectData GetObjectData(Point p)
        {
            ObjectData od = ObjectData.Empty;

            if (IsMe(p))
            {
                od.SourceObject.Type = ObjectType.Card;
                od.SourceObject.CardDetailes.CardID = _ID;
                od.SourceObject.CardDetailes.Tag = Selected ? 1 : 0;
            }
            return od;
        }

        public void Select(bool sel)
        {
            if (sel)
                Location -= new Size(0, Height / 6);
            else if (_selected)
                Location += new Size(0, Height / 6);
            _selected = sel;
        }
    }
}
