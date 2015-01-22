using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Textures;
using System.Windows.Forms;

namespace BangUIDesigner
{
    class Pack : GraphicDriver
    {
        PackState _state;
        Image _img;
        Image _light1;
        Image _light2;
        bool _light;
        bool _IsLight;
        Timer _timer;

        public bool Light
        {
            get { return _IsLight; }
            set
            {
                if (value)
                    _timer.Start();
                else
                    _timer.Stop();

                _IsLight = value;
            }
        }

        public bool Visible
        {
            get;
            set;
        }

        public PackState State
        {
            get { return _state; }
            set
            {
                _state = value;
                Visible = true;
                switch (_state)
                {
                    case PackState.Empty:
                        Visible = false;
                    break;
                    case PackState.Low:
                        _img = Picture.CartBitmap("PodborL");
                        _light1 = Picture.CartBitmap("PodborLL");
                        _light2 = Picture.CartBitmap("PodborLL_");
                    break;
                    case PackState.Middle:
                        _img = Picture.CartBitmap("PodborM");
                        _light1 = Picture.CartBitmap("PodborLM");
                        _light2 = Picture.CartBitmap("PodborLM_");
                    break;
                    case PackState.Full:
                        _img = Picture.CartBitmap("PodborF");
                        _light1 = Picture.CartBitmap("PodborLF");
                        _light2 = Picture.CartBitmap("PodborLF_");
                    break;
                }
            }
        }

        public bool IsClear
        {
            get;
            set;
        }

        public Pack(Graphics gr) : base(gr)
        {
            _img = null;
            Visible = true;

            _timer = new Timer();
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = 200;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _light = !_light;
        }

        public override void Draw()
        {
            if (Visible && Light)
            {
                if (_light)
                    _graphic.DrawImage(_light1, Rect);
                else
                    _graphic.DrawImage(_light2, Rect);
            }

            if (Visible && _img != null)
                _graphic.DrawImage(_img, Rect);
        }

        private bool IsMe(Point p)
        {
            if ((p.X > Left && p.X <= Right) && (p.Y > Top && p.Y <= Bottom))
                return true;
            return false;
        }

        public ObjectData GetObjectData(Point p)
        {
            ObjectData od = ObjectData.Empty;

            if (IsMe(p))
            {
                if (IsClear)
                {
                    od.SourceObject.Type = ObjectType.Clear;
                    od.SourceObject.PackState = State;
                }
                else
                {
                    od.SourceObject.Type = ObjectType.Peal;
                    od.SourceObject.PackState = State;
                }
            }
            return od;
        }
    }
}
