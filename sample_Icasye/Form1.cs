
// This is a very simple sample program to show how to use Icasye library, which is a .NET library for LAN network communication. 
// The purpose of the library is to greatly simplify the implementation of LAN network communication in .NET projects for those who know nothing about and do not want to learn TCP/IP.

// How to run this sample program: 
// run two instances of this program, and click the 'Go online' button after choosing different names on each instance (please choose I'm David on one and I'm Tom on the other).
// After gone online, each instance will send random numbers to the other, and the random numbers will be shown in the text box of the other instance.

using System;
using System.Windows.Forms;
using Icasye;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		// the Icasye manager, which is our hero in this program
		IcasyeClient IcasyeClient = new IcasyeClient();
		// random number generator
		Random random = new Random();
		// my name to be shown to the other side I will be communicating with
		string MyName;
		// the name of the other side I will be communicating with
		string HisName;

		public Form1()
		{
			InitializeComponent();

			// By default, I'm David.
			ImDavid.Checked = true;

			// Something not very important in this sample program. We want this sample program to be as simple as possible, so we don't want to deal with threads.
			Form1.CheckForIllegalCrossThreadCalls = false;
		}

		private void RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			// When the user clicks on either I'm David or I'm Tom, set MyName and HisName accordingly.
			if (ImDavid.Checked)
			{
				MyName = "David";
				HisName = "Tom";
				btStart.Text = "Go online and start to send random rumbers to Tom!";
			}
			else if (ImTom.Checked)
			{
				MyName = "Tom";
				HisName = "David";
				btStart.Text = "Go online and start to send random rumbers to David!";
			}
		}

		private void btStart_Click(object sender, EventArgs e)
		{
			// After the 'Go online' button is clicked:

			// Tell the Icasye Manager what my name is.
			IcasyeClient.SetMyName(MyName);

			// Setup a receiver function to handle messages from anyone others. 'Anyone others' include David or Tom, of course.
			IcasyeClient.OnMessageGot += OnDataGot;
			// Or setup a receiver function to hundle messages from someone with the particular name only.
			//IcasyeClient.SetSpecificMessageGotEvent(HisName, OnDataGot);

			// Go online! So that I can communicate with others now.
			IcasyeClient.GoOnline();

			// Some changes to the UI
			ImDavid.Enabled = false;
			ImTom.Enabled = false;
			btStart.Enabled = false;
			btStop.Enabled = true;
		}

		private void btStop_Click(object sender, EventArgs e)
		{
			// Go offline.
			IcasyeClient.GoOffline();

			// Some changes to the UI
			ImDavid.Enabled = true;
			ImTom.Enabled = true;
			btStart.Enabled = true;
			btStop.Enabled = false;
		}

		private void OnDataGot(string srcname, IcasyeMsg msg)
		{
			// This is the function to handle messages received.
			// Show the received message in the text box along with the name of the sender.
			lbShow.Text = "I got a number " + msg.ToString() + " from " + srcname;
		}

		private void TimerRun_Tick(object sender, EventArgs e)
		{
			// This runs once every second:

			// Generate a random number and then package it into a message.
			IcasyeMsg msg = new IcasyeMsg();
			msg.SetString(random.Next(1000).ToString());

			// Send the message to the other side.
			bool successful = IcasyeClient.Send(HisName, msg);

			// If someone named HisName is online, he will receive the message. 
			// If no others is online or none of the others online is named HisName, the message will be lost, and you will not receive any error or warning report.
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Upon quiting, don't forget to go offline.
			IcasyeClient.GoOffline();
		}

		// Note: you can also connect to other Icasye clients that are not in the same LAN network of yours, by using AddIcasyeClientManually(string IPAddress) function. 
		// And there are other APIs in the library that do different works. Check them in Icasye.xml
	}
}
