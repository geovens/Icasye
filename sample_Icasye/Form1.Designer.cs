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
			this.btStart = new System.Windows.Forms.Button();
			this.btStop = new System.Windows.Forms.Button();
			this.ImDavid = new System.Windows.Forms.RadioButton();
			this.ImTom = new System.Windows.Forms.RadioButton();
			this.TimerRun = new System.Windows.Forms.Timer(this.components);
			this.lbShow = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btStart
			// 
			this.btStart.Location = new System.Drawing.Point(15, 64);
			this.btStart.Name = "btStart";
			this.btStart.Size = new System.Drawing.Size(343, 40);
			this.btStart.TabIndex = 0;
			this.btStart.Text = "Go online and start to send random numbers to Tom!";
			this.btStart.UseVisualStyleBackColor = true;
			this.btStart.Click += new System.EventHandler(this.btStart_Click);
			// 
			// btStop
			// 
			this.btStop.Enabled = false;
			this.btStop.Location = new System.Drawing.Point(15, 110);
			this.btStop.Name = "btStop";
			this.btStop.Size = new System.Drawing.Size(343, 40);
			this.btStop.TabIndex = 0;
			this.btStop.Text = "Go offline and stop sending and receiving numbers";
			this.btStop.UseVisualStyleBackColor = true;
			this.btStop.Click += new System.EventHandler(this.btStop_Click);
			// 
			// ImDavid
			// 
			this.ImDavid.AutoSize = true;
			this.ImDavid.Font = new System.Drawing.Font("SimSun", 12F);
			this.ImDavid.Location = new System.Drawing.Point(85, 24);
			this.ImDavid.Name = "ImDavid";
			this.ImDavid.Size = new System.Drawing.Size(98, 20);
			this.ImDavid.TabIndex = 1;
			this.ImDavid.Text = "I\'m David";
			this.ImDavid.UseVisualStyleBackColor = true;
			this.ImDavid.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
			// 
			// ImTom
			// 
			this.ImTom.AutoSize = true;
			this.ImTom.Font = new System.Drawing.Font("SimSun", 12F);
			this.ImTom.Location = new System.Drawing.Point(205, 24);
			this.ImTom.Name = "ImTom";
			this.ImTom.Size = new System.Drawing.Size(82, 20);
			this.ImTom.TabIndex = 1;
			this.ImTom.Text = "I\'m Tom";
			this.ImTom.UseVisualStyleBackColor = true;
			this.ImTom.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
			// 
			// TimerRun
			// 
			this.TimerRun.Enabled = true;
			this.TimerRun.Interval = 1000;
			this.TimerRun.Tick += new System.EventHandler(this.TimerRun_Tick);
			// 
			// lbShow
			// 
			this.lbShow.AutoSize = true;
			this.lbShow.Font = new System.Drawing.Font("SimSun", 12F);
			this.lbShow.Location = new System.Drawing.Point(65, 180);
			this.lbShow.Name = "lbShow";
			this.lbShow.Size = new System.Drawing.Size(160, 16);
			this.lbShow.TabIndex = 3;
			this.lbShow.Text = "Numbers I received:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(370, 233);
			this.Controls.Add(this.lbShow);
			this.Controls.Add(this.ImTom);
			this.Controls.Add(this.ImDavid);
			this.Controls.Add(this.btStop);
			this.Controls.Add(this.btStart);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btStart;
		private System.Windows.Forms.Button btStop;
		private System.Windows.Forms.RadioButton ImDavid;
		private System.Windows.Forms.RadioButton ImTom;
		private System.Windows.Forms.Timer TimerRun;
		private System.Windows.Forms.Label lbShow;
	}
}

