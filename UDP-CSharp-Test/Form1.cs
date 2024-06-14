using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UDP_CSharp;

namespace UDP_CSharp_Test
{
	public partial class Form1 : Form
	{
		UDPCSharp UM = new UDPCSharp();
		string LastReceivedMessage;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			UM.OnMessageGot += OnMessageGot;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			tbListenPort.Enabled = false;
			btListenStart.Enabled = false;

			int ret = UM.AddListen("abc1", int.Parse(tbListenPort.Text));
			if (ret == 0)
				lbStatus.Text = "Start listen successful.";
			else
				lbStatus.Text = "Start listen failed.";
			UM.SetSpeMessageGotEvent("abc1", OnFavorite);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			tbDestIP.Enabled = false;
			tbDestPort.Enabled = false;

			UM.AddDestination("dest", tbDestIP.Text, int.Parse(tbDestPort.Text));

			UDPMessage msg = new UDPMessage();
			msg.SetString(tbDestMsg.Text);
			UM.Send("dest", msg);
			
			/*
			UDPMessage msg2 = new UDPMessage();
			msg2.Data[0] = (byte)'d';
			msg2.Data[1] = (byte)'a';
			msg2.Data[2] = (byte)'t';
			msg2.Data[3] = (byte)'a';
			msg2.Length = 4;
			UM.Send("dest", msg2);
			*/
		}

		private void OnMessageGot(string srcname, UDPMessage msg)
		{
			Console.WriteLine(srcname + " got " + msg.Length.ToString() + " bytes: " + msg.GetString());
			LastReceivedMessage = msg.GetString();
		}
		private void OnFavorite(string srcname, UDPMessage msg)
		{
			Console.WriteLine("got favorite message");
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			UM.StopListen("abc1");
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			lbListenReceived.Text = LastReceivedMessage;
		}
	}
}
