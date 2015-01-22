namespace Bang_
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdShowLog = new System.Windows.Forms.Button();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.LoginPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.cmdLogin = new System.Windows.Forms.Button();
            this.ServersPanel = new System.Windows.Forms.Panel();
            this.cmdDisconnect = new System.Windows.Forms.Button();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.cmdCloseGame = new System.Windows.Forms.Button();
            this.cmdNewGame = new System.Windows.Forms.Button();
            this.ImageGirl = new System.Windows.Forms.PictureBox();
            this.cmdHideLog = new System.Windows.Forms.Button();
            this.txtCommand = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lstServers = new Bang_.GameListBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.LoginPanel.SuspendLayout();
            this.ServersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageGirl)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmdShowLog);
            this.splitContainer1.Panel1.Controls.Add(this.Tabs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdHideLog);
            this.splitContainer1.Panel2.Controls.Add(this.txtCommand);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.txtInput);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1106, 649);
            this.splitContainer1.SplitterDistance = 832;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 12;
            // 
            // cmdShowLog
            // 
            this.cmdShowLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdShowLog.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cmdShowLog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.ForestGreen;
            this.cmdShowLog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.ForestGreen;
            this.cmdShowLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdShowLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdShowLog.ForeColor = System.Drawing.Color.GreenYellow;
            this.cmdShowLog.Location = new System.Drawing.Point(1075, 0);
            this.cmdShowLog.Name = "cmdShowLog";
            this.cmdShowLog.Size = new System.Drawing.Size(29, 22);
            this.cmdShowLog.TabIndex = 11;
            this.cmdShowLog.Text = "<<";
            this.cmdShowLog.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdShowLog.UseVisualStyleBackColor = true;
            this.cmdShowLog.Click += new System.EventHandler(this.ShowHideLog);
            // 
            // Tabs
            // 
            this.Tabs.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.Tabs.Controls.Add(this.tabPage1);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(1106, 649);
            this.Tabs.TabIndex = 13;
            this.Tabs.SelectedIndexChanged += new System.EventHandler(this.Tabs_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(149)))), ((int)(((byte)(174)))));
            this.tabPage1.Controls.Add(this.LoginPanel);
            this.tabPage1.Controls.Add(this.ServersPanel);
            this.tabPage1.Controls.Add(this.ImageGirl);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1098, 623);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Servers";
            this.tabPage1.Resize += new System.EventHandler(this.tabPage1_Resize);
            // 
            // LoginPanel
            // 
            this.LoginPanel.BackColor = System.Drawing.Color.PaleGreen;
            this.LoginPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.LoginPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoginPanel.Controls.Add(this.label2);
            this.LoginPanel.Controls.Add(this.txtLogin);
            this.LoginPanel.Controls.Add(this.cmdLogin);
            this.LoginPanel.Location = new System.Drawing.Point(39, 185);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(570, 322);
            this.LoginPanel.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(290, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 32);
            this.label2.TabIndex = 7;
            this.label2.Text = "Назви себе, ковбою!";
            // 
            // txtLogin
            // 
            this.txtLogin.BackColor = System.Drawing.Color.Linen;
            this.txtLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtLogin.Location = new System.Drawing.Point(15, 224);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(229, 38);
            this.txtLogin.TabIndex = 1;
            this.txtLogin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLogin.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // cmdLogin
            // 
            this.cmdLogin.BackColor = System.Drawing.Color.Khaki;
            this.cmdLogin.Enabled = false;
            this.cmdLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cmdLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Khaki;
            this.cmdLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdLogin.Image = global::Bang_.Properties.Resources.Gun_32x32;
            this.cmdLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdLogin.Location = new System.Drawing.Point(65, 268);
            this.cmdLogin.Name = "cmdLogin";
            this.cmdLogin.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.cmdLogin.Size = new System.Drawing.Size(131, 37);
            this.cmdLogin.TabIndex = 5;
            this.cmdLogin.Text = "Продовжити";
            this.cmdLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdLogin.UseVisualStyleBackColor = false;
            this.cmdLogin.Click += new System.EventHandler(this.cmdLogin_Click);
            // 
            // ServersPanel
            // 
            this.ServersPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(164)))), ((int)(((byte)(186)))));
            this.ServersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ServersPanel.Controls.Add(this.cmdDisconnect);
            this.ServersPanel.Controls.Add(this.cmdConnect);
            this.ServersPanel.Controls.Add(this.lstServers);
            this.ServersPanel.Controls.Add(this.cmdCloseGame);
            this.ServersPanel.Controls.Add(this.cmdNewGame);
            this.ServersPanel.ForeColor = System.Drawing.Color.Black;
            this.ServersPanel.Location = new System.Drawing.Point(269, 90);
            this.ServersPanel.Name = "ServersPanel";
            this.ServersPanel.Size = new System.Drawing.Size(562, 389);
            this.ServersPanel.TabIndex = 15;
            this.ServersPanel.Visible = false;
            // 
            // cmdDisconnect
            // 
            this.cmdDisconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdDisconnect.Enabled = false;
            this.cmdDisconnect.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.cmdDisconnect.FlatAppearance.CheckedBackColor = System.Drawing.Color.Blue;
            this.cmdDisconnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmdDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("cmdDisconnect.Image")));
            this.cmdDisconnect.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdDisconnect.Location = new System.Drawing.Point(432, 102);
            this.cmdDisconnect.Name = "cmdDisconnect";
            this.cmdDisconnect.Padding = new System.Windows.Forms.Padding(5);
            this.cmdDisconnect.Size = new System.Drawing.Size(120, 89);
            this.cmdDisconnect.TabIndex = 12;
            this.cmdDisconnect.Text = "Відключитись";
            this.cmdDisconnect.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdDisconnect.UseVisualStyleBackColor = false;
            this.cmdDisconnect.Click += new System.EventHandler(this.cmdDisconnect_Click);
            // 
            // cmdConnect
            // 
            this.cmdConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdConnect.Enabled = false;
            this.cmdConnect.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.cmdConnect.FlatAppearance.CheckedBackColor = System.Drawing.Color.Blue;
            this.cmdConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmdConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdConnect.Image = ((System.Drawing.Image)(resources.GetObject("cmdConnect.Image")));
            this.cmdConnect.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdConnect.Location = new System.Drawing.Point(432, 7);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Padding = new System.Windows.Forms.Padding(5);
            this.cmdConnect.Size = new System.Drawing.Size(120, 89);
            this.cmdConnect.TabIndex = 12;
            this.cmdConnect.Text = "Підключитись";
            this.cmdConnect.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdConnect.UseVisualStyleBackColor = false;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "unlock-icon-32.png");
            this.imageList2.Images.SetKeyName(1, "lock-icon-32.png");
            this.imageList2.Images.SetKeyName(2, "button-ok-icon-32.png");
            this.imageList2.Images.SetKeyName(3, "button-cancel-icon-32.png");
            // 
            // cmdCloseGame
            // 
            this.cmdCloseGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdCloseGame.Enabled = false;
            this.cmdCloseGame.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.cmdCloseGame.FlatAppearance.CheckedBackColor = System.Drawing.Color.Blue;
            this.cmdCloseGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmdCloseGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCloseGame.Image = ((System.Drawing.Image)(resources.GetObject("cmdCloseGame.Image")));
            this.cmdCloseGame.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCloseGame.Location = new System.Drawing.Point(432, 293);
            this.cmdCloseGame.Name = "cmdCloseGame";
            this.cmdCloseGame.Padding = new System.Windows.Forms.Padding(5);
            this.cmdCloseGame.Size = new System.Drawing.Size(120, 89);
            this.cmdCloseGame.TabIndex = 13;
            this.cmdCloseGame.Text = "Відмінити гру";
            this.cmdCloseGame.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCloseGame.UseVisualStyleBackColor = false;
            this.cmdCloseGame.Click += new System.EventHandler(this.btnCloseGame_Click);
            // 
            // cmdNewGame
            // 
            this.cmdNewGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdNewGame.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.cmdNewGame.FlatAppearance.CheckedBackColor = System.Drawing.Color.Blue;
            this.cmdNewGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmdNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdNewGame.Image = ((System.Drawing.Image)(resources.GetObject("cmdNewGame.Image")));
            this.cmdNewGame.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdNewGame.Location = new System.Drawing.Point(432, 198);
            this.cmdNewGame.Name = "cmdNewGame";
            this.cmdNewGame.Padding = new System.Windows.Forms.Padding(5);
            this.cmdNewGame.Size = new System.Drawing.Size(120, 89);
            this.cmdNewGame.TabIndex = 13;
            this.cmdNewGame.Text = "Нова гра";
            this.cmdNewGame.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdNewGame.UseVisualStyleBackColor = false;
            this.cmdNewGame.Click += new System.EventHandler(this.cmdNewGame_Click);
            // 
            // ImageGirl
            // 
            this.ImageGirl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ImageGirl.Image = global::Bang_.Properties.Resources.girl;
            this.ImageGirl.Location = new System.Drawing.Point(8, 243);
            this.ImageGirl.Name = "ImageGirl";
            this.ImageGirl.Size = new System.Drawing.Size(130, 390);
            this.ImageGirl.TabIndex = 16;
            this.ImageGirl.TabStop = false;
            // 
            // cmdHideLog
            // 
            this.cmdHideLog.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cmdHideLog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.cmdHideLog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.cmdHideLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdHideLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdHideLog.ForeColor = System.Drawing.Color.Green;
            this.cmdHideLog.Location = new System.Drawing.Point(235, 0);
            this.cmdHideLog.Name = "cmdHideLog";
            this.cmdHideLog.Size = new System.Drawing.Size(34, 22);
            this.cmdHideLog.TabIndex = 11;
            this.cmdHideLog.Text = ">>";
            this.cmdHideLog.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdHideLog.UseVisualStyleBackColor = true;
            this.cmdHideLog.Click += new System.EventHandler(this.ShowHideLog);
            // 
            // txtCommand
            // 
            this.txtCommand.BackColor = System.Drawing.SystemColors.Info;
            this.txtCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCommand.BulletIndent = 5;
            this.txtCommand.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCommand.HideSelection = false;
            this.txtCommand.Location = new System.Drawing.Point(0, 22);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.ReadOnly = true;
            this.txtCommand.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtCommand.Size = new System.Drawing.Size(96, 58);
            this.txtCommand.TabIndex = 14;
            this.txtCommand.Text = "";
            // 
            // label1
            // 
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 22);
            this.label1.TabIndex = 13;
            this.label1.Text = "  Game log... ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.ShowHideLog);
            // 
            // txtInput
            // 
            this.txtInput.BackColor = System.Drawing.SystemColors.Window;
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtInput.Location = new System.Drawing.Point(0, 80);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(96, 20);
            this.txtInput.TabIndex = 12;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // lstServers
            // 
            this.lstServers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstServers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstServers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstServers.Font = new System.Drawing.Font("Garamond", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstServers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lstServers.FormattingEnabled = true;
            this.lstServers.ImageList = this.imageList2;
            this.lstServers.IntegralHeight = false;
            this.lstServers.ItemHeight = 34;
            this.lstServers.Location = new System.Drawing.Point(7, 7);
            this.lstServers.Name = "lstServers";
            this.lstServers.Size = new System.Drawing.Size(419, 375);
            this.lstServers.TabIndex = 11;
            this.lstServers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstServers_MouseDoubleClick);
            this.lstServers.SelectedIndexChanged += new System.EventHandler(this.lstServers_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 649);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(584, 447);
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "Bang!";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.Tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.ServersPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImageGirl)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel ServersPanel;
        private System.Windows.Forms.Button cmdDisconnect;
        private System.Windows.Forms.Button cmdConnect;
        private GameListBox lstServers;
        private System.Windows.Forms.Button cmdCloseGame;
        private System.Windows.Forms.Button cmdNewGame;
        private System.Windows.Forms.RichTextBox txtCommand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button cmdShowLog;
        private System.Windows.Forms.Button cmdHideLog;
        private System.Windows.Forms.PictureBox ImageGirl;
        private System.Windows.Forms.Panel LoginPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Button cmdLogin;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;

    }
}

