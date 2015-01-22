using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using Textures;
using System.Windows.Forms;
using System.Diagnostics;

namespace BangUIDesigner
{
    public class Table : GraphicDriver
    {
        SortedList<string, Player> _players;
        List<string> _playerIDs;
        List<Card> _cardsOnTable;
        FirstPlaceCard _fpc;
        Pack _clear;
        Pack _peal;
        List<Card> _actionCards;
        int _playerSize;
        int _floorDX;
        int _floorDY;
        Timer _timerLight;
        Rectangle _tlbLightRect;
        Image _img;
        Image _floor;
        Image _light1;
        Image _light2;
        bool _light;
        bool _Islight;
        bool _IsInit;
        bool IsInitialised;

        public bool Light
        {
            get { return _Islight; }
            set
            {
                if (value)
                    _timerLight.Start();
                else
                    _timerLight.Stop();

                _Islight = value;
            }
        }

        public SortedList<string, Player> Players
        {
            get { return _players; }
        }

        public int PlayerCount
        {
            get
            {
                return _players.Count;
            }
        }

        public Table(Graphics gr) : base(gr)
        {
            _players = new SortedList<string, Player>();
            _playerIDs = new List<string>();
            _cardsOnTable = new List<Card>();
            _actionCards = new List<Card>();

            _timerLight = new Timer();
            _timerLight.Tick += new EventHandler(_timerLight_Tick);
            _timerLight.Interval = 200;

            _IsInit = false;
            IsInitialised = false;
        }

        void _timerLight_Tick(object sender, EventArgs e)
        {
            _light = !_light;
        }

        public void Init(Size tbSize)
        {
            Size = tbSize;
            _playerSize = Width / 5;
            Size clSize = new Size(Width / 10, Width / 10);
            _clear = new Pack(_graphic);
            _clear.Size = clSize;
            _clear.IsClear = true;
            _peal = new Pack(_graphic);
            _peal.Size = clSize;
            _tlbLightRect = new Rectangle(Width / 6, Height / 6, 2*Width / 3, 2*Height / 3);
            _fpc = FirstPlaceCard.GetFirstPlaceCard(Rect);
            InitBitmaps();

            _floorDX = Width / _floor.Width;
            _floorDY = Height / _floor.Height;

            foreach (Player pl in _players.Values)
            {
                pl.Width = _playerSize;
                pl.Height = _playerSize;
            }

            _IsInit = true;
        }

        private void InitBitmaps()
        {
            if(!IsInitialised)
            {
                if (_img == null)
                    _img = Picture.CartBitmap("Table").GetThumbnailImage(_tlbLightRect.Width, _tlbLightRect.Height, Callback.ThumbnailCallback, IntPtr.Zero);
                if (_light1 == null)
                    _light1 = Picture.CartBitmap("TableL").GetThumbnailImage(_tlbLightRect.Width, _tlbLightRect.Height, Callback.ThumbnailCallback, IntPtr.Zero);
                if (_light2 == null)
                    _light2 = Picture.CartBitmap("TableL_").GetThumbnailImage(_tlbLightRect.Width, _tlbLightRect.Height, Callback.ThumbnailCallback, IntPtr.Zero);
                if (_floor == null)
                    _floor = Picture.CartBitmap("floor");
                IsInitialised = true;
            }
        }

        public void Reinit()
        {
            IsInitialised = false;
            InitBitmaps();

            foreach(Player pl in _players.Values)
            {
                pl.ReInit();
            }

            foreach (Card cr in _cardsOnTable)
            {
                cr.Reinit();
            }
            _clear.State = _clear.State;
            _peal.State = _peal.State;
        }

        public void ChangeSize(Size newSize)
        {
            if (!_IsInit)
                return;

            Size = newSize;
            _playerSize = Width / 5;
            Size clSize = new Size(Width / 10, Width / 10);
            _clear.Size = clSize;
            _peal.Size = clSize;
            _tlbLightRect = new Rectangle(Width / 6, Height / 6, 2 * Width / 3, 2 * Height / 3);
            _fpc = FirstPlaceCard.GetFirstPlaceCard(Rect);

            foreach (Player pl in _players.Values)
            {
                pl.Width = _playerSize;
                pl.Height = _playerSize;
            }

            foreach (Card cr in _cardsOnTable)
            {
                if (_players.Values[0] != null)
                {
                    cr.Center = GetRandPoint(Center, 100);
                    cr.Width = _players.Values[0].CardSize;
                    cr.Height = (int)(_players.Values[0].CardSize * Card.CARD_HEIGTH_DIM);
                }
            }

            if (PlayerCount > 0)
                CalculatePlace();
        }

        public void AddPlayer(int[] carts, string Character, Roles role, int LifeCount)
        {
            try
            {
                Player pl = new Player(_graphic, Character);
                _playerIDs.Add(Character);
                pl.Width = _playerSize;
                pl.Height = _playerSize;
                pl.RoleID = role;
                pl.AddCarts(carts);
                pl.MaxLifes = LifeCount;
                pl.SetLifeCount(LifeCount);
                if (role == Roles.Sherif)
                    pl.ShowRole = true;

                _players.Add(Character, pl);
            }
            catch (Exception){}
        }

        public void CalculatePlace()
        {
            int X, Y;
            double coner, NextConer = 0;
            coner = (Math.PI * (360 / PlayerCount)) / 180;

            foreach (string pl in _playerIDs)
            {
                X = (int)(Width / 2 + Width / 2.6 * Math.Sin(NextConer));
                Y = (int)(Height / 2 + Height / 3 * Math.Cos(NextConer));

                _players[pl].Center = new Point(X, Y);
                if (NextConer == 0)
                    _players[pl].ShowCards(true);
                NextConer += coner;
            }
            _clear.State = PackState.Low;
            _peal.State = PackState.Full;
            _clear.X = Width - _clear.Width;
            _clear.Y = Height - _clear.Height;
            _peal.X = Width - _peal.Width;
            _peal.Y = 0;
        }

        bool IsMe(Point p)
        {
            if ((p.X <= _tlbLightRect.Right && p.Y <= _tlbLightRect.Bottom) && (p.X >= _tlbLightRect.Left && p.Y >= _tlbLightRect.Top))
                return true;
            return false;
        }

        public ObjectData GetObjectData(Point p)
        {
            ObjectData od = ObjectData.Empty;

            if (!_IsInit)
                return od;

            od = _clear.GetObjectData(p);
            if (od == ObjectData.Empty)
            {
                od = _peal.GetObjectData(p);
                if (od == ObjectData.Empty)
                {
                    for (int i = _cardsOnTable.Count - 1; i != -1; i--)
                    {
                        od = _cardsOnTable[i].GetObjectData(p);
                        if (od != ObjectData.Empty)
                        {
                            od.SourceObject.CardDetailes.CardPlace = CardPlace.Table;
                            break;
                        }
                    }

                    if (od == ObjectData.Empty)
                    {
                        foreach (Player pl in _players.Values)
                        {
                            od = pl.GetObjectData(p);
                            if (od != ObjectData.Empty)
                                break;
                        }

                        if (od == ObjectData.Empty && IsMe(p))
                        {
                            od.SourceObject.Type = ObjectType.Table;
                            od.SourceObject.CardDetailes.CardID = -1;
                        }
                    }
                }
            }
            return od;
        }

        private Point GetRandPoint(Point pt, int Dim)
        {
            Random rnd = new Random();
            return new Point(rnd.Next(pt.X - Dim, pt.X + Dim), rnd.Next(pt.Y - Dim, pt.Y + Dim));
        }

        public void DoAction(BangActions act, ObjectData data)
        {
            switch (act)
            {
                case BangActions.MoveToPlayer:
                    MoveToPlayer(data);
                    break;
                case BangActions.TableCartToClear:
                    TableCardToClear();
                    break;
                case BangActions.MoveToTable:
                    MoveToTable(data);
                    break;
                case BangActions.Lifes:
                    _players[data.DestinationObject.PlayerDetailes.PlayerID].SetLifeCount(data.DestinationObject.PlayerDetailes.Tag);
                    break;
                case BangActions.SetPackState:
                    if (data.DestinationObject.Type == ObjectType.Clear)
                        _clear.State = data.DestinationObject.PackState;
                    if (data.DestinationObject.Type == ObjectType.Peal)
                        _peal.State = data.DestinationObject.PackState;
                    break;
                case BangActions.MoveToClear:
                    MoveToClear(data);
                    break;
                case BangActions.Ligth:
                    Ligth(data);
                    break;
                case BangActions.HideLigth:
                    HideLigth(data);
                    break;
                case BangActions.Multiplicate:
                    _fpc.Multyplicate = data.DestinationObject.Tag;
                    break;
                case BangActions.AddCard:
                    AddCard(data);
                    break;
                case BangActions.ShowCard:
                    ShowCard(data);
                    break;
                default:
                    if (data.DestinationObject.CardDetailes.CardPlace != CardPlace.Table)
                        _players[data.DestinationObject.PlayerDetailes.PlayerID].DoAction(act, data);
                    break;
            }
        }

        private void TableCardToClear()
        {
            List<Point> pts = new List<Point>();

            foreach (Card cr in _cardsOnTable)
            {
                pts.Add(cr.Center);
            }

            _cardsOnTable.Clear();
            MoveFromTo(pts.ToArray(), _clear.Center);
        }

        private void MoveToTable(ObjectData data)
        {
            Card card = _players[data.DestinationObject.PlayerDetailes.PlayerID].RemoveCard(data.DestinationObject.CardDetailes.CardID);
            Point dest = GetRandPoint(Center, 50);
            MoveFromTo(card.Center, dest);
            card.Center = dest;
            card.Show = true;
            _cardsOnTable.Add(card);
        }

        private void MoveToClear(ObjectData data)
        {
            SortedList<int, Card> selCards = _players[data.DestinationObject.PlayerDetailes.PlayerID].SelectedCard;
            List<Point> pts = new List<Point>();

            foreach (Card cr in selCards.Values)
            {
                pts.Add(cr.Center);
                _players[data.DestinationObject.PlayerDetailes.PlayerID].RemoveCard(cr.ID);
            }

            MoveFromTo(pts.ToArray(), _clear.Center);
        }

        private void Ligth(ObjectData data)
        {
            switch (data.DestinationObject.Type)
            {
                case ObjectType.Table:
                    Light = true;
                    break;
                case ObjectType.Player:
                    _players[data.DestinationObject.PlayerDetailes.PlayerID].Light = true;
                    break;
                case ObjectType.Peal:
                    _peal.Light = true;
                    break;
                case ObjectType.Clear:
                    _clear.Light = true;
                    break;
            }
        }

        private void HideLigth(ObjectData data)
        {
            switch (data.DestinationObject.Type)
            {
                case ObjectType.Table:
                    Light = false;
                    break;
                case ObjectType.Player:
                    _players[data.DestinationObject.PlayerDetailes.PlayerID].Light = false;
                    break;
                case ObjectType.Peal:
                    _peal.Light = false;
                    break;
                case ObjectType.Clear:
                    _clear.Light = false;
                    break;
            }
        }

        private void AddCard(ObjectData data)
        {
            Point src = Point.Empty;
            Player pl = _players[data.DestinationObject.PlayerDetailes.PlayerID];

            switch (data.SourceObject.Type)
            {
            case ObjectType.Peal:
                src = _peal.Center;
                break;
            case ObjectType.Card:
                Card cr = _players[data.SourceObject.PlayerDetailes.PlayerID].RemoveCard(data.DestinationObject.CardDetailes.CardID);
                src = cr.Center;
                break;
            }

            MoveFromTo(src, pl.Center);
            pl.AddCarts(new int[] { data.DestinationObject.CardDetailes.CardID });
            pl.ShowCards(true);
        }

        private void ShowCard(ObjectData data)
        {
            if (data.DestinationObject.Tag != 0)
            {
                switch (data.DestinationObject.CardDetailes.CardPlace)
                {
                    case CardPlace.Table:
                        foreach (Card cr in _cardsOnTable)
                            if (data.DestinationObject.CardDetailes.CardID == cr.ID)
                                _fpc.Card = cr;
                        break;
                    case CardPlace.Hand:
                        _fpc.Card = _players[data.DestinationObject.PlayerDetailes.PlayerID].Cards[data.SourceObject.CardDetailes.CardID];
                        break;
                    case CardPlace.Role:
                        _fpc.Card = _players[data.DestinationObject.PlayerDetailes.PlayerID].Role;
                        break;
                    case CardPlace.PlayerTable:
                        _fpc.Card = _players[data.DestinationObject.PlayerDetailes.PlayerID].CardsOnMyTable[data.SourceObject.CardDetailes.CardID];
                        break;
                }

                _fpc.Width = Width / 10;
                _fpc.Height = (int)(_fpc.Width * Card.CARD_HEIGTH_DIM);
            }
            else
                _fpc.Clear();
        }

        private void MoveToPlayer(ObjectData data)
        {
            Card card = _players[data.SourceObject.PlayerDetailes.PlayerID].RemoveCard(data.SourceObject.CardDetailes.CardID);
            Player player = _players[data.DestinationObject.PlayerDetailes.PlayerID];
            Point dest = GetRandPoint(new Point((Center.X + player.Center.X) / 2, (Center.Y + player.Center.Y) / 2), 10);
            MoveFromTo(card.Center, dest);
            card.Center = dest;
            card.Show = true;
            _cardsOnTable.Add(card);
        }

        private void MoveFromTo(Point source, Point dest)
        {
            MoveFromTo( new Point[]{source}, dest);
        }

        private void MoveFromTo(Point[] source, Point dest)
        {
            try
            {
                float dx = 0, dy = 0, step = 15;
                for (int i = 0; i < source.Length; i++)
                {
                    Card cr = new Card(_graphic, -1);

                    cr.Width = _playerSize / 3;
                    cr.Height = (int)(cr.Width * Card.CARD_HEIGTH_DIM);
                    cr.Center = source[i];

                    if (i == 0)
                    {
                        dx = (dest.X - cr.Center.X) / step;
                        dy = (dest.Y - cr.Center.Y) / step;
                    }

                    _actionCards.Add(cr);
                }

                for (int i = 0; i < step; i++ )
                {
                    foreach (Card cr in _actionCards)
                    {
                        cr.Center = new Point((int)(cr.Center.X + dx), (int)(cr.Center.Y + dy));
                    }
                    Wait(20);
                }
                _actionCards.Clear();
            }
            catch (Exception) { }
        }

        private void Wait(int value)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
                Application.DoEvents();
        }

        public override void Draw()
        {
            if (!_IsInit)
                return;

            _graphic.FillRectangle(new SolidBrush(Color.DarkGreen), Rect);

//             for (int i = 0; i < _floorDX+1; i++ )
//             {
//                 for (int j = 0; j < _floorDY+1; j++ )
//                 {
//                     _graphic.DrawImage(_floor, new Rectangle(i * _floor.Width, j * _floor.Height, _floor.Width, _floor.Height));
//                 }
//             }

            if (Light)
            {
                if (_light)
                    _graphic.DrawImage(_light1, _tlbLightRect);
                else
                    _graphic.DrawImage(_light2, _tlbLightRect);
            }
            _graphic.DrawImage(_img, _tlbLightRect);

            foreach (Player pl in _players.Values)
                pl.Draw();

            foreach (Card cr in _cardsOnTable)
                cr.Draw();

            foreach (Card cr in _actionCards)
                cr.Draw();

            _clear.Draw();
            _peal.Draw();

            if (_fpc != null)
                _fpc.Draw();
        }

        public void Clear()
        {
            _players.Clear();
            _playerIDs.Clear();
            _cardsOnTable.Clear();
        }
    }
}
