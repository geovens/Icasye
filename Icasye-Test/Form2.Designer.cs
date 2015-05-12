namespace Icasye_Test
{
	partial class Form2
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tb1Address = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.bt1Add = new System.Windows.Forms.Button();
			this.tb2Name = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tb2Address = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tb2Port = new System.Windows.Forms.TextBox();
			this.bt2Add = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.bt2Add);
			this.groupBox1.Controls.Add(this.tb2Port);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.tb2Address);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.tb2Name);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(12, 97);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(214, 134);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Or Add a General TCP Connection";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.bt1Add);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.tb1Address);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(214, 79);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Add a Remote Icasye Client";
			// 
			// tb1Address
			// 
			this.tb1Address.Location = new System.Drawing.Point(80, 20);
			this.tb1Address.Name = "tb1Address";
			this.tb1Address.Size = new System.Drawing.Size(128, 21);
			this.tb1Address.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "Address:";
			// 
			// bt1Add
			// 
			this.bt1Add.Location = new System.Drawing.Point(80, 47);
			this.bt1Add.Name = "bt1Add";
			this.bt1Add.Size = new System.Drawing.Size(128, 23);
			this.bt1Add.TabIndex = 2;
			this.bt1Add.Text = "Add";
			this.bt1Add.UseVisualStyleBackColor = true;
			this.bt1Add.Click += new System.EventHandler(this.bt1Add_Click);
			// 
			// tb2Name
			// 
			this.tb2Name.Location = new System.Drawing.Point(80, 20);
			this.tb2Name.Name = "tb2Name";
			this.tb2Name.Size = new System.Drawing.Size(128, 21);
			this.tb2Name.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "Name:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 50);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 1;
			this.label3.Text = "Address:";
			// 
			// tb2Address
			// 
			this.tb2Address.Location = new System.Drawing.Point(80, 47);
			this.tb2Address.Name = "tb2Address";
			this.tb2Address.Size = new System.Drawing.Size(128, 21);
			this.tb2Address.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(21, 77);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 12);
			this.label4.TabIndex = 1;
			this.label4.Text = "Port:";
			// 
			// tb2Port
			// 
			this.tb2Port.Location = new System.Drawing.Point(80, 74);
			this.tb2Port.Name = "tb2Port";
			this.tb2Port.Size = new System.Drawing.Size(128, 21);
			this.tb2Port.TabIndex = 0;
			// 
			// bt2Add
			// 
			this.bt2Add.Location = new System.Drawing.Point(80, 101);
			this.bt2Add.Name = "bt2Add";
			this.bt2Add.Size = new System.Drawing.Size(128, 23);
			this.bt2Add.TabIndex = 2;
			this.bt2Add.Text = "Add";
			this.bt2Add.UseVisualStyleBackColor = true;
			this.bt2Add.Click += new System.EventHandler(this.bt2Add_Click);
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(238, 242);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.MaximizeBox = false;
			this.Name = "Form2";
			this.Text = "Add a Client";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button bt1Add;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tb1Address;
		private System.Windows.Forms.Button bt2Add;
		private System.Windows.Forms.TextBox tb2Port;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tb2Address;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tb2Name;
		private System.Windows.Forms.Label label2;
	}
}