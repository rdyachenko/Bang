using System;

namespace Logic
{
    public class ClientSendDataEventArgs : EventArgs
    {
        public ClientSendDataEventArgs(string xml)
        {
            Xml = xml;
        }
        public string Xml { get; private set; }
    }
}
