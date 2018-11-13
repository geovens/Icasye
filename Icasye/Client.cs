// Icasye - a C# class library for oversimplified TCP communication by Nai Weizhi

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TCP_CSharp;

namespace Icasye
{
	internal class Client
	{
		public Mnge Owner;
		public string Name;
		public string Address;
		public int Port;
		public event MessageGotEvent OnOwnMessageGot;

		public bool IsIcasyeClient;
		public bool IsManullyAdded;
		public bool Init;
		public int WaitReplyID;
		// if is bloced OR DELETED (for thoes manually added)
		public bool Blocked;

		public bool Connected;
		public bool Connecting;

		public TCPMessage MakeUp = new TCPMessage();

		// local machine ip facing the client
		// public string LocalIP;

		public Client(Mnge owner)
		{
			Owner = owner;
		}

		public string GetTMName()
		{
			return Name + "TM";
		}
		
		public MessageGotEvent GetSpeMessageGotEvent()
		{
			return OnOwnMessageGot;
		}
		public void SetSpeMessageGotEvent(MessageGotEvent mge)
		{
			OnOwnMessageGot = null;
			OnOwnMessageGot += mge;
		}

		public int Connect()
		{
			int suc = Owner.TM.Connect(GetTMName(), Address, Port);
			if (suc == 0)
			{
				Owner.Owner.PrintLog("Icasye.Client.Connect: connect " + GetTMName() + " successful");
				Connected = true;
			}
			else
			{
				Owner.Owner.PrintLog("Icasye.Client.Connect: connect failed " + suc.ToString());
			}
			return suc;
		}
		public void ConnectThread()
		{
			if (Connecting)
				return;
			Connecting = true;

			Owner.Owner.PrintLog("Icasye.Client.ConnectThread: trying to connect " + GetTMName());
			Connect();

			// if it is also a Icasye client, send client info through the new TCP connection
			if (IsIcasyeClient)
			{
				Random rd = new Random();
				WaitReplyID = rd.Next(500) + 1;
				Thread.Sleep(100);
				SendWelcome();
			}
			Connecting = false;
		}

		public void SendWelcome()
		{
			Owner.Owner.PrintLog("Icasye.Client.SendWelcome: send welcome to " + Name + " replyid=" + WaitReplyID.ToString());
			TCPMessage msg = new TCPMessage();
			msg.SetString("IcasyeWIB\0" + Owner.MyName + "\0" + Owner.MyDataPort + "\0");
			msg.AppendString(WaitReplyID.ToString() + "\0" + "IcasyeWIE\0");
			Owner.TM.Send(GetTMName(), msg);
		}

		// canceled in 0.1.12
		// to form packet
		public void AppendMakeUp(TCPMessage msg)
		{
			MakeUp.Append(msg);
		}
		public IcasyeMsg CheckDeliver()
		{
			byte[] prefix = Encoding.ASCII.GetBytes("IcasyeMGB\0");
			byte[] suffix = Encoding.ASCII.GetBytes("\0IcasyeMGE\0");

			for (int i = 0; i <= MakeUp.Length - prefix.Length - suffix.Length; i++)
			{
				bool foundprefix = true;
				for (int k = 0; k < prefix.Length; k++)
					if (MakeUp.Data[i + k] != prefix[k])
					{
						foundprefix = false;
						break;
					}
				if (foundprefix)
				{
					for (int j = i + prefix.Length; j <= MakeUp.Length - suffix.Length; j++)
					{
						bool foundsuffix = true;
						for (int k = 0; k < suffix.Length; k++)
							if (MakeUp.Data[j + k] != suffix[k])
							{
								foundsuffix = false;
								break;
							}
						if (foundsuffix)
						{
							IcasyeMsg aMsg = new IcasyeMsg();
							aMsg.Length = j - i - prefix.Length;
							//for (int k = 0; k < aMsg.Length; k++)
							//	aMsg.Data[k] = MakeUp.Data[prefix.Length + k];
							Array.Copy(MakeUp.Data, prefix.Length, aMsg.Data, 0, aMsg.Length);
							MakeUp.Length = MakeUp.Length - j - suffix.Length;
							//for (int k = 0; k < MakeUp.Length; k++)
							//	MakeUp.Data[k] = MakeUp.Data[k + j + suffix.Length];
							Array.Copy(MakeUp.Data, j + suffix.Length, MakeUp.Data, 0, MakeUp.Length);
							return aMsg;
						}
					}
				}
			}
			return null;
		}
	}
}
