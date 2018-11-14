namespace WindowsFormsApplication1
{
	partial class Form1
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
			this.btAddConnect = new System.Windows.Forms.Button();
			this.btSend = new System.Windows.Forms.Button();
			this.tbSend = new System.Windows.Forms.TextBox();
			this.btDisconnect = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btFavorite = new System.Windows.Forms.Button();
			this.btRenameConnection = new System.Windows.Forms.Button();
			this.lbConnections = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tbConnectName = new System.Windows.Forms.TextBox();
			this.tbConnectPort = new System.Windows.Forms.TextBox();
			this.tbConnectAddress = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btRemoveListen = new System.Windows.Forms.Button();
			this.lbListeners = new System.Windows.Forms.ListBox();
			this.btAddListen = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.tbListenPort = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tbListeneName = new System.Windows.Forms.TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.tbMessages = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabTCPLog = new System.Windows.Forms.TabPage();
			this.tabStatusLog = new System.Windows.Forms.TabPage();
			this.tbStatusLog = new System.Windows.Forms.TextBox();
			this.btPause = new System.Windows.Forms.Button();
			this.btClear = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabTCPLog.SuspendLayout();
			this.tabStatusLog.SuspendLayout();
			this.SuspendLayout();
			// 
			// btAddConnect
			// 
			this.btAddConnect.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btAddConnect.Location = new System.Drawing.Point(15, 347);
			this.btAddConnect.Name = "btAddConnect";
			this.btAddConnect.Size = new System.Drawing.Size(150, 33);
			this.btAddConnect.TabIndex = 0;
			this.btAddConnect.Text = "Add Connection";
			this.btAddConnect.UseVisualStyleBackColor = true;
			this.btAddConnect.Click += new System.EventHandler(this.btConnect_Click);
			// 
			// btSend
			// 
			this.btSend.Location = new System.Drawing.Point(340, 326);
			this.btSend.Name = "btSend";
			this.btSend.Size = new System.Drawing.Size(88, 33);
			this.btSend.TabIndex = 1;
			this.btSend.Text = "Send";
			this.btSend.UseVisualStyleBackColor = true;
			this.btSend.Click += new System.EventHandler(this.btSend_Click);
			// 
			// tbSend
			// 
			this.tbSend.Location = new System.Drawing.Point(6, 330);
			this.tbSend.Name = "tbSend";
			this.tbSend.Size = new System.Drawing.Size(324, 20);
			this.tbSend.TabIndex = 2;
			this.tbSend.Text = "hello!";
			// 
			// btDisconnect
			// 
			this.btDisconnect.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btDisconnect.Location = new System.Drawing.Point(15, 177);
			this.btDisconnect.Name = "btDisconnect";
			this.btDisconnect.Size = new System.Drawing.Size(150, 33);
			this.btDisconnect.TabIndex = 3;
			this.btDisconnect.Text = "Disconnect";
			this.btDisconnect.UseVisualStyleBackColor = true;
			this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btFavorite);
			this.groupBox1.Controls.Add(this.btRenameConnection);
			this.groupBox1.Controls.Add(this.lbConnections);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.btDisconnect);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.tbConnectName);
			this.groupBox1.Controls.Add(this.tbConnectPort);
			this.groupBox1.Controls.Add(this.btAddConnect);
			this.groupBox1.Controls.Add(this.tbConnectAddress);
			this.groupBox1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.groupBox1.Location = new System.Drawing.Point(211, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(187, 391);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connections";
			// 
			// btFavorite
			// 
			this.btFavorite.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btFavorite.Location = new System.Drawing.Point(90, 213);
			this.btFavorite.Name = "btFavorite";
			this.btFavorite.Size = new System.Drawing.Size(75, 33);
			this.btFavorite.TabIndex = 9;
			this.btFavorite.Text = "Favorite";
			this.btFavorite.UseVisualStyleBackColor = true;
			this.btFavorite.Click += new System.EventHandler(this.btFavorite_Click);
			// 
			// btRenameConnection
			// 
			this.btRenameConnection.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btRenameConnection.Location = new System.Drawing.Point(15, 213);
			this.btRenameConnection.Name = "btRenameConnection";
			this.btRenameConnection.Size = new System.Drawing.Size(75, 33);
			this.btRenameConnection.TabIndex = 8;
			this.btRenameConnection.Text = "Rename";
			this.btRenameConnection.UseVisualStyleBackColor = true;
			this.btRenameConnection.Click += new System.EventHandler(this.btRenameConnection_Click);
			// 
			// lbConnections
			// 
			this.lbConnections.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lbConnections.FormattingEnabled = true;
			this.lbConnections.ItemHeight = 12;
			this.lbConnections.Location = new System.Drawing.Point(15, 31);
			this.lbConnections.Name = "lbConnections";
			this.lbConnections.Size = new System.Drawing.Size(150, 136);
			this.lbConnections.TabIndex = 7;
			this.lbConnections.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbConnections_MouseUp);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(13, 321);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 12);
			this.label3.TabIndex = 3;
			this.label3.Text = "Port:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(13, 291);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "Addr.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(13, 262);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "Name:";
			// 
			// tbConnectName
			// 
			this.tbConnectName.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbConnectName.Location = new System.Drawing.Point(54, 259);
			this.tbConnectName.Name = "tbConnectName";
			this.tbConnectName.Size = new System.Drawing.Size(111, 21);
			this.tbConnectName.TabIndex = 2;
			// 
			// tbConnectPort
			// 
			this.tbConnectPort.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbConnectPort.Location = new System.Drawing.Point(54, 317);
			this.tbConnectPort.Name = "tbConnectPort";
			this.tbConnectPort.Size = new System.Drawing.Size(111, 21);
			this.tbConnectPort.TabIndex = 1;
			this.tbConnectPort.Text = "5001";
			// 
			// tbConnectAddress
			// 
			this.tbConnectAddress.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbConnectAddress.Location = new System.Drawing.Point(54, 288);
			this.tbConnectAddress.Name = "tbConnectAddress";
			this.tbConnectAddress.Size = new System.Drawing.Size(111, 21);
			this.tbConnectAddress.TabIndex = 1;
			this.tbConnectAddress.Text = "127.0.0.1";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btRemoveListen);
			this.groupBox2.Controls.Add(this.lbListeners);
			this.groupBox2.Controls.Add(this.btAddListen);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.tbListenPort);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.tbListeneName);
			this.groupBox2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.groupBox2.Location = new System.Drawing.Point(21, 13);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(184, 391);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Listeners";
			// 
			// btRemoveListen
			// 
			this.btRemoveListen.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btRemoveListen.Location = new System.Drawing.Point(15, 230);
			this.btRemoveListen.Name = "btRemoveListen";
			this.btRemoveListen.Size = new System.Drawing.Size(150, 33);
			this.btRemoveListen.TabIndex = 8;
			this.btRemoveListen.Text = "Stop Listen";
			this.btRemoveListen.UseVisualStyleBackColor = true;
			this.btRemoveListen.Click += new System.EventHandler(this.btRemoveListen_Click);
			// 
			// lbListeners
			// 
			this.lbListeners.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lbListeners.FormattingEnabled = true;
			this.lbListeners.ItemHeight = 12;
			this.lbListeners.Location = new System.Drawing.Point(15, 31);
			this.lbListeners.Name = "lbListeners";
			this.lbListeners.Size = new System.Drawing.Size(150, 196);
			this.lbListeners.TabIndex = 7;
			// 
			// btAddListen
			// 
			this.btAddListen.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btAddListen.Location = new System.Drawing.Point(15, 345);
			this.btAddListen.Name = "btAddListen";
			this.btAddListen.Size = new System.Drawing.Size(150, 33);
			this.btAddListen.TabIndex = 4;
			this.btAddListen.Text = "Start Listen";
			this.btAddListen.UseVisualStyleBackColor = true;
			this.btAddListen.Click += new System.EventHandler(this.btAddListen_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(13, 316);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 12);
			this.label4.TabIndex = 3;
			this.label4.Text = "Port:";
			// 
			// tbListenPort
			// 
			this.tbListenPort.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbListenPort.Location = new System.Drawing.Point(54, 313);
			this.tbListenPort.Name = "tbListenPort";
			this.tbListenPort.Size = new System.Drawing.Size(111, 21);
			this.tbListenPort.TabIndex = 0;
			this.tbListenPort.Text = "5001";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label5.Location = new System.Drawing.Point(13, 287);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(35, 12);
			this.label5.TabIndex = 3;
			this.label5.Text = "Name:";
			// 
			// tbListeneName
			// 
			this.tbListeneName.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbListeneName.Location = new System.Drawing.Point(54, 284);
			this.tbListeneName.Name = "tbListeneName";
			this.tbListeneName.Size = new System.Drawing.Size(111, 21);
			this.tbListeneName.TabIndex = 2;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 200;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// tbMessages
			// 
			this.tbMessages.Location = new System.Drawing.Point(6, 8);
			this.tbMessages.Multiline = true;
			this.tbMessages.Name = "tbMessages";
			this.tbMessages.ReadOnly = true;
			this.tbMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbMessages.Size = new System.Drawing.Size(419, 312);
			this.tbMessages.TabIndex = 7;
			this.tbMessages.WordWrap = false;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabTCPLog);
			this.tabControl1.Controls.Add(this.tabStatusLog);
			this.tabControl1.Location = new System.Drawing.Point(419, 13);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(439, 391);
			this.tabControl1.TabIndex = 9;
			// 
			// tabTCPLog
			// 
			this.tabTCPLog.BackColor = System.Drawing.SystemColors.Control;
			this.tabTCPLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.tabTCPLog.Controls.Add(this.btSend);
			this.tabTCPLog.Controls.Add(this.tbMessages);
			this.tabTCPLog.Controls.Add(this.tbSend);
			this.tabTCPLog.Location = new System.Drawing.Point(4, 22);
			this.tabTCPLog.Name = "tabTCPLog";
			this.tabTCPLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabTCPLog.Size = new System.Drawing.Size(431, 365);
			this.tabTCPLog.TabIndex = 1;
			this.tabTCPLog.Text = "TCP Data";
			// 
			// tabStatusLog
			// 
			this.tabStatusLog.BackColor = System.Drawing.SystemColors.Control;
			this.tabStatusLog.Controls.Add(this.tbStatusLog);
			this.tabStatusLog.Location = new System.Drawing.Point(4, 22);
			this.tabStatusLog.Name = "tabStatusLog";
			this.tabStatusLog.Size = new System.Drawing.Size(431, 365);
			this.tabStatusLog.TabIndex = 2;
			this.tabStatusLog.Text = "Status Log";
			// 
			// tbStatusLog
			// 
			this.tbStatusLog.Location = new System.Drawing.Point(6, 8);
			this.tbStatusLog.Multiline = true;
			this.tbStatusLog.Name = "tbStatusLog";
			this.tbStatusLog.ReadOnly = true;
			this.tbStatusLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbStatusLog.Size = new System.Drawing.Size(419, 345);
			this.tbStatusLog.TabIndex = 8;
			this.tbStatusLog.WordWrap = false;
			// 
			// btPause
			// 
			this.btPause.Location = new System.Drawing.Point(680, 7);
			this.btPause.Name = "btPause";
			this.btPause.Size = new System.Drawing.Size(75, 22);
			this.btPause.TabIndex = 19;
			this.btPause.Text = "Pause";
			this.btPause.UseVisualStyleBackColor = true;
			this.btPause.Click += new System.EventHandler(this.btPause_Click);
			// 
			// btClear
			// 
			this.btClear.Location = new System.Drawing.Point(763, 7);
			this.btClear.Name = "btClear";
			this.btClear.Size = new System.Drawing.Size(75, 22);
			this.btClear.TabIndex = 20;
			this.btClear.Text = "Clear";
			this.btClear.UseVisualStyleBackColor = true;
			this.btClear.Click += new System.EventHandler(this.btClear_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(870, 437);
			this.Controls.Add(this.btClear);
			this.Controls.Add(this.btPause);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "Form1";
			this.Text = "TCPCSharp.dll Test";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabTCPLog.ResumeLayout(false);
			this.tabTCPLog.PerformLayout();
			this.tabStatusLog.ResumeLayout(false);
			this.tabStatusLog.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btAddConnect;
		private System.Windows.Forms.Button btSend;
		private System.Windows.Forms.TextBox tbSend;
		private System.Windows.Forms.Button btDisconnect;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbConnectName;
		private System.Windows.Forms.TextBox tbConnectPort;
		private System.Windows.Forms.TextBox tbConnectAddress;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbListenPort;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbListeneName;
		private System.Windows.Forms.Button btAddListen;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ListBox lbConnections;
		private System.Windows.Forms.Button btRemoveListen;
		private System.Windows.Forms.ListBox lbListeners;
		private System.Windows.Forms.TextBox tbMessages;
		private System.Windows.Forms.Button btRenameConnection;
		private System.Windows.Forms.Button btFavorite;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabTCPLog;
		private System.Windows.Forms.TabPage tabStatusLog;
		private System.Windows.Forms.TextBox tbStatusLog;
		private System.Windows.Forms.Button btPause;
		private System.Windows.Forms.Button btClear;
	}
}

