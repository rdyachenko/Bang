using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using Textures;
using System.Windows.Forms;

namespace BangUIDesigner
{
    public class Player : GraphicDriver
    {
        SortedList<int, Card> _cards;
        SortedList<int, Card> _cardsOnMyTable;
        Card _role;
        Roles _roleID;
        Rectangle _rcPic;
        MessageG _msg;
        Lifes _lifes;
        int _cardSize;
        int _cardOnTableSize;
        string _character;
        Image _Pic;
        Image _light1;
        Image _light2;
        Timer timer;
        Font _textFont;
        bool _light;
        bool _IsLight;
        bool IsPictureInit;

        public int CardSize
        {
            get { return _cardSize; }
        }

        public Card Role
        {
            get { return _role; }
        }

        public SortedList<int, Card> Cards
        {
            get { return _cards; }
        }

        public SortedList<int, Card> CardsOnMyTable
        {
            get { return _cardsOnMyTable; }
        }

        public int MaxLifes
        {
            get { return _lifes.Maximum; }
            set { _lifes.Maximum = value; }
        }

        public int LifesCount
        {
            get { return _lifes.Count; }
        }

        public bool Light
        {
            get { return _IsLight; }
            set
            {
                if (value)
                    timer.Start();
                else
                    timer.Stop();

                _IsLight = value;
            }
        }

        public SortedList<int, Card> SelectedCard
        {
            get
            {
                SortedList<int, Card> selCards = new SortedList<int, Card>();
                foreach (Card cr in _cards.Values)
                    if (cr.Selected)
                        selCards.Add(cr.ID, cr);
                return selCards;
            }
        }

        public bool ShowRole
        {
            get { return _role.Show; }
            set { _role.Show = value; }
        }

        public Roles RoleID
        {
            get { return _roleID; }
            set
            {
                _roleID = value;
                _role = new Card(_graphic, (int)_roleID);
                _role.Location = Location + new Size(0, _rcPic.Height / 6);
                _role.Width = GetValidSize() / 6;
                _role.Height = (int)((GetValidSize() / 6) * Card.CARD_HEIGTH_DIM);
            }
        }

        public new Point Location
        {
            get { return base.Location; }
            set
            {
                base.Location = value;
                InitInnerObjectPlace();
            }
        }

        public new Point Center
        {
            get { return base.Center; }
            set
            {
                base.Center = value;
                InitInnerObjectPlace();
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

        private void InitInnerObjectPlace()
        {
            _rcPic.Location = Location + new Size(GetValidSize() / 2 - _rcPic.Width / 2, 0);
            if (_role != null)
                _role.Location = Location + new Size(0, _rcPic.Height / 6);
            _lifes.Location = Location + new Size(5 * _role.Width / 4, _rcPic.Height / 6);

            SetCardPossitions();
        }

        public Player(Graphics gr, string character) : base(gr)
        {
            _cards = new SortedList<int, Card>();
            _cardsOnMyTable = new SortedList<int, Card>();
            _character = character;
            _textFont = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular);
            
            _lifes = new Lifes(gr);
            _msg = new MessageG(gr);

            IsPictureInit = false;

            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 200;
            timer.Stop();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            _light = !_light;
        }

        private void Init()
        {
            if (Width != 0 && Height != 0)
            {
                int valSize = GetValidSize();
                _cardSize = valSize / 3;
                _cardOnTableSize = valSize / 5;

                _rcPic.Width = valSize / 2;
                _rcPic.Height = _rcPic.Width;

                _lifes.Width = valSize / 8;
                _lifes.Height = (int)(_lifes.Width * Lifes.LIFES_HEIGHT_DIM);

                InitBitmaps();

                if (_role != null)
                {
                    _role.Width = valSize / 6;
                    _role.Height = (int)((valSize / 6) * Card.CARD_HEIGTH_DIM);
                }

                foreach (Card cr in _cards.Values)
                {
                    cr.Width = _cardSize;
                    cr.Height = (int)(_cardSize * Card.CARD_HEIGTH_DIM);
                }
            }
        }

        private void InitBitmaps()
        {
            if (!IsPictureInit)
            {
                _light1 = Picture.CartBitmap("light").GetThumbnailImage(_rcPic.Width, _rcPic.Height, Callback.ThumbnailCallback, IntPtr.Zero);
                _light2 = Picture.CartBitmap("light_").GetThumbnailImage(_rcPic.Width, _rcPic.Height, Callback.ThumbnailCallback, IntPtr.Zero);
                _Pic = Picture.CartBitmap(_character).GetThumbnailImage(_rcPic.Width, _rcPic.Height, Callback.ThumbnailCallback, IntPtr.Zero);
                IsPictureInit = true;
            }
        }

        public void ReInit()
        {
            IsPictureInit = false;
            InitBitmaps();
            foreach (Card cr in _cards.Values)
            {
                cr.Reinit();
            }

            foreach (Card cr in _cardsOnMyTable.Values)
            {
                cr.Reinit();
            }

            _role.Reinit();
            _lifes.Reinit();
        }

        private int GetValidSize()
        {
            int validSize = 0;
            if (Width > Height)
                validSize = Height;
            else
                validSize = Width;
            return validSize;
        }

        public override void Draw()
        {
            //_graphic.DrawRectangle(new Pen(Color.Red), Rect);
            if (Light)
            {
                if (_light)
                    //_graphic.FillRectangle(new SolidBrush(Color.Yellow), _plRect);
                    _graphic.DrawImage(_light1, _rcPic);
                else
                    _graphic.DrawImage(_light2, _rcPic);
            }

            _msg.Draw();

            if (_Pic != null)
                _graphic.DrawImage(_Pic, _rcPic);

            if (_role != null)
                _role.Draw();

            foreach (Card cart in _cards.Values)
                cart.Draw();

            foreach (Card cart in _cardsOnMyTable.Values)
                cart.Draw();

            _lifes.Draw();
        }

        public void SetLifeCount(int count)
        {
            if (count >= 0 && count <= _lifes.Maximum)
                _lifes.Count = count;
        }

        public void AddCarts(int[] IDs)
        {
            try
            {
                foreach (int ID in IDs)
                {
                    Card cr = new Card(_graphic, ID);
                    cr.Width = _cardSize;
                    cr.Height = (int)(_cardSize * Card.CARD_HEIGTH_DIM);
                    _cards.Add(ID, cr);
                }
                SetCardPossitions();
            }
            catch (Exception) { }
        }

        public void ShowCards(bool show)
        {
            foreach (Card card in _cards.Values)
            {
                card.Show = show;
            }
        }

        private int GetDX()
        {
            int retDX = 0;
            if (_cards.Count - 1 != 0)
                retDX = (Width - _cardSize) / (_cards.Count - 1);
            if (retDX > _cardSize)
                retDX = _cardSize;
            return retDX;
        }

        private int GetDY()
        {
            int retDY = 0;
            int validSize = GetValidSize();
            if (_cardsOnMyTable.Count * _cardOnTableSize > validSize)
                retDY = (validSize - (int)(_cardOnTableSize * Card.CARD_HEIGTH_DIM)) / (_cardsOnMyTable.Count == 1 ? 1 : (_cardsOnMyTable.Count - 1));
            else
                retDY = _cardOnTableSize / 2;
            return retDY;
        }

        private Point GetCardPlace()
        {
            return new Point(X, (int)(Bottom - _cardSize * Card.CARD_HEIGTH_DIM));
        }

        private void SetCardPossitions()
        {
            bool IsSelect;
            Point cardPlase = GetCardPlace();
            int DX = GetDX();
            int DY = GetDY();
            foreach (Card card in _cards.Values)
            {
                IsSelect = card.Selected;
                card.Selected = false;
                card.Location = cardPlase;
                cardPlase.X += DX;
                card.Selected = IsSelect;
            }

            Point drPn = new Point(Right - _cardOnTableSize, Location.Y);

            foreach (Card card in _cardsOnMyTable.Values)
            {
                card.Location = drPn;
                drPn.Y += DY;
            }
        }

        bool IsMe(Point p)
        {
            if ((p.X > _rcPic.Left && p.Y > _rcPic.Top) && (p.X <= _rcPic.Right && p.Y <= _rcPic.Bottom))
                return true;
            return false;
        }

        bool IsMyCards(Point p)
        {
            if ((p.X > Left && p.Y > Top) && (p.X <= Right && p.Y <= Bottom))
                return true;
            return false;
        }

        public ObjectData GetObjectData(Point p)
        {
            ObjectData od = ObjectData.Empty;

            if (IsMyCards(p))
            {
                od = _role.GetObjectData(p);
                if (od == ObjectData.Empty)
                {
                    for (int i = _cards.Count - 1; i != -1; i--)
                    {
                        od = _cards.Values[i].GetObjectData(p);
                        if (od != ObjectData.Empty)
                        {
                            od.SourceObject.CardDetailes.CardPlace = CardPlace.Hand;
                            break;
                        }
                    }

                    if (od == ObjectData.Empty)
                    {
                        for (int i = _cardsOnMyTable.Count - 1; i != -1; i--)
                        {
                            od = _cardsOnMyTable.Values[i].GetObjectData(p);
                            if (od != ObjectData.Empty)
                            {
                                od.SourceObject.CardDetailes.CardPlace = CardPlace.PlayerTable;
                                break;
                            }
                        }

                        if (od == ObjectData.Empty && IsMe(p))
                            od.SourceObject.Type = ObjectType.Player;
                    }
                }
                od.SourceObject.PlayerDetailes.PlayerID = _character;
                od.SourceObject.PlayerDetailes.RoleID = _roleID;
                od.SourceObject.PlayerDetailes.Tag = _lifes.Count;
            }
            return od;
        }

        public void DoAction(BangActions act, ObjectData data)
        {
            switch (act)
            {
                case BangActions.MoveToMyTable:
                    if(data.DestinationObject.CardDetailes.CardPlace == CardPlace.Hand)
                    {
                        Card cr = this.RemoveCard(data.DestinationObject.CardDetailes.CardID);
                        cr.Width = _cardOnTableSize;
                        cr.Height = (int)(_cardOnTableSize * Card.CARD_HEIGTH_DIM);
                        cr.Select(false);
                        cr.Show = true;
                        _cardsOnMyTable.Add(cr.ID, cr);
                        SetCardPossitions();
                    }
                    break;
                case BangActions.SelectCard:
                    if (data.DestinationObject.CardDetailes.CardPlace == CardPlace.Hand)
                        _cards[data.DestinationObject.CardDetailes.CardID].Select(!(_cards[data.DestinationObject.CardDetailes.CardID].Selected));
//                     if (data.CardPlace == CardPlace.PlayerTable)
//                         _cardsOnMyTable[data.ID].Select(!(_cardsOnMyTable[data.ID].Selected));
                    break;
            }
        }

        public Card RemoveCard(int ID)
        {
            Card cr = new Card(_cards[ID]);
            _cards.Remove(ID);
            return cr;
        }
    }
}
