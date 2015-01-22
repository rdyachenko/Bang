using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public interface IServerEngine
    {
        //(int PlayerID, string xmlAction);

        void InitGame(int playersNumber);
        void RecieveData(int PlayerID, string xmlAction);
        event EventHandler<ServerSendDataEventArgs> SendData;
    }


}