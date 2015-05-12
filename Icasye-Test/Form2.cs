using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Icasye_Test
{
	public partial class Form2 : Form
	{
		Form1 Owner;

		public Form2(Form1 owner)
		{
			InitializeComponent();
			Owner = owner;
		}

		private void bt1Add_Click(object sender, EventArgs e)
		{
			Owner.IcasyeClient.AddIcasyeClientManually(tb1Address.Text);
			this.Close();
		}

		private void bt2Add_Click(object sender, EventArgs e)
		{
			int port = 0;
			if (int.TryParse(tb2Port.Text, out port))
			{
				Owner.IcasyeClient.AddGeneralConnectionManually(tb2Name.Text, tb2Address.Text, port);
				this.Close();
			}
		}
	}
}
