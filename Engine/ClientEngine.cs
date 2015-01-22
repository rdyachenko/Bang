using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Engine
{
    public class ClientEngine
    {
        private ClientEngine()
        {

        }

        public ClientEngine(IUILogic ui)
        {
            _ui = ui;
            _ui.UserEvent += new EventHandler<ClientEventArgs>(UserEvent); 
        }

        private IUILogic _ui;


        public void GameReceiveData(string xmlAction)
        {
            XmlSerializer xs = new XmlSerializer(typeof(GameAction));
            TextReader tr = new StringReader(xmlAction);
            GameAction action = (GameAction)xs.Deserialize(tr);

            tr.Close();

            switch (action.Type)
            {
                case GameActionType.InitGameTable:
                    processGameInit(action.N);
                    break;
                case GameActionType.CardFromPeal:
                    processTakeCardAction(action.Card);
                    break;
                case GameActionType.PlayerCardFromPeal:
                    processPlayerTakeCardAction(action.PlayerID);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void processGameInit(int numberPlayers)
        {
            string xml = "<players count=\"2\">";
            xml += "<player id=\"Bart_Cassidy\" user=\"0\" lifes=\"5\" role=\"Sherif\">";
            xml += "<cards count=\"0\">";
            xml += "</cards>";
            xml += "</player>";
            xml += "<player id=\"El_Gringo\" user=\"1\" lifes=\"3\" role=\"Outlaw\">";
            xml += "<cards count=\"0\">";
            xml += "</cards>";
            xml += "</player>";
            xml += "</players>";

            _ui.Init(xml);
        }

        private void processPlayerTakeCardAction(int playerID)
        {
            _ui.AnotherPlayerGotCardFromPeal(1);
        }

        private void processTakeCardAction(string cardID)
        {
            _ui.GotCardFromPeal(Int32.Parse(cardID));
        }

        void UserEvent(object sender,ClientEventArgs e)
        {
            switch (e.EventType)
            {
                case UserEventType.ClickPeal:
                    performClickPealAction();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void performClickPealAction()
        {
            PlayerAction action = new PlayerAction { Type = PlayerActionType.TakeCardAction };

            string xml = serializePlayerAction(action);

            ClientSendDataEventArgs args = new ClientSendDataEventArgs(xml);

            PlayerSendData(this, args);
        }

        private string serializePlayerAction(PlayerAction action)
        {
            XmlSerializer xs = new XmlSerializer(typeof(PlayerAction));
            MemoryStream ms = new MemoryStream();
            xs.Serialize(ms, action);

            return Encoding.UTF8.GetString(ms.GetBuffer());
        }

        public event EventHandler<ClientSendDataEventArgs> PlayerSendData;
    }
}
