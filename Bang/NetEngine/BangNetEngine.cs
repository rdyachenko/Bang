using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bang_.NetEngine
{
    public class BangNetEngine
    {
        #region Local variables
        private BangUdpWrapper udpWrapper = null;
        private BangCryptoWrapper cryptoWrapper = null;
        private Dictionary<string, string> sessionKeys = new Dictionary<string, string>();
        private string defaultKey = "03a9Pgy+ysDbYrhdvJUwc5jf82Zp1730Sb3R0NaNKJ4=";
        #endregion

        #region Delegate & Event definition

        public delegate void OnPlayerMessageHandler(BangNetServerEventArgs args);
        public delegate void OnServerFoundHandler(BangNetServerEventArgs args);
        public delegate void OnServerClosedHandler(BangNetServerEventArgs args);
        public delegate bool OnPlayerTryConnectHandler(BangNetServerEventArgs args);
        public delegate void OnPlayerConnectHandler(BangNetServerEventArgs args);
        public delegate bool OnPlayerTryDisconnectHandler(BangNetServerEventArgs args);
        public delegate void OnPlayerDisconnectHandler(BangNetServerEventArgs args);
        public delegate void OnClientCommandHandler(BangNetServerEventArgs args);
        public delegate void OnServerCommandHandler(BangNetServerEventArgs args);
        public delegate void ListGamesRequestHandler(out BangNetListGamesRequestArgs args);
        public delegate void OnAddToLogHandler(BangNetServerEventArgs args);

        public event OnPlayerMessageHandler OnPlayerMessage;
        public event OnServerFoundHandler OnServerFound;
        public event OnServerClosedHandler OnServerClosed;
        public event OnPlayerTryConnectHandler OnPlayerTryConnect;
        public event OnPlayerConnectHandler OnPlayerConnect;
        public event OnPlayerTryDisconnectHandler OnPlayerTryDisconnect;
        public event OnPlayerDisconnectHandler OnPlayerDisconnect;
        public event OnServerCommandHandler OnServerCommand;
        public event OnClientCommandHandler OnClientCommand;
        public event ListGamesRequestHandler ListGamesRequest;
        public event OnAddToLogHandler OnAddToLog;
        
        #region EventArguments definition
        public class BangNetServerEventArgs : EventArgs
        {
            private string senderIP;
            private string text;

            public string SenderIP
            {
                get { return senderIP; }
                set { senderIP = value; }
            }
            public string Text
            {
                get { return text; }
                set { text = value; }
            }

            public BangNetServerEventArgs(string senderIP, string name)
            {
                this.senderIP = senderIP;
                this.text = name;
            }
        }
        public class BangNetListGamesRequestArgs: EventArgs
        {
            public List<string> Games = new List<string>();
            public void Add(string gameName)
            {
                Games.Add(gameName);
            }
        }
        #endregion
        #endregion

        #region C-tors
        public BangNetEngine()
        {
            cryptoWrapper = new BangCryptoWrapper();
            udpWrapper = new BangUdpWrapper();
            udpWrapper.OnDataReceived += new BangUdpWrapper.OnDataReceivedHandler(udpWrapper_OnDataReceived);
        }
        #endregion

        #region Helper functionality
        void udpWrapper_OnDataReceived(BangUdpWrapper.OnDataReceivedArgs eventArgs)
        {
            ProcessData(eventArgs.SenderIP, eventArgs.DataReceived);
        }

        private void ProcessData(string senderIP, string dataReceived)
        {
            char cmd;
            string extraData;
            string extraDataDecripted;
            string extraDataDecriptedDefault;
            if (dataReceived.Length > 0)
            {
                cmd = dataReceived[0];
                extraData = dataReceived.Substring(1);
                extraDataDecripted = cryptoWrapper.Decrypt(extraData, GetKey(senderIP));
                extraDataDecriptedDefault = cryptoWrapper.Decrypt(extraData, GetKey());
                #region Switch command
                switch (cmd)
                {
                    #region Server Side Messages
                    case (char)BangCommand.GetGameList:
                        BangNetListGamesRequestArgs args = new BangNetListGamesRequestArgs();
                        if (ListGamesRequest != null)
                            ListGamesRequest(out args);
                        if (args.Games.Count() > 0)
                        {
                            foreach (string a in args.Games)
                            {
                                Send(senderIP, BangCommand.GamePingReply, cryptoWrapper.Encrypt(a, GetKey(senderIP)));
                            }

                        }
                        break;
                    case (char)BangCommand.Connect:
                        if (OnPlayerTryConnect != null)
                        {
                            if (OnPlayerTryConnect(new BangNetServerEventArgs(senderIP, extraDataDecriptedDefault)))
                                Send(BangCommand.ConnectReply, cryptoWrapper.Encrypt(extraDataDecriptedDefault, GetKey()));
                        }
                        break;
                    case (char)BangCommand.Disconnect:
                        if (OnPlayerTryDisconnect != null)
                        {
                            if (OnPlayerTryDisconnect(new BangNetServerEventArgs(senderIP, extraDataDecripted)))
                                Send(BangCommand.DisconnectReply, cryptoWrapper.Encrypt(extraDataDecripted, GetKey()));
                        }
                        break;
                    case (char)BangCommand.Message:
                        if (OnPlayerMessage != null)
                        {
                            extraData = cryptoWrapper.Decrypt(extraData, GetKey(senderIP));
                            OnPlayerMessage(new BangNetServerEventArgs(senderIP, extraData));
                        }
                        break;
                    #endregion

                    #region Client Side Messages
                    case (char)BangCommand.GamePingReply:
                        if (OnServerFound != null)
                            OnServerFound(new BangNetServerEventArgs(senderIP, extraDataDecriptedDefault));
                        break;
                    case (char)BangCommand.GameClosed:
                        if (OnServerClosed != null)
                            OnServerClosed(new BangNetServerEventArgs(senderIP, extraDataDecriptedDefault));
                        break;
                    case (char)BangCommand.DisconnectReply:
                        if (OnPlayerDisconnect != null)
                            OnPlayerDisconnect(new BangNetServerEventArgs(senderIP, extraDataDecriptedDefault));
                        break;
                    case (char)BangCommand.ConnectReply:
                        if (OnPlayerConnect != null)
                            OnPlayerConnect(new BangNetServerEventArgs(senderIP, extraDataDecriptedDefault));
                        break;
                    case (char)BangCommand.AddToLog:
                        if (OnAddToLog != null)
                            OnAddToLog(new BangNetServerEventArgs(senderIP, extraDataDecripted));
                        break;
                    #endregion

                    #region General Messages
                    case (char)BangCommand.ServerCommand:
                        if (OnServerCommand != null)
                        {
                            extraData = cryptoWrapper.Decrypt(extraData, GetKey(senderIP));
                            OnServerCommand(new BangNetServerEventArgs(senderIP, extraData));
                        }
                        break;
                    case (char)BangCommand.ClientCommand:
                        if (OnClientCommand != null)
                        {
                            extraData = cryptoWrapper.Decrypt(extraData, GetKey(senderIP));
                            OnClientCommand(new BangNetServerEventArgs(senderIP, extraData));
                        }
                        break;
                    #endregion

                    #region Technical Messages
                    case (char)BangCommand.SessionKey:
                        // Request to generate & send session key, encrypted with open key specified
                        {
                            byte[] newSessionKey = cryptoWrapper.GenerateRandomKey();
                            string encryptedSessionKey = cryptoWrapper.GenerateEncryptedSessionKey(extraData, newSessionKey);    //extraData == open key
                            if (!encryptedSessionKey.Equals(""))
                            {
                                string newSessionKeyStr = Convert.ToBase64String(newSessionKey); // cryptoWrapper.GetSessionKey(extraData);  // extraData == encrypted session key
                                if (sessionKeys.Keys.Contains(senderIP))
                                {
                                    sessionKeys[senderIP] = newSessionKeyStr;
                                }
                                else
                                {
                                    sessionKeys.Add(senderIP, newSessionKeyStr);
                                }
                                // Send back to server
                                Send(senderIP, BangCommand.SessionKeyReply, encryptedSessionKey);
                            }
                        }
                        break;
                    case (char)BangCommand.SessionKeyReply:
                        // Request reply contains client generated session key, encrypted with server open key
                        {
                            string newSessionKey = cryptoWrapper.GetSessionKey(extraData);  // extraData == encrypted session key
                            if (!newSessionKey.Equals(""))
                            {
                                if (sessionKeys.Keys.Contains(senderIP))
                                {
                                    sessionKeys[senderIP] = newSessionKey;
                                }
                                else
                                {
                                    sessionKeys.Add(senderIP, newSessionKey);
                                }
                            }
                        }
                        break;
                    #endregion
                }
                #endregion
            }
        }

        private void Send(BangCommand cmd)
        {
            Send(cmd, "");
        }

        private void Send(BangCommand cmd, string extraData)
        {
            udpWrapper.Send((char)cmd + extraData);
        }

        private void Send(string ipAddress, BangCommand cmd)
        {
            Send(ipAddress, cmd, "");
        }

        private void Send(string ipAddress, BangCommand cmd, string extraData)
        {
            udpWrapper.Send(ipAddress, (char)cmd + extraData);
        }
        
        private string GetKey(string remoteIP)
        {
            if (sessionKeys.Keys.Contains(remoteIP))
                return sessionKeys[remoteIP];
            return defaultKey;
        }

        private string GetKey()
        {
            return defaultKey;
        }
        
        #endregion

        #region Public functionality

        public void StartGame(string gameName)
        {
            Send(BangCommand.GamePingReply, cryptoWrapper.Encrypt(gameName, GetKey()));
            Send(BangCommand.SessionKey, cryptoWrapper.GetPublicKey());
        }

        public void CloseGame(string gameName)
        {
            Send(BangCommand.GameClosed, cryptoWrapper.Encrypt(gameName, GetKey()));
        }

        public void ConnectGame(string ipAddress, string serverName, string playerName)
        {
            string textData = cryptoWrapper.Encrypt(serverName + "|" + playerName, GetKey());
            Send(ipAddress, BangCommand.Connect, textData);
            //Send(BangCommand.SessionKey, cryptoWrapper.GetPublicKey());
        }

        public void DisconnectGame(string gameServerIP, string serverName, string playerName)
        {
            string textData = cryptoWrapper.Encrypt(serverName + "|" + playerName, GetKey());
            Send(gameServerIP, BangCommand.Disconnect, textData);
        }

        public void ServerCommand(string gameData, string clientIP)
        {
            gameData = cryptoWrapper.Encrypt(gameData, GetKey(clientIP));
            Send(clientIP, BangCommand.ServerCommand, gameData);
        }

        public void ClientCommand(string gameData, string serverIP)
        {
            gameData = cryptoWrapper.Encrypt(gameData, GetKey(serverIP));
            Send(serverIP, BangCommand.ClientCommand, gameData);
        }

        public void SendMessage(string message, string serverIP)
        {
            message = cryptoWrapper.Encrypt(message, GetKey(serverIP));
            Send(serverIP, BangCommand.Message, message);
        }

        public void GetGameList()
        {
            Send(BangCommand.GetGameList);
        }

        public void Close()
        {
            udpWrapper.Close();
        }

        #endregion
    }
}
