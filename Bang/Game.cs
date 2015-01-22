using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BangUIDesigner;
using Bang_.NetEngine;
using System.Net.NetworkInformation;
using Engine;

namespace Bang_
{  

    public class Game
    {
        private const int MINIMUM_PLAYERS_COUNT = 1;
        private const int MAXIMUM_PLAYERS_COUNT = 8;

        private BangUI UI;
        private UILogic uiLogic;
        private ClientEngine _clientEngine;
        private ServerEngine _serverEngine;
        private BangNetEngine NetConnector;
        public Panel panel = new Panel();
        private Button btnStart = new Button();
        private Label lblMessage = new Label();
        private string _Name;
        public bool isStarted { get; private set;}
        public string ServerIP { get; set; }
        public bool localConnected { get; private set; }

        private Dictionary<string, Client> _clients_ips = new Dictionary<string, Client>();
        private Dictionary<int, Client> _clients_ids = new Dictionary<int, Client>();
        private Dictionary<string, Client> _clients_names = new Dictionary<string, Client>();

        #region C-tors

        public Game(string name, string serverIP,  BangNetEngine netConnector)
        {
            Name = name;
            ServerIP = serverIP;
            UI = new BangUI();
            #region  Net init
            NetConnector = netConnector;
            NetConnector.OnServerCommand += new BangNetEngine.OnServerCommandHandler(NetConnector_OnGameCommand);
            NetConnector.OnClientCommand += new BangNetEngine.OnClientCommandHandler(NetConnector_OnClientCommand);
            #endregion  Net init
            #region  lblMessage init
            lblMessage.AutoSize = true;
            lblMessage.BackColor = System.Drawing.Color.Wheat;
            lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lblMessage.ForeColor = System.Drawing.Color.SaddleBrown;
            lblMessage.MaximumSize = new System.Drawing.Size(450, 300);
            lblMessage.MinimumSize = new System.Drawing.Size(250, 50);
            lblMessage.Padding = new System.Windows.Forms.Padding(3);
            lblMessage.Size = new System.Drawing.Size(250, 50);
            lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            panel.Controls.Add(lblMessage);
            #endregion  lblMessage init
            #region  panel init
            panel.BackColor = System.Drawing.Color.DarkGreen;
            panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel.Location = new System.Drawing.Point(3, 3);
            panel.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            panel.Dock = System.Windows.Forms.DockStyle.Fill;
            panel.SizeChanged += new System.EventHandler(this.Panel_SizeChanged);
            panel.ParentChanged += new EventHandler(Panel_ParentChanged);
            panel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseClick);
            panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseUp);
            panel.MouseMove += new MouseEventHandler(panel_MouseMove);
            #endregion  panel init
            #region  Server init
            if (isLocal())
            {
                #region  btnStart init
                btnStart.BackColor = System.Drawing.Color.LightCoral;
                btnStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
                btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                btnStart.Image = global::Bang_.Properties.Resources.gunfight;
                btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                btnStart.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
                btnStart.Size = new System.Drawing.Size(130, 37);
                btnStart.TabIndex = 5;
                btnStart.Text = "Почати гру!";
                btnStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                btnStart.UseVisualStyleBackColor = false;
                btnStart.Location = new System.Drawing.Point(0, 0);
                btnStart.Click += new System.EventHandler(this.btnStart_Click);
                panel.Controls.Add(btnStart);
                #endregion  btnStart init
                #region  ServerEngine init
                _serverEngine = new ServerEngine();
                _serverEngine.SendData += new EventHandler<ServerSendDataEventArgs>(_serverEngine_ServerSendData);
                #endregion  ServerEngine init
            }
            #endregion  Server init
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, _clients_names.Count());
        }

        #endregion

        #region Properties and functions

        public string Name 
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public bool isLocal()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
            return ServerIP.Equals(connections[0].LocalEndPoint.Address.ToString());
        }
        
        public bool PlayerConnected(string playerName)
        {
            return _clients_names.ContainsKey(playerName);
        }
        
        public int PlayerCount()
        {
            return _clients_ids.Count();
        }

        #endregion Properties and functions
        
        #region public methods

        public void AddPlayer(Client client)
        {
            int pID = _clients_ips.Count;
            client.ID = pID;
            if (_serverEngine!=null)
                _clients_ips.Add(client.IP,client);
            else
                _clients_ids.Add(client.ID, client);
            _clients_names.Add(client.Name, client);
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
            if (client.IP.Equals(connections[0].LocalEndPoint.Address.ToString()))
            {
                localConnected = true;
                uiLogic = new UILogic(UI);
                _clientEngine = new ClientEngine(uiLogic);
                _clientEngine.PlayerSendData += new EventHandler<ClientSendDataEventArgs>(_clientEngine_PlayerSendData);
            }
            //btnStart.Enabled = logic.PlayersCount() > 2;
        }

        internal void RemovePlayer(string playerName)
        {
            Client pl = _clients_names[playerName];
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
            if (pl.IP.Equals(connections[0].LocalEndPoint.Address.ToString()))
                localConnected = false;
            _clients_names.Remove(pl.Name);
            _clients_ids.Remove(pl.ID);
            _clients_ips.Remove(pl.IP);
            //btnStart.Enabled = logic.PlayersCount() > 2;
        }
        public void StartGame()
        {
            if (!isStarted)
            {
                isStarted = true;
                _serverEngine.InitGame(_clients_ids.Count); //
            }
        }

        #endregion
        
        #region UI events

        private void Panel_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                UI.ChangeSize(((Panel)sender).ClientSize);
                btnStart.Left = (panel.Width - 130) / 2;
                btnStart.Top = (panel.Height - 37) / 2;
                lblMessage.Left = 20; // (panel.Width - lblMessage.Width) / 2;
                lblMessage.Top = 20; // (panel.Height - lblMessage.Height) / 2;
            }
            catch (System.Exception) {  }
        }
        private void Panel_ParentChanged(object sender, EventArgs e)
        {
            try
            {
                UI.Init(panel.CreateGraphics(), panel.ClientSize);
            }
            catch (System.Exception) { }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_clients_ids.Count >= MINIMUM_PLAYERS_COUNT)
            {
                btnStart.Hide();
                StartGame();
            }
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isStarted)
            {
                lblMessage.Text = "Очікуємо старту гри...";
            }
            else
            {
                ObjectData od = UI.GetObjectData(e.Location);
                switch (od.SourceObject.Type)
                {
                    case ObjectType.Player:
                        lblMessage.Text = od.SourceObject.PlayerDetailes.PlayerID.Replace('_', ' ');
                        break;
                    case ObjectType.Table:
                        lblMessage.Text = "Table";
                        break;
                    case ObjectType.Peal:
                        lblMessage.Text = "Колода карт";
                        break;
                    case ObjectType.Clear:
                        lblMessage.Text = "Відбій";
                        break;
                    case ObjectType.None:
                        lblMessage.Text = "Поза столом";
                        break;
                    case ObjectType.Card:
                        //try {lblMessage.Text = "Карта: " + logic._cardBatch.CardByID(od.SourceObject.CardDetailes.CardID).CardAction; }
                        //catch (System.Exception) { }
                        break;
                }
            }
        }
        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
            if (isStarted && e.Button == MouseButtons.Left)
            {
                uiLogic.ClickOnObject(e.Location);
            }

        }
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ObjectData od = UI.GetObjectData(e.Location);

                switch (od.SourceObject.Type)
                {
                    case ObjectType.Player:
                        od.DestinationObject = od.SourceObject;
                        UI.DoAction(BangActions.Ligth, od);
                        break;
                    case ObjectType.Card:
                        od.DestinationObject = od.SourceObject;
                        od.DestinationObject.Tag = 1;
                        UI.DoAction(BangActions.ShowCard, od);
                        break;
                }
            }
        }
        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ObjectData od = UI.GetObjectData(e.Location);

                if (e.Button == MouseButtons.Right)
                {
                    switch (od.SourceObject.Type)
                    {
                        case ObjectType.Player:
                            // show info about player
                            break;
                        case ObjectType.Card:
                            od.DestinationObject = od.SourceObject;
                            od.DestinationObject.Tag = 0;
                            UI.DoAction(BangActions.ShowCard, od);
                            break;
                    }
                }
            }
        }

        #endregion UI events

        #region Server With Client Connect
        void NetConnector_OnGameCommand(BangNetEngine.BangNetServerEventArgs args)
        {
            string[] EventData = args.Text.Split("|".ToCharArray(), 2);
            if (Name == EventData[0])
                _clientEngine.GameReceiveData(EventData[1]);
        }

        void NetConnector_OnClientCommand(BangNetEngine.BangNetServerEventArgs args)
        {
            string[] EventData = args.Text.Split("|".ToCharArray(),2);
            if (Name == EventData[0])
            {
                int player = _clients_ips[args.SenderIP].ID;
                string additionalInfo = EventData[1];
                _serverEngine.ReceiveData(player, additionalInfo);
            }
        }

        void _clientEngine_PlayerSendData(object sender, ClientSendDataEventArgs e)
        {
            NetConnector.ClientCommand(Name + '|' + e.Xml,ServerIP);
        }

        void _serverEngine_ServerSendData(object sender, ServerSendDataEventArgs e)
        {
            string ip = _clients_ids[e.PlayerID].IP;
            string data = e.Xml;
            NetConnector.ServerCommand(string.Format("{0}|{1}", Name, data), ip);
        }
        #endregion Server With Client Connect
    }
}
