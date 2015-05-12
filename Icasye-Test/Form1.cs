using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices; 
using Icasye;

namespace Icasye_Test
{
	public partial class Form1 : Form
	{
		public IcasyeClient IcasyeClient = new IcasyeClient();

		public Form1()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			IcasyeClient.OnMessageGot += OnMessageGot;
		}
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			IcasyeClient.GoOffline();
		}

		public void OnMessageGot(string srcname, IcasyeMsg msg)
		{
			AddTCPLog(srcname, "R", msg);
		}
		public void OnFavoriteMessageGot(string srcname, IcasyeMsg message)
		{
			// to let the window flash
			FlashWindowFlag = true;
		}

		private void btGoOnline_Click(object sender, EventArgs e)
		{
			IcasyeClient.SetToPrintLog();
			IcasyeClient.SetMyName(tbMyName.Text);
			IcasyeClient.GoOnline();

			tbMyName.ReadOnly = true;
			btGoOffline.Enabled = true;
			btGoOnline.Enabled = false;
		}
		private void btGoOffline_Click(object sender, EventArgs e)
		{
			IcasyeClient.GoOffline();

			tbMyName.ReadOnly = false;
			btGoOffline.Enabled = false;
			btGoOnline.Enabled = true;

			Logs.Clear();
		}
		private void btAdd_Click(object sender, EventArgs e)
		{
			Form2 form2 = new Form2(this);
			form2.Show();
		}
		private void btSend_Click(object sender, EventArgs e)
		{
			if (lbClients.SelectedIndex == -1)
				return;
			string destname = lbClients.SelectedItem.ToString();

			IcasyeMsg message = new IcasyeMsg();
			message.SetString(tbSend.Text);
			if (cbR.Checked)
				message.AppendString("\r");
			if (cbN.Checked)
				message.AppendString("\n");

			bool suc = IcasyeClient.Send(destname, message);
	
			if (suc)
				AddTCPLog(destname, "S", message);
		}
		private void btBlock_Click(object sender, EventArgs e)
		{
			if (lbClients.SelectedIndex != -1)
			{
				BlockedList.Add(lbClients.SelectedItem.ToString());
				IcasyeClient.Block(lbClients.SelectedItem.ToString());
			}
		}
		private void btUnblock_Click(object sender, EventArgs e)
		{
			foreach (string name in BlockedList)
			{
				IcasyeClient.UnBlock(name);
			}
			BlockedList.Clear();
		}
		private void btDelete_Click(object sender, EventArgs e)
		{
			if (lbClients.SelectedIndex != -1)
			{
				IcasyeClient.DeleteManuallyAdded(lbClients.SelectedItem.ToString());
			}
		}
		private void btFavorite_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < lbClients.Items.Count; i++)
				if (lbClients.SelectedIndex == i)
					IcasyeClient.SetSpecificMessageGotEvent(lbClients.Items[i].ToString(), OnFavoriteMessageGot);
				else
					IcasyeClient.SetSpecificMessageGotEvent(lbClients.Items[i].ToString(), null);
		}



		bool FlashWindowFlag = false;
		[DllImport("user32.dll")]
		static extern void FlashWindow(IntPtr a, bool b); 
		private void timer1_Tick(object sender, EventArgs e)
		{
			// renew the client list
			string selected = "";
			if (lbClients.SelectedIndex != -1)
				selected = lbClients.SelectedItem.ToString();
			List<string> clientlist = IcasyeClient.GetClientList();
			lbClients.Items.Clear();
			foreach (string client in clientlist)
				lbClients.Items.Add(client);
			for (int i = 0; i < lbClients.Items.Count; i++)
			{
				if (lbClients.Items[i].ToString() == selected)
					lbClients.SelectedIndex = i;
			}

			// renew the log textbox if needed
			if (lbClients.SelectedIndex != -1)
			{
				selected = lbClients.SelectedItem.ToString();
				if (TCPLogRefreshFlag)
				{
					tbMessages.Text = GetTCPLog(selected, tbContain.Text, 0);
					tbMessages.Select(tbMessages.TextLength, 0);
					tbMessages.ScrollToCaret();
					TCPLogRefreshFlag = false;
					TCPLogName = lbClients.SelectedItem.ToString();
				}
			}

			// if need to flash window
			if (FlashWindowFlag)
			{
				FlashWindow(this.Handle, true);
				FlashWindowFlag = false;
			}
		}

		public List<Log> Logs = new List<Log>();
		public bool TCPLogRefreshFlag = false;
		public string TCPLogName;
		public bool Paused = false;
		public List<string> BlockedList = new List<string>();

		private void AddTCPLog(string name, string type, IcasyeMsg msg)
		{
			if (Paused)
				return;

			bool found = false; ;
			for (int i = 0; i < Logs.Count; i++)
			{
				if (Logs[i].ClientName == name)
				{
					Logs[i].AddMessage(type, msg);
					found = true;
				}
			}
			if (!found)
			{
				Log aLog = new Log();
				aLog.ClientName = name;
				Logs.Add(aLog);
				aLog.AddMessage(type, msg);
			}
			TCPLogRefreshFlag = true;
		}
		private string GetTCPLog(string name, string contain, int count)
		{
			for (int i = 0; i < Logs.Count; i++)
			{
				if (Logs[i].ClientName == name)
				{
					Logs[i].CurrentContain = contain;
					return Logs[i].GetTextMessages(count);
				}
			}
			return "";
		}

		private void btClear_Click(object sender, EventArgs e)
		{
			if (lbClients.SelectedIndex == -1)
				return;

			string selected = lbClients.SelectedItem.ToString();
			for (int i = 0; i < Logs.Count; i++)
			{
				if (Logs[i].ClientName == selected)
				{
					Logs[i].Messages.Clear();
				}
			}
			TCPLogRefreshFlag = true;
		}
		private void tbMessages_Click(object sender, EventArgs e)
		{
			int index = tbMessages.GetLineFromCharIndex(tbMessages.SelectionStart);
			for (int i = 0; i < Logs.Count; i++)
			{
				if (Logs[i].ClientName == TCPLogName)
				{
					for (int m = 0; m < Logs[i].Messages.Count; m++)
					{
						if (Logs[i].Messages[m].TextIndex == index)
						{
							int bytecount = 0;
							tbMessage.Text = "";
							for (int b = 0; b < Logs[i].Messages[m].Length; b++)
							{
								if (bytecount == 0)
								{
									tbMessage.Text += b.ToString("X3") + ": ";
								}
								tbMessage.Text += Logs[i].Messages[m].Data[b].ToString("X2") + " ";
								bytecount++;
								if (bytecount == 4)
								{
									tbMessage.Text += "\r\n";
									bytecount = 0;
								}
							}
						}
					}
				}
			}
		}
		private void tbContain_TextChanged(object sender, EventArgs e)
		{
			TCPLogRefreshFlag = true;
		}
		private void lbClients_MouseUp(object sender, MouseEventArgs e)
		{
			if (lbClients.SelectedIndex != -1)
				TCPLogRefreshFlag = true;
		}

		private void btPause_Click(object sender, EventArgs e)
		{
			Paused = !Paused;
		}

	}
}
