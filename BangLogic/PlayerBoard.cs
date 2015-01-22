using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BangLogic
{
	public class PlayerBoard
	{
		#region Cards in table
			private Card _barrel = null;
			private Card _gun = null;
			private Card _mustang = null;
			private Card _jail = null;
			private Card _dynamite = null;
			private Card _scope = null;
		#endregion

		public bool BarrelExist
		{
			get { return (_barrel != null); }
		}
		public bool JailExist
		{
			get { return (_jail != null); }
		}		
		public bool MustangExist
		{
			get { return (_mustang != null); }
		}
		public bool ScopeExist
		{
			get { return (_scope != null); }
		}
		public bool DinamiteExist
		{
			get { return (_dynamite != null); }
		}
		public int GunDistance
		{
			get 
			{
				if (_gun != null)
					return _gun.CardDistance;
				return 1;
			}
		}
		public eCardActions GunName
		{
			get {
				if (_gun != null)	
					return _gun.CardAction;
				return eCardActions.none;
			}
		}

		public List<Card> ToArray
		{
			get {
				List<Card> cards = new List<Card>();
				if (_barrel != null) cards.Add(_barrel);
				if (_gun != null) cards.Add(_gun);
				if (_mustang != null) cards.Add(_mustang);
				if (_jail != null) cards.Add(_jail);
				if (_dynamite != null) cards.Add(_dynamite);
				if (_scope != null) cards.Add(_scope);
				return cards;
			}
		}

		public Card PutCard(Card card)
		{
            Card forReturn = null;
			switch (card.CardAction)
			{
				case eCardActions.Barrel:
					_barrel = card;
					break;
				case eCardActions.Dynamite:
					_dynamite = card;
					break;
				case eCardActions.Mustang:
					_mustang = card;
					break;
				case eCardActions.Scope:
					_scope = card;
					break;
				case eCardActions.Jail:
					_jail = card;
					break;
				case eCardActions.Carabine:
				case eCardActions.Winchester:
				case eCardActions.Volcano:
				case eCardActions.Skofild:
				case eCardActions.Remington:
                    if (_gun != null)
                        forReturn = _gun;
					_gun = card;
					break;
			}
            return forReturn;
		}
        public void RemoveCard(Card card)
        {
            switch (card.CardAction)
            {
                case eCardActions.Barrel:
                    _barrel = null;
                    break;
                case eCardActions.Dynamite:
                    _dynamite = null;
                    break;
                case eCardActions.Mustang:
                    _mustang = null;
                    break;
                case eCardActions.Scope:
                    _scope = null;
                    break;
                case eCardActions.Jail:
                    _jail = null;
                    break;
                case eCardActions.Carabine:
                case eCardActions.Winchester:
                case eCardActions.Volcano:
                case eCardActions.Skofild:
                case eCardActions.Remington:
                    _gun = null;
                    break;
            }
        }		
	}
}
