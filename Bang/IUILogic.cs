using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bang_
{
    public enum ObjectType
    {
        Card,
        Clear,
        None,
        Peal, 
        Player, 
        Table
    }

    public interface IUILogic
    {
        /// <summary>
        /// Draw table, Peal, Clear and players
        /// </summary>
        /// <param name="tableAsXML">
        /// Table in XML format for Init
        /// <players count="2">
        ///    <player id="Bart_Cassidy" user="Tolik" lifes="5" role="Sherif">
        ///         <cards count="5">
        ///             <card id="33" />
        ///             <card id="2" />
        ///             <card id="70" />
        ///             <card id="28" />
        ///             <card id="16" />
        ///         </cards>
        ///    </player>
        ///    <player id="El_Gringo" user="Serg" lifes="3" role="Outlaw">
        ///         <cards count="3">
        ///             <card id="11" />
        ///             <card id="7" />
        ///             <card id="43" />
        ///             <card id="17" />
        ///         </cards>
        ///    </player>
        /// </players>
        /// </param>
        void Init(string tableAsXML);

        /// <summary>
        /// Move card from Peal to own hand and show it
        /// </summary>
        /// <param name="cardID">Card ID</param>
        void GotCardFromPeal(int cardID);

        /// <summary>
        /// Drive card face down from peal to selected player 
        /// </summary>
        /// <param name="playerID">Happy player id</param>
        void AnotherPlayerGotCardFromPeal(int playerID);

        /// <summary>
        /// Light table around current player
        /// </summary>
        /// <param name="playerID"></param>
        void SetCurrentPlayer(int playerID);
    }
}
