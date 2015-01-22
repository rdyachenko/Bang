using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bang_.NetEngine
{
    enum BangCommand
    {
        GetGameList = 0,
        GamePingReply,
        GameClosed,
        ServerCommand,
        ClientCommand,
        Connect,
        ConnectReply,
        Disconnect,
        DisconnectReply,
        Message,
        AddToLog,
        SessionKey,
        SessionKeyReply
    }
}
