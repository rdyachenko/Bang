using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace BangUIDesigner
{
    class FirstPlaceCard : Coordinate
    {
        static FirstPlaceCard _fpc;
        Card _card;
        Point _destPoint;
        Rectangle _tableRect;
        float _multyplicate;

        public Card Card
        {
            get { return _card; }
            set
            {
                _card = new Card(value);
                _destPoint = _card.Center;
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

        public float Multyplicate
        {
            get { return _multyplicate; }
            set
            {
                if (value <= 300 && value >= 50)
                {
                    _multyplicate = value;
                    Init();
                }
            }
        }

        private FirstPlaceCard(Rectangle rect) : base()
        {
            _tableRect = rect;
            _multyplicate = 100;
        }

        public static FirstPlaceCard GetFirstPlaceCard(Rectangle rect)
        {
            if (_fpc == null)
                _fpc = new FirstPlaceCard(rect);
            _fpc._tableRect = rect;
            _fpc.Init();
            return _fpc;
        }

        private void Init()
        {
            if (_card != null && Width > 0 && Height > 0)
            {
                _card.Width = (int)(Width * _multyplicate/100);
                _card.Height = (int)(Height * _multyplicate/100);
                _card.Center = _destPoint;

                if (_card.Right > _tableRect.Right)
                    _card.Center = new Point(_card.Center.X - _card.Right + _tableRect.Right, _card.Center.Y);

                if (_card.Left < _tableRect.Left)
                    _card.Center = new Point(_card.Center.X + _tableRect.Left - _card.Left, _card.Center.Y);

                if (_card.Bottom > _tableRect.Bottom)
                    _card.Center = new Point(_card.Center.X, _card.Center.Y - _card.Bottom + _tableRect.Bottom);

                if (_card.Top < _tableRect.Top)
                    _card.Center = new Point(_card.Center.X, _card.Center.Y + _tableRect.Top - _card.Top);

                _card.Reinit();
            }
        }

        public void Draw()
        {
            if (_card != null)
                _card.Draw();
        }

        public void Clear()
        {
            _card = null;
        }
    }
}
