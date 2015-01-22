using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public interface IClientEngine
    {
        void RecieveData(string xmlAction);
        event EventHandler<ClientSendDataEventArgs> SendData;
    }
}
