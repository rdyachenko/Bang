using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public enum PlayerActionType
    {
        TakeCardAction
    }

    public enum GameActionType
    {
        InitGameTable,
        PlayerCardFromPeal,
        CardFromPeal
    }

    public enum UserEventType
    {
        ClickPeal
    }

    public class PlayerAction
    {
        public PlayerActionType Type { get; set; }
        public string Card { get; set; }
    }

    public class GameAction
    {
        public GameActionType Type { get; set; }
        public string Card { get; set; }
        public int PlayerID { get; set; }
        public int N { get; set; }
    }

    public class ClientSendDataEventArgs: EventArgs
    {
        public ClientSendDataEventArgs(string xml)
        {
            Xml = xml;
        }
        public string Xml { get; private set; }
    }

    public class ServerSendDataEventArgs: EventArgs
    {
        public ServerSendDataEventArgs(string xml, int playerID)
        {
            Xml = xml;
            PlayerID = playerID;
        }
        public string Xml { get; private set; }
        public int PlayerID { get; private set; }

    }

    public class ClientEventArgs: EventArgs
    {
        public ClientEventArgs(UserEventType eventType)
        {
            EventType = eventType;
        }
        public UserEventType EventType { get; private set; }

    }

    public interface IUILogic
    {
        event EventHandler<ClientEventArgs> UserEvent;
        /// <summary>
        /// Draw table, Peal, Clear and players
        /// </summary>
        /// <param name="tableAsXML">
        /// Table in XML format for Init
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
        /// <param name="playerID"></param>
        void AnotherPlayerGotCardFromPeal(int playerID);
    }

}
