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
			UM.AddListen("abc1", 5001);
			UM.AddListen("abc", 5002);

			UM.SetSpeMessageGotEvent("abc1", OnFavorite);

			//UM.AddHost("h1", "127.0.0.1", 5001);
			//UM.AddHost("h2", "127.0.0.1", 500);
			//UM.AddHost("h2", "127.0.0.1", 5002);

			UM.RenameListener("abc", "abc2");
		}

		private void button2_Click(object sender, EventArgs e)
		{
			UDPMessage msg = new UDPMessage();
			msg.SetString("hello");
			UM.Send("h1", msg);
			UM.Send("h2", msg);
		}

		private void OnMessageGot(string srcname, UDPMessage msg)
		{
			Console.WriteLine(srcname + " got " + msg.Length.ToString() + " bytes");
		}
		private void OnFavorite(string srcname, UDPMessage msg)
		{
			Console.WriteLine("got favorite message");
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			UM.StopListen("abc1");
			UM.StopListen("abc2");
		}
	}
}
