using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BangLogic.Shots;

namespace BangLogic.PlayerStates
{
    public abstract class ActiveState: iPlayerState
    {
        private List<iShot> _shots = new List<iShot>();
        #region Члены iTurnState

        public event OnChangeEventHandler OnChange;

        public Card InitialCard
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Player Opponent
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public TurnStateStatuses State
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool IsValidCard(Card card)
        {
            throw new NotImplementedException();
        }

        public virtual void AddShot(iShot shot)
        {
            _shots.Add(shot);
        }

        #endregion

        public static iPlayerState SelectState(Card initialCard)
        {
            iPlayerState newState;
            switch (initialCard.CardAction)
            {
                case eCardActions.Bang:
                case eCardActions.Miss:
                    newState = new AnswerState();
                    break;
                case eCardActions.Beer:
                case eCardActions.Saloon:
                    newState = new WaitState();
                    break;
                default:
                    newState = new WaitState();
                    break;
            }
            newState.InitialCard = initialCard;
            newState.State = TurnStateStatuses.Open;
            return newState;
        }
    }
}
