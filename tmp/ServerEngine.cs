using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;

namespace tmp
{
    public class ServerEngine: IServerEngine
    {
        public event EventHandler SendData;

        #region IServerEngine Members

        public void InitGame(int playersNumber)
        {
            throw new NotImplementedException();
        }

        public void RecieveData(int PlayerID, string xmlAction)
        {
            throw new NotImplementedException();
        }

        event EventHandler<ServerSendDataEventArgs> IServerEngine.SendData
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        #endregion
    }
}
