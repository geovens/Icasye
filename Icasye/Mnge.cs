// Icasye - a C# class library for oversimplified TCP communication by Nai Weizhi

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using TCP_CSharp;
using UDP_CSharp;

namespace Icasye
{
	internal class Mnge
	{
		public IcasyeClient Owner;

		public TCPCSharp TM;
		public UDPCSharp UM;
		// clients and general connections list. in this program, client means one instance user of Icasye library
		protected List<Client> Clients = new List<Client>();
		//0 = offline; 1 = online
		protected int IcasyeStatus;
		public string MyName;
		public int MyID;
		public int MyCardPort;
		public int MyDataPort;

		public TCPMessage WelcomeMsg = new TCPMessage();

		public Mnge(IcasyeClient owner)
		{
			Owner = owner;
			TM = new TCPCSharp();
			UM = new UDPCSharp();
			TM.OnMessageGot += OnMessageGot;
			TM.OnClientGot += OnClientGot;
			TM.OnForcedDisconnect += OnForcedDisconnect;
			UM.OnMessageGot += OnCardGot;
		}

		// public functions
		public bool SetMyName(string name)
		{
			if (name.Contains(",") || name.Contains("\0"))
				return false;

			if (IcasyeStatus == 0)
			{
				MyName = name;
				return true;
			}
			else
				return false;
		}
		public bool GoOnline()
		{
			if (IcasyeStatus == 1)
				return true;

			int suc = -1;
			for (MyDataPort = 5301; MyDataPort <= 5305; MyDataPort++)
			{
				suc = TM.AddListen("IcasyeDataListene", MyDataPort);
				if (suc == 0) break;
			}
			if (suc != 0) return false;

			for (MyCardPort = 5201; MyCardPort <= 5205; MyCardPort++)
			{
				suc = UM.AddListen("IcasyeCardListene", MyCardPort);
				if (suc == 0) break;
			}
			if (suc != 0) return false;

			IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
			foreach (IPAddress ip in ipHost.AddressList)
			{
				Owner.PrintLog("Icasye.Mnge.GoOnline: a native ip " + ip.ToString());
			}

			Random rd = new Random();
			MyID = rd.Next(10000);

			IcasyeStatus = 1;
			Thread MngeThread = new Thread(ManageThread);
			MngeThread.Start();

			SendCard(1);
			Owner.PrintLog("Icasye.Mnge.GoOnline: successful online");
			return true;
		}
		public bool GoOffline()
		{
			TM.StopListen("IcasyeDataListene");
			UM.StopListen("IcasyeCardListene");

			// wait for threads (especially the UDP listener) to exit
			Thread.Sleep(200);
			IcasyeStatus = 0;
			return true;
		}
		public bool Send(string name, TCPMessage msg)
		{
			Client existing = GetClient(name);
			if (existing != null)
			{
				if (!existing.Blocked)
				{
					// canceled in 0.1.12
					// add prefix & suffix
					if (existing.IsIcasyeClient)
					{
					   byte[] bytes = Encoding.ASCII.GetBytes("IcasyeMGB\0");
					   msg.PrefixBytes(bytes);
					   bytes = Encoding.ASCII.GetBytes("\0IcasyeMGE\0");
					   msg.AppendBytes(bytes);
					}

					//Owner.PrintLog("Icasye.Mnge.Send: sending to " + existing.Name);
					bool suc = TM.Send(existing.GetTMName(), msg);
					if (suc)
					{
						//Owner.PrintLog("Icasye.Mnge.Send: success");

						// to send a duplicate to the wiretapper if existing
						Client wiretapper = GetClient(MyName + ".wiretapper");
						if (wiretapper != null)
							TM.Send(wiretapper.GetTMName(), msg);
					}
					else
						Owner.PrintLog("Icasye.Mnge.Send: failed");

					return suc;
				}
				else
					return false;
			}
			else
				return false;
		}
		public List<string> GetClientList()
		{
			List<string> names = new List<string>();
			for (int i = 0; i < Clients.Count; i++)
			{
				if (Clients[i].Connected && !Clients[i].Blocked)
					names.Add(Clients[i].Name);
			}
			return names;
		}
		public bool CheckClientOnline(string name)
		{
			Client existing = GetClient(name);
			if (existing != null && existing.Connected && !existing.Blocked)
				return true;
			else
				return false;
		}
		public string GetClientAddress(string name)
		{
			Client existing = GetClient(name);
			if (existing != null && existing.Connected && !existing.Blocked)
				return existing.Address;
			else
				return "";
		}
		public Icasye.MessageGotEvent GetSpeMessageGotEvent(string name)
		{
			Client existing = GetClient(name);
			if (existing != null)
				return existing.GetSpeMessageGotEvent();
			else
				return null;
		}
		public bool SetSpeMessageGotEvent(string name, Icasye.MessageGotEvent mge)
		{
			Client existing = GetClient(name);
			if (existing != null)
			{
				existing.SetSpeMessageGotEvent(mge);
				return true;
			}
			else
			{
				Client aClient = new Client(this);
				aClient.Name = name;
				aClient.SetSpeMessageGotEvent(mge);
				RegistNewClient(aClient);
				return true;
			}
		}
		public bool AddGeneralConnection(string name, string address, int port)
		{
			List<string> clientnames = GetClientList();
			foreach (string clientname in clientnames)
			{
				Client client = GetClient(clientname);
				if (client == null)
					continue;

				// not allowed to manually add a connection with a name same with onother client, whether current or outdated one
				//if (client.Name == name && !(client.Address == address && client.Port == port))
				if (client.Name == name || (client.Address == address && client.Port == port))
					return false;
				// if reenabling the connection to an old host which is blocked before, whether with the old name or a new name
				//else if (client.Address == address && client.Port == port)
				//{
				//   if (client.IsManullyAdded && client)
				//   {
				//      client.Name = name;
				//      client.IsIcasyeClient = false;
				//      client.Init = true;
				//      client.Blocked = false;
				//      return true;
				//   }
				//   else
				//   {
				//      return false;
				//   }
				//}
			}

			// continue reenabling, or adding a connection to a new host with a new name
			Client aclient = new Client(this);
			aclient.Name = name;
			aclient.Address = address;
			aclient.Port = port;
			aclient.IsIcasyeClient = false;
			aclient.IsManullyAdded = true;
			aclient.Init = true;
			bool suc = RegistNewClient(aclient);
			return suc;
		}
		public bool AddIcasyeClientManually(string address)
		{
			List<string> clientnames = GetClientList();
			foreach (string clientname in clientnames)
			{
				Client client = GetClient(clientname);
				if (client == null)
					continue;

				if (client.Address == address)
				{
					//if (client.Blocked)
					//{
					//   client.Blocked = false;
					//   return true;
					//}
					//else
						return false;
				}
			}

			bool suc = false;
			Random rd = new Random();
			string rdname = rd.Next(30000).ToString();
			for (int port = 5301; port <= 5305; port++)
			{
				Client aclient = new Client(this);

				aclient.Name = "manual-" + rdname + "-" + port.ToString();
				aclient.Address = address;
				aclient.Port = port;
				aclient.IsIcasyeClient = true;
				aclient.IsManullyAdded = true;
				aclient.Init = true;
				bool suc0 = RegistNewClient(aclient);
				suc = suc || suc0;
			}

			return suc;
		}
		public void BlockClient(string name)
		{
			Client existing = GetClient(name);
			if (existing != null)
			{
				existing.Blocked = true;
			}
			else
			{
				Client aClient = new Client(this);
				aClient.Name = name;
				aClient.Blocked = true;
				RegistNewClient(aClient);
			}
		}
		public void UnBlockClient(string name)
		{
			Client existing = GetClient(name);
			if (existing != null)
			{
				existing.Blocked = false;
			}
		}
		public bool DeleteAdded(string name)
		{
			Client existing = GetClient(name);
			if (existing != null)
			{
				if (existing.IsManullyAdded)
				{
					Clients.Remove(existing);
					TM.Disconnect(existing.GetTMName());
					return true;
				}
				else
					return false;
			}
			else
				return false;
		}
		public void SetToPrintLog()
		{
			TM.ToPrintLog = true;
			UM.ToPrintLog = true;
		}

		// unaccomplished, unstable, non-general public functions
		public string GetMyAddressFacingClient(string name)
		{
			Client existing = GetClient(name);
			if (existing != null && existing.Connected && !existing.Blocked)
			{
				string ip = existing.Address;

				IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
				foreach (IPAddress myip in ipHost.AddressList)
				{
					if (myip.AddressFamily == AddressFamily.InterNetworkV6)
						continue;
					string mask = GetSubnetMask(myip);
					byte[] ipb = IPAddress.Parse(ip).GetAddressBytes();
					byte[] myipb = myip.GetAddressBytes();
					byte[] maskb = IPAddress.Parse(mask).GetAddressBytes();
					for (int i = 0; i < 4; i++)
						myipb[i] |= (byte)(~maskb[i]);
					for (int i = 0; i < 4; i++)
						ipb[i] |= (byte)(~maskb[i]);

					bool yes = true;
					for (int i = 0; i < 4; i++)
						if (ipb[i] != myipb[i])
							yes = false;
					if (yes)
						return myip.ToString();
				}
			}
			return "";
		}

		// events handler
		protected void OnMessageGot(string srcname, TCPMessage message)
		{
			bool suc;
			bool found;

			// canceled in 0.1.12
			// form the welcome info packet
			if (message.GetString().Contains("IcasyeWIB"))
				WelcomeMsg = message;
			else if (WelcomeMsg.Length != 0)
				WelcomeMsg.Append(message);
			if (WelcomeMsg.Length != 0)
			{
				if (!WelcomeMsg.ToString().Contains("\0IcasyeWIE\0"))
					return;
			}

			// if this is Icasye welcome info
			if (message.ToString().Contains("IcasyeWIB"))
			{
				string[] msgparams = message.ToString().Split('\0');
				if (msgparams.Length == 6)
				{
					Owner.PrintLog("Icasye.Mnge.OnWelcomeGot: got TCP welcome info " + msgparams[1]);
					string reportname = msgparams[1];
					int reportport = 0;
					int.TryParse(msgparams[2], out reportport);
					int reportreplyid = 0;
					int.TryParse(msgparams[3], out reportreplyid);

					Client theclient = null;
					// this is a replay of my welcome
					if (reportreplyid >= 1001)
					{
						Owner.PrintLog("Icasye.Mnge.OnMessageGot: got a reply");
						List<string> clientnames2 = GetClientList();
						foreach (string clientname in clientnames2)
						{
							Client client = GetClient(clientname);
							if (client == null)
								continue;

							if (client.WaitReplyID == reportreplyid - 1000)
							{
								Owner.PrintLog("Icasye.Mnge.OnMessageGot: renew client depending on the reply");
								ChangeClientName(client.Name, reportname);
								client.Port = reportport;
								client.WaitReplyID = 0;
								theclient = client;
								break;
							}
						}
						if (theclient == null)
						{
							Owner.PrintError("Icasye.Mnge.OnMessageGot: got a reply id with no correspoding.");
							return;
						}
					}
					// not a reply
					else
					{
						Client existing = GetClient(reportname);
						// already in table
						if (existing != null)
						{
							theclient = existing;
							existing.Connected = true;
						}
						// not in table
						else
						{
							Owner.PrintLog("Icasye.Mnge.OnWelcomeGot: got a connection corresponding to no client in the table. add to the table");
							theclient = new Client(this);
							theclient.Name = reportname;
							theclient.Address = TM.GetRemoteAddress(srcname); // do not use the info reported. and seems no use
							theclient.Port = reportport;  // seems no use
							theclient.IsIcasyeClient = true;
							theclient.IsManullyAdded = false;
							theclient.Init = false;
							theclient.Connected = true;
							suc = RegistNewClient(theclient);
							if (suc)
								Owner.PrintLog("Icasye.Mnge.OnWelcomeGot: add " + suc.ToString());
						}
					}

					Owner.PrintLog("Icasye.Mnge.OnWelcomeGot: renaming " + srcname + " to " + theclient.GetTMName());
					suc = TM.RenameConnection(srcname, theclient.GetTMName());
					Owner.PrintLog("Icasye.Mnge.OnWelcomeGot: rename " + suc.ToString());

					// if this welcome info requires a reply
					if (reportreplyid > 0 && reportreplyid <= 1000)
					{
						theclient.WaitReplyID = reportreplyid + 1000;
						theclient.SendWelcome();
						theclient.SendWelcome();
						theclient.SendWelcome();
						theclient.SendWelcome();
						theclient.SendWelcome();
					}
				}
				else
				{
					Owner.PrintError("Icasye.Mnge.OnMessageGot: got a welcome message that has segment count not equal to 6");
				}
				WelcomeMsg.Length = 0;
				return;
			}

			// if it's heart beat message
			if (message.ToString().Contains("IcasyeHBB"))
			{
				return;
			}

			// or normal TCP data
			found = false;
			List<string> clientnames = GetClientList();
			foreach (string clientname in clientnames)
			{
				Client client = GetClient(clientname);
				if (client == null)
					continue;

				if (client.GetTMName() == srcname && !client.Blocked)
				{
					// canceled in 0.1.12
					// to decompose into packets
					if (client.IsIcasyeClient)
					{
					   client.AppendMakeUp(message);
					   IcasyeMsg aMsg;
					   while ((aMsg = client.CheckDeliver()) != null)
					   {
					      DeliverMessage(client.Name, aMsg);
					   }
					}
					else
					{
						DeliverMessage(client.Name, message);
					}
					found = true;
				}
			}
			if (!found)
				Owner.PrintLog("Icasye.Mnge.OnMessageGot: got a message having no corresponding client");
			
		}
		protected void DeliverMessage(string srcname, IcasyeMsg message)
		{
			Owner.MessageGot(srcname, message);
		
			Owner.PrintLog("Icasye.Mnge.OnMessageGot: got a message from " + srcname + ", length = " + message.Length);

			// to send a duplicate to the wiretapper if existing
			Client wiretapper = GetClient(MyName + ".wiretapper");
			if (wiretapper != null)
			{
				TCPMessage aMsg = (TCPMessage)message;
				byte[] bytes = Encoding.ASCII.GetBytes("IcasyeMGB\0");
				aMsg.PrefixBytes(bytes);
				bytes = Encoding.ASCII.GetBytes("\0IcasyeMGE\0");
				aMsg.AppendBytes(bytes);
				TM.Send(wiretapper.GetTMName(), aMsg);
			}
		}
		protected void OnClientGot(string name, string listenename)
		{
			Owner.PrintLog("Icasye.Mnge.OnClientGot: got a connection");
		}
		protected void OnForcedDisconnect(string srcname)
		{
			Owner.PrintLog("Icasye.Mnge.OnForcedDisconnect: OnForcedDisconnect event happens");
			List<string> clientnames = GetClientList();
			foreach (string clientname in clientnames)
			{
				Client client = GetClient(clientname);
				if (client == null)
					continue;

				if (client.GetTMName() == srcname)
				{
					Owner.PrintLog("Icasye.Mnge.OnForcedDisconnect: removing a connection in table");
					client.Connected = false;
				}
			}
		}
		protected void OnCardGot(string name, UDPMessage message)
		{
			if (!Owner.ToReceiveCard)
				return;
			string msgstr = message.GetString();
			if (msgstr.Contains("IcasyeCardData"))
			{
				string[] msgparams = msgstr.Split('\0');
				if (msgparams.Length == 6)
				{
					//Owner.PrintLog("OnCardGot: " + msgparams[0] + " " + msgparams[1] + " " + msgparams[2] + " " + msgparams[3]);
					string reportname = msgparams[1];
					string reportaddress = msgparams[2];
					int reportport = 0;
					int.TryParse(msgparams[3], out reportport);
					int reportid = 0;
					int.TryParse(msgparams[4], out reportid);
					int needreply = 0;
					int.TryParse(msgparams[5], out needreply);

					if (reportname == MyName && needreply == 1 && reportid != MyID)
					{
						Owner.PrintError("OnCardGot: got a card with the same name with me, exit.");
						GoOffline();
						return;
					}
					if (reportname != MyName && needreply == 1)
					{
						SendCard(0);
					}

					Client aClient = new Client(this);
					aClient.Name = reportname;
					aClient.Address = reportaddress;
					aClient.Port = reportport;
					aClient.IsIcasyeClient = true;
					aClient.IsManullyAdded = false;
					if (string.Compare(MyName, reportname) > 0)
						aClient.Init = true;
					else
						aClient.Init = false;

					Client existing = GetClient(reportname);
					if (existing != null)
					{
						// renew the info depending on the received card. especially init?
						existing.Address = aClient.Address;
						existing.Port = aClient.Port;
						existing.IsIcasyeClient = true;
						existing.Init = aClient.Init;
					}
					else
					{
						bool suc = RegistNewClient(aClient);
						if (suc)
							Owner.PrintLog("Icasye.Mnge.OnCardGot: got a new card from " + aClient.Name + "," + aClient.Address + "," + aClient.Port + "and registed");
					}
				}
			}
		}

		// thread
		protected void ManageThread()
		{
			List<string> TMConnections;
			int tick = 0;
			while (IcasyeStatus == 1)
			{
				// try to connect ?
				TMConnections = TM.GetConnectionList();
				for (int i = 0; i < Clients.Count; i++)
				{
					if (!TMConnections.Contains(Clients[i].GetTMName()))
					{
						if (!Clients[i].Connecting)
						{
							if (Clients[i].Init)
							{
								Thread aConnThread = new Thread(Clients[i].ConnectThread);
								aConnThread.Start();
							}
						}
					}
				}

				tick++;
				if (tick >= 50)
					tick = 0;

				// send card periodically
				if (tick == 0 && Owner.ToSendCard)
					SendCard(0);

				// send TCP heart beat message periodically. 2018.11.14
				if (tick % 10 == 0)
				{
					for (int i = 0; i < Clients.Count; i++)
					{
						if (!Clients[i].Connecting && Clients[i].Connected)
						{
							Clients[i].SendHeartBeat();
						}
					}
				}

				Thread.Sleep(100);
			}

			// when getting offline
			TMConnections = TM.GetConnectionList();
			foreach (string conn in TMConnections)
			{
			   TM.Disconnect(conn);
			}
			//foreach (Client client in Clients)
			//{
			//   client.Connected = false;
			//}
			Clients.Clear();
			Owner.PrintLog("Icasye.Mnge.ManageThread: exit");
		}
		protected void SendCard(int needreply)
		{

			//Owner.PrintLog("Icasye.Mnge.SendCard: sending card");
			IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
			UDPMessage msg = new UDPMessage();
			foreach (IPAddress ip in ipHost.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetworkV6)
					continue;
				string ipstr = ip.ToString();
				if (ipstr == "127.0.0.1")
					continue;
				//string[] ipstrs = ipstr.Split('.');
				//string ipstrw = ipstrs[0] + "." + ipstrs[1] + "." + ipstrs[2] + ".255";
				msg.SetString("IcasyeCardData\0" + MyName + "\0" + ipstr + "\0" + MyDataPort.ToString() + "\0" + MyID.ToString() +"\0" + needreply.ToString());
				string submask = GetSubnetMask(ip);
				for (int p = 5201; p <= 5205; p++)
				{
					UM.AddBroadcastDestination(ipstr + p.ToString(), ipstr, submask, p);
					UM.Send(ipstr + p.ToString(), msg);
				}
			}
		}

		// helper
		protected Client GetClient(string name)
		{
			for (int i = 0; i < Clients.Count; i++)
				if (Clients[i].Name == name)
					return Clients[i];
			return null;
		}
		protected int GetClientIndex(string name)
		{
			for (int i = 0; i < Clients.Count; i++)
				if (Clients[i].Name == name)
					return i;
			return -1;
		}
		protected bool RegistNewClient(Client client)
		{
			if (client.Name == MyName)
				return false;
			Client existing = GetClient(client.Name);
			if (existing != null)
				return false;

			for (int i = 0; i < Clients.Count; i++)
			{
				// if no !Clients[i].Connected in the following brackets, when two clients with the same address and port (though NAT) comes there will be error
				if (Clients[i].Address == client.Address && Clients[i].Port == client.Port && !Clients[i].Connected)
				{
					// this usually happens when a client with a bigger name than me renames to onother bigger name
					Owner.PrintLog("Icasye.Mnge.RegistNewClient: found existing Address=" + client.Address + ", Port=" + client.Port);
					Owner.PrintLog("Icasye.Mnge.RegistNewClient: renew an old client name " + Clients[i].Name + " to " + client.Name);
					Clients[i].Name = client.Name;
					Clients[i].Init = client.Init;
					//Clients[i].IsIcasyeClient = client.IsIcasyeClient;
					Clients[i].Connected = true;
					return true;
				}
			}

			Owner.PrintLog("Icasye.Mnge.RegistNewClient: adding a new client " + client.Name);
			Clients.Add(client);
			return true;
		}
		protected bool ChangeClientName(string oldname, string newname)
		{
			if (newname == MyName)
				return false;
			Client client = GetClient(oldname);
			if (client == null)
				return false;
			Client existing = GetClient(newname);
			if (existing != null)
			{
				if (existing.Connected)
					return false;
				else
					Clients.Remove(existing);
			}
			client.Name = newname;
			return true;
		}

		protected string GetSubnetMask(IPAddress address)
		{
			foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
			{
				foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
				{
					if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
					{
						if (address.Equals(unicastIPAddressInformation.Address))
						{
							return unicastIPAddressInformation.IPv4Mask.ToString();
						}
					}
				}
			}
			throw new ArgumentException(string.Format("Can't find subnetmask for IP address '{0}'", address));
		}
	}
}
