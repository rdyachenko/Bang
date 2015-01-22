using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BangLogic.PlayerStates
{
    class DeadState: iPlayerState
    {

        #region iPlayerState Members

        public event OnChangeEventHandler OnChange;

        public Card InitialCard
        {
            get { return null; }
            set {}
        }

        public Player Opponent
        {
            get { return null; }
            set {}
        }

        public TurnStateStatuses State
        {
            get { return TurnStateStatuses.Closed; }
            set {}
        }

        public bool IsValidCard(Card card)
        {
            return false;
        }

        public void AddShot(BangLogic.Shots.iShot shot)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
