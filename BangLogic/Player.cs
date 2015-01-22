using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BangLogic.PlayerStates;
using System.Net.NetworkInformation;

namespace BangLogic
{
	public class Player
	{
		#region private variables

		private List<Card> _cardsInHand = new List<Card>();
		private PlayerBoard _cardsInTable = new PlayerBoard();
		private ePlayerRoles _role = ePlayerRoles.None;
		private int _Lifes = 4;
		private Character _character = null;
		private iPlayerState _state = (iPlayerState) new WaitState() ;
        private string _ip = "";
        private string _name = "";

		#endregion

        public Player(string name, string ipAddress)
        {
            this.Name = name;
            this.IP = ipAddress;
        }

		#region properties

		public PlayerBoard CardsInTable
		{
			get { return _cardsInTable; }
			set { _cardsInTable = value; }
		}
		public string Name
		{
			get { return _name; }
            private set { _name = value; }
		}
        public string ID
        {
            get { return _character.Name.Replace(' ','_'); }
        }
        public Character Character 
		{
			get { return _character; }
			set {
				if (_character == null)
				{
					_character = value;
					_Lifes = value.Lifes;
				}

			}
		}
		public ePlayerRoles Role
		{
			get { return _role; }
			set 
			{ 
				_role = value;
			}
		}
		public int Lifes
		{
			get { return _Lifes; }
			set { _Lifes = value; }
		}
		public int LifesLimit
		{
			get {
                if (Character != null)
                {
                    if (_role == ePlayerRoles.Sherif)
                        return Character.Lifes + 1;
                    return Character.Lifes;
                }
                return 0;
			}
		}
        public string IP
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
            }
        }
        public bool Alive
        {
            get
            {
                return Lifes > 0;
            }
        }
        public iPlayerState Turn
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
        public bool isLocal()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
            return _ip.Equals(connections[0].LocalEndPoint.Address.ToString());
        }

		#endregion properties

		public void Go()
		{
            _state = (iPlayerState)new PlayerStates.Stage0();
		}


		public void PutCardsToHand(Card[] e)
		{
			_cardsInHand.AddRange(e.ToList());
		}
		
        public void RemoveCardFromHand(Card e)
		{
			_cardsInHand.Remove(e);
		}
        
        public void RemoveCardFromTable(Card e)
        {
            _cardsInTable.RemoveCard(e);
        }
		
        public Card[] ShowCardsRequest()
		{
			// request from UI for show card to me or to opponent
			return  _cardsInHand.ToArray<Card>();
		}

        public string ToXML()
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendFormat("\r\t<player id=\"{0}\" user=\"{1}\" lifes=\"{2}\" role=\"{3}\">", this._character.Name.Replace(' ', '_'), this.Name, this.Lifes.ToString(), this.Role);
            xml.Append("\r\t\t<cards count=\"" + _cardsInHand.Count + "\">");
            foreach (Card i in this._cardsInHand) { xml.Append(i.ToXML()); }
            xml.Append("\r\t\t</cards>");
            xml.Append("\r\t</player>");
            return xml.ToString();
        }

        public override string ToString()
		{
			return this._character.Name;
		}

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Player)
                return (this.Character.Name == ((Player)obj).Character.Name);
            if (obj != null && obj is string)
                return (this.Character.Name == (string)obj || this.ID == obj.ToString());
            return false;
        }

        public override int GetHashCode()
        {
            return this._character.Name.GetHashCode();
        }
	}
}
