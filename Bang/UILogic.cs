using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using BangUIDesigner;
using System.Drawing;

namespace Bang_
{
    class UILogic:IUILogic
    {
        private BangUI UI;
        public UILogic(BangUI ui)
        {
            UI = ui;
        }

        public delegate void UserEventHandler(ClientEventArgs args);
        public event EventHandler<ClientEventArgs> UserEvent;

        #region IUILogic Members

        public void Init(string tableAsXML)
        {
            UI.CreateTable(tableAsXML);
        }

        public void GotCardFromPeal(int cardID)
        {
            ObjectData od = new ObjectData();
            od.SourceObject.Type = ObjectType.Peal;
            od.DestinationObject.Type = ObjectType.Player;
            od.DestinationObject.PlayerDetailes.PlayerID = "Bart_Cassidy";
            od.DestinationObject.CardDetailes.CardID=cardID;
            UI.DoAction(BangActions.AddCard, od);
        }

        public void AnotherPlayerGotCardFromPeal(int playerID)
        {
            ObjectData od = new ObjectData();
            od.SourceObject.Type = ObjectType.Peal;
            od.DestinationObject.Type = ObjectType.Player;
            od.DestinationObject.PlayerDetailes.PlayerID = "El_Gringo";
            od.DestinationObject.CardDetailes.CardID = 20;
            UI.DoAction(BangActions.AddCard, od);
        }

        #endregion


        internal void ClickOnObject(Point location)
        {
            ObjectData od = UI.GetObjectData(location);

            switch (od.SourceObject.Type)
            {
                case ObjectType.Peal:
                    if (UserEvent!=null)
                        UserEvent(this,new ClientEventArgs(UserEventType.ClickPeal));    
                    break;
                case ObjectType.Clear:
                    //NetConnector.ServerCommand(string.Format("{0}|{1}|{2}", Name, logic.LocalPlayer().ID, UIactions.clickOnClear), ServerIP);   
                    break;
                case ObjectType.Table:
                    //NetConnector.ServerCommand(string.Format("{0}|{1}|{2}", Name, logic.LocalPlayer().ID, UIactions.clickOnTable), ServerIP);   
                    break;
                case ObjectType.Player:
                    //NetConnector.ServerCommand(string.Format("{0}|{1}|{2}|{3}", Name, logic.LocalPlayer().ID, UIactions.clickOnPlayer, od.SourceObject.PlayerDetailes.PlayerID), ServerIP);   
                    break;
                case ObjectType.Card:
                    //NetConnector.ServerCommand(string.Format("{0}|{1}|{2}|{3}", Name, logic.LocalPlayer().ID, UIactions.clickOnCard, od.SourceObject.CardDetailes.CardID), ServerIP);   
                    break;
            }
        }

        #region IUILogic Members



        #endregion
    }
}
