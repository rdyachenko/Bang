using System;
using System.Collections.Generic;
using System.Text;

namespace BangLogic
{
    public class Turn
    {
        private Player CurPlayer = null;
        private bool BungLimit = true;
        private int BungCounter = 0;
        private int LifesLimit = 0;
        private Players AllPlayers = null;

        public Player CurrentPlayer
        {
            get { return CurPlayer; }
            set 
            {
                CurPlayer = value;
                BungLimit = true;
                BungCounter = 0;
                LifesLimit = CurPlayer.Character.Lifes;
                if (CurPlayer.Role == ePlayerRoles.Sherif) LifesLimit++;
                if (value.Character.Ability == eAbilities.NoBungLimit)
                    BungLimit = false;
                if (value.CardsInTable.GunName == eCardActions.Volcano)
                    BungLimit = false;
            }
        }

        public Players Players
        {
            get { return AllPlayers; }
            set { AllPlayers = value; }
        }

        public bool isValidCardForMove(Card card)
        {
            bool isValid = false;
            switch (card.CardAction)
            {
                case eCardActions.Bang:
                    if (!(BungLimit && BungCounter > 0))
						if (AllPlayers.OpponentsWithDistance(CurPlayer,CurPlayer.CardsInTable.GunDistance) > 0)
                        isValid = true;
                    break;
                case eCardActions.Miss:
                    if (CurPlayer.Character.Ability == eAbilities.BangVSMiss)
                        isValid = true;
                    break;
                case eCardActions.Beer:
                    if (CurPlayer.Lifes < LifesLimit)
                        isValid = true;
                    break;
                case eCardActions.Saloon:
				case eCardActions.Carabine:
				case eCardActions.Remington:
				case eCardActions.Skofild:
				case eCardActions.Volcano:
				case eCardActions.Winchester:
				case eCardActions.Shop:
				case eCardActions.Indians:
				case eCardActions.Gatling:
				case eCardActions.Jail:
				case eCardActions.Duel:
				case eCardActions.Cat_Balu:
				case eCardActions.Coach:
				case eCardActions.WalshFargo:
				case eCardActions.Dynamite:
                    isValid = true;
                    break;
				case eCardActions.Panic:
					if (AllPlayers.OpponentsWithDistance(CurPlayer,1) > 0)
                        isValid = true;
                    break;
				case eCardActions.Barrel:
                    if (! CurPlayer.CardsInTable.BarrelExist)
                        isValid = true;
                    break;
            }
            return isValid;
        }

        public bool isValidCardForAnswer(Player opponent, Card card, Card attacCard)
        {
            bool isValid = false;
            switch (attacCard.CardAction)
            {
                case eCardActions.Bang:
                case eCardActions.Miss:
                case eCardActions.Gatling:
                    isValid = card.CardAction == eCardActions.Miss;
                    if (opponent.Character.Ability == eAbilities.BangVSMiss && card.CardAction == eCardActions.Bang)
                        isValid = true;
                    break;
                case eCardActions.Duel:
                case eCardActions.Indians:
                    isValid = (card.CardAction == eCardActions.Bang);
                    if (CurPlayer.Character.Ability == eAbilities.BangVSMiss && card.CardAction == eCardActions.Miss)
						isValid = true;
                    break;
            }
            return isValid;
        }
    }
}
