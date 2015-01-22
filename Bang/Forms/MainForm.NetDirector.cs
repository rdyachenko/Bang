using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using BangUIDesigner;
using Bang_.NetEngine;

namespace Bang_
{
    partial class MainForm: Form
    {
        private BangNetEngine NetDirector = new BangNetEngine();

        private delegate void ServerEventsCallback(BangNetEngine.BangNetServerEventArgs text);

        private void AssignNetDirectorEvents()
        {
            NetDirector.OnServerFound += new BangNetEngine.OnServerFoundHandler(NetDirector_OnServerFound);
            NetDirector.OnPlayerConnect += new BangNetEngine.OnPlayerConnectHandler(NetDirector_OnPlayerConnect);
            NetDirector.OnPlayerTryConnect += new BangNetEngine.OnPlayerTryConnectHandler(NetDirector_OnPlayerTryConnect);
            NetDirector.OnServerClosed += new BangNetEngine.OnServerClosedHandler(NetDirector_OnServerClosed);
            NetDirector.OnPlayerDisconnect += new BangNetEngine.OnPlayerDisconnectHandler(NetDirector_OnPlayerDisconnect);
            NetDirector.OnPlayerTryDisconnect += new BangNetEngine.OnPlayerTryDisconnectHandler(NetDirector_OnPlayerTryDisconnect);
            NetDirector.OnPlayerMessage += new BangNetEngine.OnPlayerMessageHandler(NetDirector_OnPlayerMessage);
            NetDirector.ListGamesRequest += new BangNetEngine.ListGamesRequestHandler(NetDirector_ListGamesRequest);
        }

        private void NetDirector_ListGamesRequest(out BangNetEngine.BangNetListGamesRequestArgs args)
        {
            BangNetEngine.BangNetListGamesRequestArgs a = new BangNetEngine.BangNetListGamesRequestArgs();
            foreach (Game g in Games.Values)
            {
                if (g.isLocal())
                    a.Add(g.Name);
            }
            args = a;
        }

        private void NetDirector_OnPlayerMessage(BangNetEngine.BangNetServerEventArgs args)
        {
            if (this.txtCommand.InvokeRequired)
            {
                ServerEventsCallback d = new ServerEventsCallback(NetDirector_OnPlayerMessage);
                this.Invoke(d, new object[] { args });
            }
            else
            {
                //Player p = Table.GetPlayerByIP(args.IP);
                //string player = " - ";
                //if (p != null)
                //    player = p.Name;
                string text = args.Text;
                //AddLogString(player,text);
                AddLogString("---", text);
            }
        }

        private void NetDirector_OnServerClosed(BangNetEngine.BangNetServerEventArgs args)
        {
            if (this.lstServers.InvokeRequired)
            {
                ServerEventsCallback d = new ServerEventsCallback(NetDirector_OnServerClosed);
                this.Invoke(d, new object[] { args });
            }
            else
            {
                foreach (Game g in Games.Values)
                {
                    if (g.Name == args.Text)
                    {
                        lstServers.Items.Remove(g);
                        Games.Remove(g.Name);
                    }
                }
            }
        }

        private void NetDirector_OnPlayerConnect(BangNetEngine.BangNetServerEventArgs args)
        {
            if (this.lstServers.InvokeRequired)
            {
                ServerEventsCallback d = new ServerEventsCallback(NetDirector_OnPlayerConnect);
                this.Invoke(d, new object[] { args });
            }
            else
            {
                try
                {
                    string playerName;
                    string gameName;
                    playerName = args.Text.Split('|')[1];
                    gameName = args.Text.Split('|')[0];
                    Client pl = new Client(playerName, args.SenderIP);
                    Game g = Games[gameName];
                    g.AddPlayer(pl);
                    lstServers.Items[lstServers.Items.IndexOf(g)] = g;
                    lstServers.Refresh();
                    if (g.localConnected && Tabs.TabPages.IndexOfKey(g.Name) == -1)
                    {
                        Tabs.TabPages.Add(g.Name, g.Name);
                        Tabs.TabPages[g.Name].Controls.Add(g.panel);
                        lstServers_SelectedIndexChanged(lstServers, new EventArgs());
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.Source);
                }
            }

        }

        private bool NetDirector_OnPlayerTryConnect(BangNetEngine.BangNetServerEventArgs args)
        {
            string playerName;
            string gameName;
            playerName = args.Text.Split('|')[1];
            gameName = args.Text.Split('|')[0];
            Game g = Games[gameName];
            if (g !=null)
                if (g.PlayerConnected(playerName))
                    return false;
                else
                    return true;
            return false;
        }

        private void NetDirector_OnPlayerDisconnect(BangNetEngine.BangNetServerEventArgs args)
        {
            if (this.lstServers.InvokeRequired)
            {
                ServerEventsCallback d = new ServerEventsCallback(NetDirector_OnPlayerDisconnect);
                this.Invoke(d, new object[] { args });
            }
            else
            {
                string playerName;
                string gameName;
                playerName = args.Text.Split('|')[1];
                gameName = args.Text.Split('|')[0];
                Game g = Games[gameName];
                g.RemovePlayer(playerName);
                lstServers.Items[lstServers.Items.IndexOf(g)] = g;
                lstServers.Refresh();
                lstServers.Update();
                MessageBox.Show("Disconnected: " + playerName + " from " + gameName, args.SenderIP);
                if (playerName == localUserName)
                {
                    Tabs.TabPages.Remove(Tabs.TabPages[g.Name]);
                }
            }

        }

        private bool NetDirector_OnPlayerTryDisconnect(BangNetEngine.BangNetServerEventArgs args)
        {
            string playerName;
            string gameName;
            playerName = args.Text.Split('|')[1];
            gameName = args.Text.Split('|')[0];
            Game g = Games[gameName];
            if (g!=null && g.PlayerConnected(playerName))
                return !g.isStarted;
            return true;
        }

        private void NetDirector_OnServerFound(BangNetEngine.BangNetServerEventArgs args)
        {
            if (this.lstServers.InvokeRequired)
            {
                ServerEventsCallback d = new ServerEventsCallback(NetDirector_OnServerFound);
                this.Invoke(d, new object[] { args });
            }
            else
            {
            try
            {
                string gName;
                gName = args.Text;
                Game g = new Game(gName, args.SenderIP, NetDirector);
                lstServers.Items.Add(g);
                Games.Add(g.Name, g);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
            }
        }

    }
}
