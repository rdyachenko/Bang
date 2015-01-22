using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BangLogic.Shots;

namespace BangLogic.PlayerStates
{
    public abstract class TurnState : ActiveState
    {
        public override bool IsValidCard(Card card)
        {
            switch(card.CardAction)
            {
                case eCardActions.Saloon:
                case eCardActions.Beer:
                    return true;
                case eCardActions.Carabine:
                case eCardActions.Gatling:
                case eCardActions.Indians:
                case eCardActions.Coach:
                case eCardActions.Jail:
                case eCardActions.Dynamite:
                case eCardActions.Cat_Balu:
                case eCardActions.Remington:
                case eCardActions.Skofild:
                case eCardActions.Shop:
                case eCardActions.Volcano:
                case eCardActions.WalshFargo:
                case eCardActions.Winchester:
                    return true;
                default:
                    return false;
            }
        }
    }
}
