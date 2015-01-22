using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BangLogic.Shots;

namespace BangLogic.PlayerStates
{
    public enum TurnStateStatuses
    {
        Open,
        Closed
    }
    
    public delegate bool OnChangeEventHandler(iPlayerState nextState);

    public interface iPlayerState
    {
        
        event OnChangeEventHandler OnChange;

        Card InitialCard
        {
            get;
            set;
        }

        Player Opponent
        {
            get;
            set;
        }

        TurnStateStatuses State
        {
            get;
            set;
        }

        bool IsValidCard(Card card);
        void AddShot(iShot shot);
    }
}
