// This is a test software of TCP-CSharp by Nai Weizhi

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using TCP_CSharp;


namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		[DllImport("user32.dll")]
		static extern void FlashWindow(IntPtr a, bool b); 

		public TCPCSharp TM = new TCPCSharp();

		// the list of current connections (their names). renewed periodly by timer1 from TM
		public List<string> ConnectionList;
		// the list of currently listeners (their names). renewed periodly by timer1 from TM
		public List<string> ListenerList;

		// the TCP data records for each connection
		public List<string> TCPDataLog_ConnNames = new List<string>();
		public List<string> TCPDataLog_Messages = new List<string>();
		public bool Paused;

		// status log
		public string StatusLog;

		public Form1()
		{
			InitializeComponent();

			// add an event for client got. this is triggered when a listener got a client connection
			TM.OnClientGot += OnClientGot;
			// add an event for message got. any connection receiving data will trigger this
			TM.OnMessageGot += OnMessageGot;
			// add an event for forced disconnect. only triggered when the connection was closed by the other side, whether server or client
			TM.OnForcedDisconnect += OnDisconnect;
		}

		
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			ConnectionList = TM.GetConnectionList();
			foreach (string conn in ConnectionList)
			{
				TM.Disconnect(conn);
			}
			foreach (string listene in ListenerList)
			{
				TM.StopListen(listene);
			}
		}

		private void btAddListen_Click(object sender, EventArgs e)
		{
			int suc = TM.AddListen(tbListeneName.Text, int.Parse(tbListenPort.Text));
			if (suc == 0)
				AddStatusLog("listener '" + tbListeneName.Text + "': start to listen on port " + tbListenPort.Text);
		}
		private void btRemoveListen_Click(object sender, EventArgs e)
		{
			if (lbListeners.SelectedIndex != -1)
			{
				TM.StopListen(lbListeners.SelectedItem.ToString());
				AddStatusLog("listener '" + lbListeners.SelectedItem.ToString() + "': stoped listening");
			}
		}
		private void btConnect_Click(object sender, EventArgs e)
		{
			int suc = TM.Connect(tbConnectName.Text, tbConnectAddress.Text, int.Parse(tbConnectPort.Text));
			switch (suc)
			{
				case 0:
					AddStatusLog("connection '" + tbConnectName.Text + "': successfully connected");
					break;
				case 10061:
					AddStatusLog("connection '" + tbConnectName.Text + "': connect rejected by remote server");
					break;
				case 10060:
					AddStatusLog("connection '" + tbConnectName.Text + "': connect timeout");
					break;
				case 11004:
					AddStatusLog("connection '" + tbConnectName.Text + "': connect failed, wrong address");
					break;
				case -9:
					AddStatusLog("connection '" + tbConnectName.Text + "': connect failed, wrong params");
					break;
				default:
					AddStatusLog("connection '" + tbConnectName.Text + "': connect failed, error code " + suc);
					break;
			}
		}
		private void btDisconnect_Click(object sender, EventArgs e)
		{
			if (lbConnections.SelectedIndex != -1)
			{
				TM.Disconnect(lbConnections.SelectedItem.ToString());
				ClearTCPLog(lbConnections.SelectedItem.ToString());
				AddStatusLog("connection '" + lbConnections.SelectedItem.ToString() + "': closed by user");
			}
		}
		// rename the selected connection
		private void btRenameConnection_Click(object sender, EventArgs e)
		{
			if (lbConnections.SelectedIndex != -1)
			{
				string oldname = lbConnections.SelectedItem.ToString();
				string newname = tbConnectName.Text;
				bool suc = TM.RenameConnection(oldname, newname);
				if (suc)
				{
					RenameTCPLogName(oldname, newname);
					AddStatusLog("connection '" + oldname + "': renamed to '" + newname + "'");
				}
			}

		}
		// set the selected connection to be favorite
		private void btFavorite_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < ConnectionList.Count; i++)
			{
				if (i == lbConnections.SelectedIndex)
				{
					TM.SetSpeMessageGotEvent(ConnectionList[i], OnFavoriteMessageGot);
					AddStatusLog("connection '" + ConnectionList[i] + "': set to be favorite");
				}
				else
					TM.SetSpeMessageGotEvent(ConnectionList[i], null);
			}
		}

		// send text message to the selected connection
		private void btSend_Click(object sender, EventArgs e)
		{
			if (lbConnections.SelectedIndex == -1)
				return;
			for (int i = 0; i < ConnectionList.Count; i++)
			{
				if (ConnectionList[i] == lbConnections.SelectedItem.ToString())
				{
					TCPMessage message = new TCPMessage();
					message.SetString(PlainToString(tbSend.Text));
					bool suc = TM.Send(ConnectionList[i], message);
					if (suc)
					{
						AddTCPLog(ConnectionList[i], "S  : " + message.GetString());
						AddStatusLog("connection '" + ConnectionList[i] + "': send " + message.Length.ToString() + " bytes");
					}
				}
			}		
		}

		// happens when a client connection is establihed by a listener
		public void OnClientGot(string name, string listenername)
		{
			AddStatusLog("listener '" + listenername + "': got a client and named it '" + name + "'");
		}
		// happens when TCP data was got by any connection
		public void OnMessageGot(string srcname, TCPMessage message)
		{
			AddTCPLog(srcname, "  R: " + message.GetString());
			AddStatusLog("connection '" + srcname + "': received " + message.Length.ToString() + " bytes");
		}
		// only happens when the connection was closed by remote side, whether server or client
		public void OnDisconnect(string srcname)
		{
			ClearTCPLog(srcname);
			AddStatusLog("connection '" + srcname + "': closed by remote side");
		}
		// happens when the favorite connection received TCP data
		public void OnFavoriteMessageGot(string srcname, TCPMessage message)
		{
			// to let the window flash
			FlashWindowFlag = true;
		}



		bool FlashWindowFlag = false;
		string TCPLogName;
		bool TCPLogRefreshFlag = false;
		bool StatusLogRefreshFlag = false;
		private void timer1_Tick(object sender, EventArgs e)
		{
			string selected = "";
			if (lbConnections.SelectedIndex != -1)
				selected = lbConnections.SelectedItem.ToString();
			ConnectionList = TM.GetConnectionList();
			lbConnections.Items.Clear();
			foreach (string conn in ConnectionList)
			{
				lbConnections.Items.Add(conn);
			}
			for (int i = 0; i < lbConnections.Items.Count; i++)
			{
				if (lbConnections.Items[i].ToString() == selected)
					lbConnections.SelectedIndex = i;
			}

			if (lbListeners.SelectedIndex != -1)
				selected = lbListeners.Items[lbListeners.SelectedIndex].ToString();
			ListenerList = TM.GetListenerList();
			lbListeners.Items.Clear();
			foreach (string listener in ListenerList)
			{
				lbListeners.Items.Add(listener);
			}
			for (int i = 0; i < lbListeners.Items.Count; i++)
			{
				if (lbListeners.Items[i].ToString() == selected)
					lbListeners.SelectedIndex = i;
			}

			if (FlashWindowFlag)
			{
				FlashWindow(this.Handle, true);
				FlashWindowFlag = false;
			}
			if (lbConnections.SelectedIndex != -1)
			{
				selected = lbConnections.SelectedItem.ToString();
				if (TCPLogRefreshFlag || selected != TCPLogName)
				{
					tabTCPLog.Text = "TCP data of connection '" + selected + "'";
					tbMessages.Text = GetTCPLog(selected);
					tbMessages.SelectionStart = tbMessages.Text.Length;
					tbMessages.ScrollToCaret();
					TCPLogRefreshFlag = false;
					TCPLogName = lbConnections.SelectedItem.ToString();
				}
			}
			if (StatusLogRefreshFlag)
			{
				tbStatusLog.Text = StatusLog;
				StatusLogRefreshFlag = false;
			}
		}

		private void AddTCPLog(string name, string message)
		{
			if (Paused)
				return;

			bool found = false; ;
			for (int i = 0; i < TCPDataLog_ConnNames.Count; i++)
			{
				if (TCPDataLog_ConnNames[i] == name)
				{
					TCPDataLog_Messages[i] += StringToPlain(message) + "\r\n";
					found = true;
				}
			}
			if (!found)
			{
				TCPDataLog_ConnNames.Add(name);
				TCPDataLog_Messages.Add(StringToPlain(message) + "\r\n");
			}
			TCPLogRefreshFlag = true;
		}
		private string GetTCPLog(string name)
		{
			for (int i = 0; i < TCPDataLog_ConnNames.Count; i++)
			{
				if (TCPDataLog_ConnNames[i] == name)
				{
					return TCPDataLog_Messages[i];
				}
			}
			return "";
		}
		private void ClearTCPLog(string name)
		{
			for (int i = 0; i < TCPDataLog_ConnNames.Count; i++)
			{
				if (TCPDataLog_ConnNames[i] == name)
				{
					TCPDataLog_Messages[i] = "";
				}
			}
			TCPLogRefreshFlag = true;
		}
		private void RenameTCPLogName(string oldname, string newname)
		{
			for (int i = 0; i < TCPDataLog_ConnNames.Count; i++)
			{
				if (TCPDataLog_ConnNames[i] == oldname)
				{
					TCPDataLog_ConnNames[i] = newname;
				}
			}
		}
		private void AddStatusLog(string message)
		{
			if (Paused)
				return;

			StatusLog += message + "\r\n";
			StatusLogRefreshFlag = true;
		}

		private void lbConnections_MouseUp(object sender, MouseEventArgs e)
		{
			if (lbConnections.SelectedIndex == -1)
				return;
			string connname = lbConnections.SelectedItem.ToString();
			tbConnectName.Text = connname;
			tbConnectAddress.Text = TM.GetRemoteAddress(connname);
			tbConnectPort.Text = TM.GetRemotePort(connname).ToString();
		}

		public string StringToPlain(string str)
		{
			string strout = "";
			foreach (char c in str)
			{
				if (c == '\r')
					strout += "\\r";
				else if (c == '\n')
					strout += "\\n";
				else if (c == '\\')
					strout += "\\\\";
				else if (c >= 32 && c <= 126)
					strout += c;
				else
					strout += "\\" + Convert.ToByte(c).ToString("X2");
			}
			return strout;
		}

		public string PlainToString(string str)
		{
			string strout = "";
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == '\\')
				{
					if (i == str.Length - 1)
						break;
					if (str[i + 1] == 'r')
					{
						strout += '\r';
						i++;
					}
					else if (str[i + 1] == 'n')
					{
						strout += '\n';
						i++;
					}
					else if (str[i + 1] == '\\')
					{
						strout += '\\';
						i++;
					}
					else
					{
						if (i == str.Length - 2)
							break;
						int outnum;
						if (int.TryParse(str.Substring(i + 1, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out outnum))
						{
							strout += (char)outnum;
							i += 2;
						}
						else
						{
							strout += '\\';
						}
					}
				}
				else
					strout += str[i];
			}
			return strout;
		}

		private void btPause_Click(object sender, EventArgs e)
		{
			Paused = !Paused;
			if (Paused)
				btPause.Text = "Unpause";
			else
				btPause.Text = "Pause";
		}

		private void btClear_Click(object sender, EventArgs e)
		{
			if (lbConnections.SelectedIndex == -1)
				return;
			for (int i = 0; i < ConnectionList.Count; i++)
			{
				if (ConnectionList[i] == lbConnections.SelectedItem.ToString())
				{
					ClearTCPLog(ConnectionList[i]);
				}
			}
		}
	}
}
