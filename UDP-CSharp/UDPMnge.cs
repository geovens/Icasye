// UDP-CSharp - a C# class library for UDP affairs by Nai Weizhi

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace UDP_CSharp
{
	internal class UDPMnge
	{
		public UDPCSharp Owner;

		public List<UDPSocket> Sockets;
		public List<UDPListene> Listenes;

		public UDPMnge(UDPCSharp owner)
		{
			Owner = owner;
			Sockets = new List<UDPSocket>();
			Listenes = new List<UDPListene>();
		}

		protected UDPSocket GetUDPSocket(string name)
		{
			for (int i = 0; i < Sockets.Count; i++)
			{
				if (Sockets[i].Name == name)
				{
					return Sockets[i];
				}
			}
			return null;
		}
		protected int GetUDPSocketIndex(string name)
		{
			for (int i = 0; i < Sockets.Count; i++)
			{
				if (Sockets[i].Name == name)
				{
					return i;
				}
			}
			return -1;
		}
		protected UDPListene GetUDPListene(string name)
		{
			for (int i = 0; i < Listenes.Count; i++)
			{
				if (Listenes[i].Name == name)
				{
					return Listenes[i];
				}
			}
			return null;
		}
		protected UDPListene GetUDPListene(int port)
		{
			for (int i = 0; i < Listenes.Count; i++)
			{
				if (Listenes[i].Port == port)
				{
					return Listenes[i];
				}
			}
			return null;
		}
		protected int GetUDPListeneIndex(string name)
		{
			for (int i = 0; i < Listenes.Count; i++)
			{
				if (Listenes[i].Name == name)
				{
					return i;
				}
			}
			return -1;
		}

		public int AddDestination(string name, string address, int port)
		{
			int found = GetUDPSocketIndex(name);
			if (found >= 0)
			{
				Sockets[found].Address = address;
				Sockets[found].Port = port;
			}
			else
			{
				Owner.PrintLog("UDPMnge.AddDestination: add a new destination");
				UDPSocket aSocket = new UDPSocket();
				aSocket.Name = name;
				aSocket.Address = address;
				aSocket.Port = port;
				Sockets.Add(aSocket);
			}
			return 0;
		}
		public int AddBroadcastDestination(string name, string ip, int bitlength, int port)
		{
			byte[] submask = new byte[4];
			submask[0] = 0x00;
			submask[1] = 0x00;
			submask[2] = 0x00;
			submask[3] = 0x00;
			for (int j = 0; j < 4; j++)
			{
				byte amask = 0xFF;
				for (int i = j * 8 + 1; i <= j * 8 + 8 && i <= bitlength; i++)
				{
					amask = (byte)(amask >> 1);
					submask[j] |= (byte)(~amask);
				}
			}
			string submasks = submask[0].ToString() + "." + submask[1].ToString() + "." + submask[2].ToString() + "." + submask[3].ToString();
			int suc = AddBroadcastDestination(name, ip, submasks, port);
			return suc;
		}
		public int AddBroadcastDestination(string name, string ip, string mask, int port)
		{
			int found = GetUDPSocketIndex(name);
			if (found >= 0)
			{
				return -1;
				//Sockets[found].Address = address;
				//Sockets[found].Port = port;
			}
			else
			{
				Owner.PrintLog("UDPMnge.AddBroadcastDestination: adding a new broadcast destination");
				UDPSocket aSocket = new UDPSocket();
				aSocket.Name = name;
				IPAddress ipa = IPAddress.None;
				bool suc = IPAddress.TryParse(ip, out ipa);
				if (!suc)
				{
					Owner.PrintLog("UDPMnge.AddBroadcastDestination: add failed, ip not correct");
					return -9;
				}
				byte[] ipb = ipa.GetAddressBytes();
				if (ipb.Length != 4)
				{
					Owner.PrintLog("UDPMnge.AddBroadcastDestination: add failed, ip.getaddressbyte() output not 4 bytes");
					return -1;
				}
				IPAddress maska = IPAddress.None;
				suc = IPAddress.TryParse(mask, out maska);
				if (!suc)
				{
					Owner.PrintLog("UDPMnge.AddBroadcastDestination: add failed, mask not correct");
					return -9;
				}
				byte[] maskb = maska.GetAddressBytes();
				if (maskb.Length != 4)
				{
					Owner.PrintLog("UDPMnge.AddBroadcastDestination: add failed, mask.getaddressbyte() output not 4 bytes");
					return -1;
				}
				for (int j = 0; j < 4; j++)
				{
					ipb[j] |= (byte)(~maskb[j]);
				}
				aSocket.Address = ipb[0].ToString() + "." + ipb[1].ToString() + "." + ipb[2].ToString() + "." + ipb[3].ToString();
				aSocket.Port = port;
				Sockets.Add(aSocket);
			}
			return 0;
		}
		public bool AddListen(string name, int port)
		{
			UDPListene listene = GetUDPListene(port);
			if (listene != null && listene.Listening)
			{
				Owner.PrintError("UDPMnge.AddListen: port is already opened by me");
				return false;
			}

			int found = GetUDPListeneIndex(name);
			if (found >= 0)
			{
				if (!Listenes[found].Listening)
				{
					Owner.PrintError("UDPMnge.AddListen: already have an unlistening listener with the name '" + name + "', start it");
					Listenes[found].Port = port;
					bool suc = Listenes[found].StartListen();
					return suc;
				}
				else
				{
					Owner.PrintError("UDPMnge.AddListen: already have a listening listene with the name '" + name + "', add failed");
					return false;
				}
			}
			else
			{
				Owner.PrintLog("UDPMnge.AddListen: adding a listene '" + name + "' on port " + port.ToString());
				UDPListene aListene = new UDPListene(this, name, port);
				bool suc = aListene.StartListen();
				if (suc)
				{
					Owner.PrintLog("UDPMnge.AddListen: add success");
					Listenes.Add(aListene);
				}
				else
					Owner.PrintLog("UDPMnge.AddListen: add failed");
				return suc;
			}
		}
		public bool AddUnStartListen(string name, int port)
		{
			UDPListene listene = GetUDPListene(port);
			if (listene != null && listene.Listening)
				return false;

			int found = GetUDPListeneIndex(name);
			if (found >= 0)
			{
				return false;
			}
			else
			{
				Owner.PrintLog("UDPMnge.AddUnStartListen: add an unstarted listener '" + name + "' on port " + port.ToString());
				UDPListene aListene = new UDPListene(this, name, port);
				Listenes.Add(aListene);
				return true;
			}
		}
		public bool RemoveListen(string name)
		{
			UDPListene listene = GetUDPListene(name);
			if (listene != null)
			{
				Owner.PrintLog("UDPMnge.RemoveListen: stop listener '" + name);
				listene.StopListen();
				return true;
			}
			else
				return false;
		}
		public bool RemoveListen(int port)
		{
			int found = 0;
			for (int i = 0; i <= Listenes.Count - 1; i++)
			{
				if (Listenes[i].Port == port)
				{
					Owner.PrintLog("UDPMnge.RemoveListen: stop listener on port " + port.ToString());
					Listenes[i].StopListen();
					found++;
				}
			}
			if (found > 0)
				return true;
			else
				return false;
		}
		public bool RenameListene(string oldname, string newname)
		{
			UDPListene udplistene = GetUDPListene(oldname);
			if (udplistene != null)
			{
				if (GetUDPListene(newname) == null)
				{
					Owner.PrintLog("UDPMnge.RenameListene: rename '" + oldname + "' to '" + newname + "'");
					udplistene.Name = newname;
					return true;
				}
				else
					return false;
			}
			else
				return false;
		}
		public List<string> GetListenerList()
		{
			List<string> list = new List<string>();
			foreach (UDPListene listene in Listenes)
			{
				if (listene.Listening)
					list.Add(listene.Name);
			}
			return list;
		}

		public bool Send(string destname, byte[] bytes, int length)
		{
			UDPSocket socket = GetUDPSocket(destname);
			if (socket == null)
				return false;

			Socket sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			try
			{
				sendSocket.Connect(socket.Address, socket.Port);
				int ret = sendSocket.Send(bytes, length, SocketFlags.None);
				sendSocket.Shutdown(SocketShutdown.Both);
				// the Close takes a lot time in lab, so try to comment it. consider to package sendSocket into each UDPSocket
				//sendSocket.Close(); 
			}
			catch (Exception e)
			{
				if (e.GetType().ToString() == "System.Net.Sockets.SocketException")
				{
					System.Net.Sockets.SocketException es = (System.Net.Sockets.SocketException)e;
					Owner.PrintError("UDPMnge.Send: exception, error code " + es.ErrorCode);
				}
				else
				{
					Owner.PrintError("UDPMnge.Send: unknown exception " + e.ToString());
				}
				return false;
			}
			return true;
		}

		public MessageGotEvent GetSpeMessageGotEvent(string name)
		{
			UDPListene udplistene = GetUDPListene(name);
			if (udplistene != null)
				return udplistene.GetSpeMessageGotEvent();
			else
				return null;
		}
		public bool SetSpeMessageGotEvent(string name, MessageGotEvent mge)
		{
			UDPListene udplistene = GetUDPListene(name);
			if (udplistene != null)
			{
				Owner.PrintLog("UDPMnge.SetSpeMessageGotEvent: setting");
				udplistene.SetSpeMessageGotEvent(mge);
				return true;
			}
			else
			{
				Owner.PrintLog("UDPMnge.SetSpeMessageGotEvent: no existing listener with name '" + name + "', add an unstarted one and set success");
				AddUnStartListen(name, 0);
				udplistene = GetUDPListene(name);
				udplistene.SetSpeMessageGotEvent(mge);
				return true;
			}
		}
	
		public void MessageGot(string srcname, byte[] bytes, int length)
		{
			Owner.MessageGot(srcname, bytes, length);
		}
	}
}

