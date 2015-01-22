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
using Bang_.NetEngine;

namespace Bang_
{
	public partial class MainForm: Form
	{    

        private SortedList<String,Game> Games = new SortedList<String,Game>();
        private List<string> GameNames = new List<string>();
        public MainForm()
		{
			InitializeComponent();
		}
        public string localUserName = "";
        private string lastWriter = "";
        private string SelectedServerIP = "";


        #region Form events

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.LoginPanel.BackgroundImage = global::Bang_.Properties.Resources.RSG;
            AssignNetDirectorEvents();
            txtLogin.Text = Environment.MachineName;
            NetDirector.GetGameList();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            NetDirector.Close();
            NetDirector = null;
        }
        
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && txtInput.Text.Trim().Length>0)
            {
                NetDirector.SendMessage(txtInput.Text,SelectedServerIP);
                txtInput.Text = "";
                e.Handled = true;
            }
        }

        private void AddLogString(string player, string text)
        {
            Font RegularFont = new Font(txtCommand.Font, FontStyle.Regular);
            Font BoldFont = new Font(txtCommand.Font, FontStyle.Bold);

            txtCommand.SelectionStart = txtCommand.Text.Length;
            if (lastWriter != player)
            {
                txtCommand.SelectedText = "\r";
                txtCommand.SelectionFont = BoldFont;
                txtCommand.SelectionColor = Color.Brown;
                txtCommand.SelectedText = player;
                lastWriter = player;
            }
            txtCommand.SelectedText = "\r";
            txtCommand.SelectionFont = RegularFont;
            txtCommand.SelectionColor = Color.Blue;
            txtCommand.SelectedText = " ";
            txtCommand.SelectedText = text;
        }

        private void ShowHideLog(object sender, EventArgs e)
        {
            if (sender.Equals(cmdShowLog))
            {
                splitContainer1.Panel2Collapsed = false;
                cmdShowLog.Hide();
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                cmdShowLog.Show();
            }
        }

        private void lstServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstServers.SelectedItem is Game)
            {
                Game item = (Game)lstServers.SelectedItem;
                if (!item.PlayerConnected(localUserName) && !item.isStarted && (item.PlayerCount() < 7))
                {
                    cmdConnect.Enabled = true;
                    cmdDisconnect.Enabled = false;
                }
                else
                {
                    if (item.PlayerConnected(localUserName) )
                        cmdDisconnect.Enabled = true;
                    cmdConnect.Enabled = false;
                }
                cmdCloseGame.Enabled = item.isLocal() && !item.isStarted;
            }
            else
            {
                cmdCloseGame.Enabled = false;
                cmdConnect.Enabled = false;
                cmdDisconnect.Enabled = false;
            }
        }

        #endregion Form events

        private void cmdNewGame_Click(object sender, EventArgs e)
        {
            string gameName = "New game";
            if (Bang_.Other.InputBox.Prompt("New game", "Type game title", ref gameName) == DialogResult.OK)
                NetDirector.StartGame(gameName);
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            if (cmdConnect.Enabled)
            {
                Game g = (Game)lstServers.SelectedItem;
                NetDirector.ConnectGame(g.ServerIP, g.Name, localUserName);
            }
        }
 
        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            Game srv = (Game)lstServers.SelectedItem;
            NetDirector.DisconnectGame(srv.ServerIP, srv.Name, localUserName);
        }

        private void TablePanel_Resize(object sender, EventArgs e)
        {
            
        }

        private void btnCloseGame_Click(object sender, EventArgs e)
        {
            Game srv = (Game)lstServers.SelectedItem;
            NetDirector.CloseGame(srv.Name);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            cmdLogin.Enabled = txtLogin.Text.Length>0;
        }

        private void cmdLogin_Click(object sender, EventArgs e)
        {
            localUserName = txtLogin.Text;
            LoginPanel.Hide();
            ServersPanel.Show();
        }

        private void tabPage1_Resize(object sender, EventArgs e)
        {
            ServersPanel.Left = (tabPage1.Width - ServersPanel.Width) / 2;
            ServersPanel.Top = (tabPage1.Height - ServersPanel.Height) / 2;
            LoginPanel.Left = (tabPage1.Width - ServersPanel.Width) / 2;
            LoginPanel.Top = (tabPage1.Height - ServersPanel.Height) / 2;
        }

        private void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Tabs.SelectedIndex>0)
            {
                string game = Tabs.SelectedTab.Text;
                SelectedServerIP = Games[game].ServerIP;
                if (splitContainer1.Panel2Collapsed)
                    cmdShowLog.Visible = true;
            }
            else
            {
                SelectedServerIP = "";
                splitContainer1.Panel2Collapsed = true;
            }
        }

        private void lstServers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdConnect_Click(cmdConnect, new EventArgs());
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Text = string.Format("Width={0}, Height={1}", Width,Height);
        }

    }
}
