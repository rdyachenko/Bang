using System;

namespace Logic
{
    public class ServerSendDataEventArgs : EventArgs
    {
        public ServerSendDataEventArgs(string xml, int playerID)
        {
            Xml = xml;
            PlayerID = playerID;
        }
        public string Xml { get; private set; }
        public int PlayerID { get; private set; }

    }
}
