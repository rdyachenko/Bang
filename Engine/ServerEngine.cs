using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using System.Xml.Serialization;

namespace Engine
{
    public class ServerEngine
    {
        private int _numPlayers;

        private List<string>[] _playersCards;
        private List<string> _pealCards;

        private int _pealNowCard;

        #region IServerEngine implementation

        public event EventHandler<ServerSendDataEventArgs> SendData;

        public void InitGame(int playersNumber)
        {
            _numPlayers = playersNumber;
            _playersCards = new List<string>[playersNumber];
            for (int i = 0; i < _numPlayers; i++)
            {
                _playersCards[i] = new List<string>();
            }
            _pealCards = new List<string>();
            _pealCards.Add("1");
            _pealCards.Add("2");
            _pealCards.Add("3");
            _pealCards.Add("4");
            _pealCards.Add("5");
            _pealCards.Add("6");
            _pealCards.Add("7");
            _pealCards.Add("8");
            _pealCards.Add("9");
            _pealCards.Add("10");


            for (int i = 0; i < _numPlayers; i++)
            {
                GameAction action = new GameAction();

                action.Type = GameActionType.InitGameTable;
                action.N = _numPlayers;

                sendAction(i, action);
            }
        }

        public void ReceiveData(int playerID, string xmlAction)
        {
            if (playerID > _numPlayers)
                throw new ArgumentOutOfRangeException();

            XmlSerializer xs = new XmlSerializer(typeof(PlayerAction));
            TextReader tr = new StringReader(xmlAction);
            PlayerAction action = (PlayerAction)xs.Deserialize(tr);

            tr.Close();

            switch (action.Type)
            {
                case PlayerActionType.TakeCardAction:
                    performTakeCardAction(playerID);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        #endregion

        private void performTakeCardAction(int playerID)
        {
            string cardID = _pealCards[_pealNowCard++ % 10];
            _playersCards[playerID].Add(cardID);

            for (int i = 0;i < _numPlayers;i++)
            {
                GameAction action = new GameAction();

                if (i == playerID)
                {
                    action.Type = GameActionType.CardFromPeal;
                    action.Card = cardID;
                }
                else
                {
                    action.Type = GameActionType.PlayerCardFromPeal;
                    action.PlayerID = playerID;
                }

                sendAction(playerID, action);
            }
        }

        private void sendAction(int playerID, GameAction action)
        {
            XmlSerializer xs = new XmlSerializer(typeof(GameAction));
            MemoryStream ms = new MemoryStream();
            xs.Serialize(ms, action);

            ServerSendDataEventArgs args = new ServerSendDataEventArgs(Encoding.UTF8.GetString(ms.GetBuffer()), playerID);

            SendData(this, args);
        }
    }
}
