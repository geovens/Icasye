namespace Icasye_Test
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
			this.btAdd = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.btGoOnline = new System.Windows.Forms.Button();
			this.btGoOffline = new System.Windows.Forms.Button();
			this.lbClients = new System.Windows.Forms.ListBox();
			this.tbMessages = new System.Windows.Forms.TextBox();
			this.tbSend = new System.Windows.Forms.TextBox();
			this.btSend = new System.Windows.Forms.Button();
			this.tbMyName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btBlock = new System.Windows.Forms.Button();
			this.btFavorite = new System.Windows.Forms.Button();
			this.btUnblock = new System.Windows.Forms.Button();
			this.btDelete = new System.Windows.Forms.Button();
			this.cbN = new System.Windows.Forms.CheckBox();
			this.tbMessage = new System.Windows.Forms.TextBox();
			this.cbR = new System.Windows.Forms.CheckBox();
			this.btClear = new System.Windows.Forms.Button();
			this.tbContain = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btPause = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btAdd
			// 
			this.btAdd.Location = new System.Drawing.Point(33, 295);
			this.btAdd.Name = "btAdd";
			this.btAdd.Size = new System.Drawing.Size(155, 23);
			this.btAdd.TabIndex = 0;
			this.btAdd.Text = "Add a Client Manually";
			this.btAdd.UseVisualStyleBackColor = true;
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// btGoOnline
			// 
			this.btGoOnline.Location = new System.Drawing.Point(11, 39);
			this.btGoOnline.Name = "btGoOnline";
			this.btGoOnline.Size = new System.Drawing.Size(97, 23);
			this.btGoOnline.TabIndex = 3;
			this.btGoOnline.Text = "Go Online";
			this.btGoOnline.UseVisualStyleBackColor = true;
			this.btGoOnline.Click += new System.EventHandler(this.btGoOnline_Click);
			// 
			// btGoOffline
			// 
			this.btGoOffline.Enabled = false;
			this.btGoOffline.Location = new System.Drawing.Point(114, 39);
			this.btGoOffline.Name = "btGoOffline";
			this.btGoOffline.Size = new System.Drawing.Size(97, 23);
			this.btGoOffline.TabIndex = 3;
			this.btGoOffline.Text = "Go Offline";
			this.btGoOffline.UseVisualStyleBackColor = true;
			this.btGoOffline.Click += new System.EventHandler(this.btGoOffline_Click);
			// 
			// lbClients
			// 
			this.lbClients.FormattingEnabled = true;
			this.lbClients.ItemHeight = 12;
			this.lbClients.Location = new System.Drawing.Point(12, 94);
			this.lbClients.Name = "lbClients";
			this.lbClients.Size = new System.Drawing.Size(200, 136);
			this.lbClients.TabIndex = 4;
			this.lbClients.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbClients_MouseUp);
			// 
			// tbMessages
			// 
			this.tbMessages.Location = new System.Drawing.Point(227, 39);
			this.tbMessages.Multiline = true;
			this.tbMessages.Name = "tbMessages";
			this.tbMessages.ReadOnly = true;
			this.tbMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbMessages.Size = new System.Drawing.Size(291, 221);
			this.tbMessages.TabIndex = 5;
			this.tbMessages.WordWrap = false;
			this.tbMessages.Click += new System.EventHandler(this.tbMessages_Click);
			// 
			// tbSend
			// 
			this.tbSend.Location = new System.Drawing.Point(227, 296);
			this.tbSend.Name = "tbSend";
			this.tbSend.Size = new System.Drawing.Size(287, 21);
			this.tbSend.TabIndex = 6;
			// 
			// btSend
			// 
			this.btSend.Location = new System.Drawing.Point(600, 294);
			this.btSend.Name = "btSend";
			this.btSend.Size = new System.Drawing.Size(76, 23);
			this.btSend.TabIndex = 7;
			this.btSend.Text = "Send";
			this.btSend.UseVisualStyleBackColor = true;
			this.btSend.Click += new System.EventHandler(this.btSend_Click);
			// 
			// tbMyName
			// 
			this.tbMyName.Location = new System.Drawing.Point(73, 12);
			this.tbMyName.Name = "tbMyName";
			this.tbMyName.Size = new System.Drawing.Size(138, 21);
			this.tbMyName.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 9;
			this.label1.Text = "My Name:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(95, 12);
			this.label2.TabIndex = 9;
			this.label2.Text = "Clients Online:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(227, 17);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(83, 12);
			this.label3.TabIndex = 10;
			this.label3.Text = "TCP Data Log:";
			// 
			// btBlock
			// 
			this.btBlock.Location = new System.Drawing.Point(16, 237);
			this.btBlock.Name = "btBlock";
			this.btBlock.Size = new System.Drawing.Size(91, 23);
			this.btBlock.TabIndex = 11;
			this.btBlock.Text = "Block";
			this.btBlock.UseVisualStyleBackColor = true;
			this.btBlock.Click += new System.EventHandler(this.btBlock_Click);
			// 
			// btFavorite
			// 
			this.btFavorite.Location = new System.Drawing.Point(113, 266);
			this.btFavorite.Name = "btFavorite";
			this.btFavorite.Size = new System.Drawing.Size(91, 23);
			this.btFavorite.TabIndex = 11;
			this.btFavorite.Text = "Set Favorite";
			this.btFavorite.UseVisualStyleBackColor = true;
			this.btFavorite.Click += new System.EventHandler(this.btFavorite_Click);
			// 
			// btUnblock
			// 
			this.btUnblock.Location = new System.Drawing.Point(113, 237);
			this.btUnblock.Name = "btUnblock";
			this.btUnblock.Size = new System.Drawing.Size(91, 23);
			this.btUnblock.TabIndex = 12;
			this.btUnblock.Text = "Unblock All";
			this.btUnblock.UseVisualStyleBackColor = true;
			this.btUnblock.Click += new System.EventHandler(this.btUnblock_Click);
			// 
			// btDelete
			// 
			this.btDelete.Location = new System.Drawing.Point(16, 266);
			this.btDelete.Name = "btDelete";
			this.btDelete.Size = new System.Drawing.Size(91, 23);
			this.btDelete.TabIndex = 13;
			this.btDelete.Text = "Delete";
			this.btDelete.UseVisualStyleBackColor = true;
			this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
			// 
			// cbN
			// 
			this.cbN.AutoSize = true;
			this.cbN.Location = new System.Drawing.Point(558, 299);
			this.cbN.Name = "cbN";
			this.cbN.Size = new System.Drawing.Size(36, 16);
			this.cbN.TabIndex = 14;
			this.cbN.Text = "\\n";
			this.cbN.UseVisualStyleBackColor = true;
			// 
			// tbMessage
			// 
			this.tbMessage.Location = new System.Drawing.Point(524, 39);
			this.tbMessage.Multiline = true;
			this.tbMessage.Name = "tbMessage";
			this.tbMessage.ReadOnly = true;
			this.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbMessage.Size = new System.Drawing.Size(152, 221);
			this.tbMessage.TabIndex = 5;
			this.tbMessage.WordWrap = false;
			// 
			// cbR
			// 
			this.cbR.AutoSize = true;
			this.cbR.Location = new System.Drawing.Point(524, 299);
			this.cbR.Name = "cbR";
			this.cbR.Size = new System.Drawing.Size(36, 16);
			this.cbR.TabIndex = 14;
			this.cbR.Text = "\\r";
			this.cbR.UseVisualStyleBackColor = true;
			// 
			// btClear
			// 
			this.btClear.Location = new System.Drawing.Point(600, 10);
			this.btClear.Name = "btClear";
			this.btClear.Size = new System.Drawing.Size(76, 23);
			this.btClear.TabIndex = 15;
			this.btClear.Text = "Clear";
			this.btClear.UseVisualStyleBackColor = true;
			this.btClear.Click += new System.EventHandler(this.btClear_Click);
			// 
			// tbContain
			// 
			this.tbContain.Location = new System.Drawing.Point(346, 268);
			this.tbContain.Name = "tbContain";
			this.tbContain.Size = new System.Drawing.Size(168, 21);
			this.tbContain.TabIndex = 16;
			this.tbContain.TextChanged += new System.EventHandler(this.tbContain_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(227, 271);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(113, 12);
			this.label4.TabIndex = 17;
			this.label4.Text = "Filter: containing";
			// 
			// btPause
			// 
			this.btPause.Location = new System.Drawing.Point(519, 10);
			this.btPause.Name = "btPause";
			this.btPause.Size = new System.Drawing.Size(75, 23);
			this.btPause.TabIndex = 18;
			this.btPause.Text = "Pause";
			this.btPause.UseVisualStyleBackColor = true;
			this.btPause.Click += new System.EventHandler(this.btPause_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(695, 334);
			this.Controls.Add(this.btPause);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tbContain);
			this.Controls.Add(this.btClear);
			this.Controls.Add(this.cbN);
			this.Controls.Add(this.cbR);
			this.Controls.Add(this.btDelete);
			this.Controls.Add(this.btUnblock);
			this.Controls.Add(this.btFavorite);
			this.Controls.Add(this.btBlock);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbMyName);
			this.Controls.Add(this.btSend);
			this.Controls.Add(this.tbSend);
			this.Controls.Add(this.tbMessage);
			this.Controls.Add(this.tbMessages);
			this.Controls.Add(this.lbClients);
			this.Controls.Add(this.btGoOffline);
			this.Controls.Add(this.btGoOnline);
			this.Controls.Add(this.btAdd);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Icasye.dll Test";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btAdd;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button btGoOnline;
		private System.Windows.Forms.Button btGoOffline;
		private System.Windows.Forms.ListBox lbClients;
		private System.Windows.Forms.TextBox tbMessages;
		private System.Windows.Forms.TextBox tbSend;
		private System.Windows.Forms.Button btSend;
		private System.Windows.Forms.TextBox tbMyName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btBlock;
		private System.Windows.Forms.Button btFavorite;
		private System.Windows.Forms.Button btUnblock;
		private System.Windows.Forms.Button btDelete;
		private System.Windows.Forms.CheckBox cbN;
		private System.Windows.Forms.TextBox tbMessage;
		private System.Windows.Forms.CheckBox cbR;
		private System.Windows.Forms.Button btClear;
		private System.Windows.Forms.TextBox tbContain;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btPause;
	}
}

