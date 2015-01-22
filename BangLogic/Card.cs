using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BangLogic
{
	public class Card
	{
		private eCardSuits _CardColor;
		private string _CardTitle;
		private string _CardWeight;
		private int _CardDistance;
		private eCardActions _CardType;
		private eCardRoles _CardRole;
		private int _CardID = 0;

		#region Properties

		public int CardID
		{
			get { return _CardID; }
			set { _CardID = value; }
		}
		public eCardRoles CardRole
		{
			get
			{
				return _CardRole;
			}
			set
			{
				_CardRole = value;
			}
		}
		public eCardSuits CardColor
		{
			get
			{
				return _CardColor;
			}
			set
			{
				_CardColor = value;
			}
		}
		public string CardTitle
		{
			get
			{
				return _CardTitle;
			}
			set
			{
				_CardTitle = value;
			}
		}
		public string CardValue
		{
			get
			{
				return _CardWeight;
			}
			set
			{
				_CardWeight = value;
			}
		}
		public int CardDistance
		{
			get
			{
				return _CardDistance;
			}
			set
			{
				_CardDistance = value;
			}
		}
		public eCardActions CardAction
		{
			get
			{
				return _CardType;
			}
			set
			{
				_CardType = value;
			}
		}

		#endregion Properties

		public override string ToString()
		{
			return _CardID.ToString() + " " + _CardTitle;
		}

        public Card[] ToArray()
        {
            Card[] c = new Card[0];
            c[0] = this;
            return c;
        }

        public string ToXML()
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendFormat("\r\t\t\t<card id=\"{0}\"/>", this.CardID);
            return xml.ToString();
        }
	}
}
