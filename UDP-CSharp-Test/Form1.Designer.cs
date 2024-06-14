namespace UDP_CSharp_Test
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
			this.btListenStart = new System.Windows.Forms.Button();
			this.btDestSend = new System.Windows.Forms.Button();
			this.tbListenPort = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tbDestIP = new System.Windows.Forms.TextBox();
			this.tbDestPort = new System.Windows.Forms.TextBox();
			this.tbDestMsg = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.lbStatus = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.lbListenReceived = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btListenStart
			// 
			this.btListenStart.Location = new System.Drawing.Point(22, 63);
			this.btListenStart.Margin = new System.Windows.Forms.Padding(4);
			this.btListenStart.Name = "btListenStart";
			this.btListenStart.Size = new System.Drawing.Size(100, 31);
			this.btListenStart.TabIndex = 0;
			this.btListenStart.Text = "Start Listen";
			this.btListenStart.UseVisualStyleBackColor = true;
			this.btListenStart.Click += new System.EventHandler(this.button1_Click);
			// 
			// btDestSend
			// 
			this.btDestSend.Location = new System.Drawing.Point(25, 119);
			this.btDestSend.Margin = new System.Windows.Forms.Padding(4);
			this.btDestSend.Name = "btDestSend";
			this.btDestSend.Size = new System.Drawing.Size(100, 31);
			this.btDestSend.TabIndex = 1;
			this.btDestSend.Text = "Send";
			this.btDestSend.UseVisualStyleBackColor = true;
			this.btDestSend.Click += new System.EventHandler(this.button2_Click);
			// 
			// tbListenPort
			// 
			this.tbListenPort.Location = new System.Drawing.Point(63, 34);
			this.tbListenPort.Name = "tbListenPort";
			this.tbListenPort.Size = new System.Drawing.Size(100, 22);
			this.tbListenPort.TabIndex = 2;
			this.tbListenPort.Text = "5001";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "Port:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Port:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(22, 34);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 17);
			this.label3.TabIndex = 3;
			this.label3.Text = "IP Address:";
			// 
			// tbDestIP
			// 
			this.tbDestIP.Location = new System.Drawing.Point(108, 31);
			this.tbDestIP.Name = "tbDestIP";
			this.tbDestIP.Size = new System.Drawing.Size(139, 22);
			this.tbDestIP.TabIndex = 2;
			this.tbDestIP.Text = "127.0.0.1";
			// 
			// tbDestPort
			// 
			this.tbDestPort.Location = new System.Drawing.Point(108, 59);
			this.tbDestPort.Name = "tbDestPort";
			this.tbDestPort.Size = new System.Drawing.Size(100, 22);
			this.tbDestPort.TabIndex = 2;
			this.tbDestPort.Text = "5001";
			// 
			// tbDestMsg
			// 
			this.tbDestMsg.Location = new System.Drawing.Point(108, 87);
			this.tbDestMsg.Name = "tbDestMsg";
			this.tbDestMsg.Size = new System.Drawing.Size(100, 22);
			this.tbDestMsg.TabIndex = 2;
			this.tbDestMsg.Text = "hello!";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(22, 90);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(69, 17);
			this.label4.TabIndex = 3;
			this.label4.Text = "Message:";
			// 
			// lbStatus
			// 
			this.lbStatus.AutoSize = true;
			this.lbStatus.Location = new System.Drawing.Point(30, 351);
			this.lbStatus.Name = "lbStatus";
			this.lbStatus.Size = new System.Drawing.Size(20, 17);
			this.lbStatus.TabIndex = 4;
			this.lbStatus.Text = "   ";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(19, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(141, 17);
			this.label5.TabIndex = 3;
			this.label5.Text = "Latest received data:";
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// lbListenReceived
			// 
			this.lbListenReceived.AutoSize = true;
			this.lbListenReceived.Location = new System.Drawing.Point(166, 104);
			this.lbListenReceived.Name = "lbListenReceived";
			this.lbListenReceived.Size = new System.Drawing.Size(20, 17);
			this.lbListenReceived.TabIndex = 4;
			this.lbListenReceived.Text = "   ";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tbListenPort);
			this.groupBox1.Controls.Add(this.lbListenReceived);
			this.groupBox1.Controls.Add(this.btListenStart);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new System.Drawing.Point(33, 31);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(334, 136);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Listen";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.btDestSend);
			this.groupBox2.Controls.Add(this.tbDestPort);
			this.groupBox2.Controls.Add(this.tbDestMsg);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.tbDestIP);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(33, 190);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(334, 178);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Send";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 387);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lbStatus);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.Text = "UDP Test";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btListenStart;
		private System.Windows.Forms.Button btDestSend;
		private System.Windows.Forms.TextBox tbListenPort;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbDestIP;
		private System.Windows.Forms.TextBox tbDestPort;
		private System.Windows.Forms.TextBox tbDestMsg;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lbStatus;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label lbListenReceived;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
	}
}

