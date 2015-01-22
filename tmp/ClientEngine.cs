using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;
namespace tmp
{
    public class ClientEngine: IClientEngine
    {
        public event EventHandler SendData;
        public event EventHandler GameEvent;

        public UILogic UILogic
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    

        public void UserEvent()
        {
            throw new System.NotImplementedException();
        }

        #region IClientEngine Members

        public void RecieveData(string xmlAction)
        {
            throw new NotImplementedException();
        }

        event EventHandler<ClientSendDataEventArgs> IClientEngine.SendData
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        #endregion
    }
}
