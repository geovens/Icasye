// TCP-CSharp - a C# class library for TCP affairs, including client/socket and server/listener manager. by Nai Weizhi

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TCP_CSharp
{
	internal class TCPSocketsMnge
	{
		public TCPMnge Owner;	
		public List<TCPSocket> TCPSockets = new List<TCPSocket>();

		public TCPSocketsMnge(TCPMnge owner)
		{
			Owner = owner;
		}

		protected TCPSocket GetTCPSocket(string name)
		{
			for (int i = 0; i < TCPSockets.Count; i++)
			{
				if (TCPSockets[i].ConnName == name)
				{
					return TCPSockets[i];
				}
			}
			return null;
		}
		protected int GetTCPSocketIndex(string name)
		{
			for (int i = 0; i < TCPSockets.Count; i++)
			{
				if (TCPSockets[i].ConnName == name)
				{
					return i;
				}
			}
			return -1;
		}

		public int Connect(string name, string host, int port)
		{
			int found = GetTCPSocketIndex(name);
			if (found >= 0)
			{
				if (!TCPSockets[found].Connected)
				{
					TCPSockets[found].Address = host;
					TCPSockets[found].Port = port;
					int ret = TCPSockets[found].Connect();
					return ret;
				}
				else
				{
					// already have a currently connected connection with the same name
					return -1;
				}
			}
			else
			{
				TCPSocket aConnSocket = new TCPSocket(this, name, host, port);
				TCPSockets.Add(aConnSocket);
				int ret = aConnSocket.Connect();
				return ret;
			}
		}
		public bool AddUnconnectedTCPSocket(string name, string host, int port)
		{
			int found = GetTCPSocketIndex(name);
			if (found >= 0)
			{
				return false;
			}
			else
			{
				TCPSocket aConnSocket = new TCPSocket(this, name, host, port);
				TCPSockets.Add(aConnSocket);
				return true;
			}
		}
		public bool Disconnect(string name)
		{
			int ind = GetTCPSocketIndex(name);
			if (ind >= 0)
			{
				if (TCPSockets[ind].Connected)
				{
					TCPSockets[ind].Close();
					//TCPSockets.RemoveAt(ind);
					return true;
				}
				else
					return false;
			}
			else
				return false;
		}
		public bool SetTimeout(string name, int ms)
		{
			int ind = GetTCPSocketIndex(name);
			if (ind >= 0)
			{			
				TCPSockets[ind].SetTimeout(ms);
				return true;
			}
			else
				return false;
		}
		public bool AttachConn(string name, TcpClient client)
		{
			int found = GetTCPSocketIndex(name);
			if (found >= 0)
			{
				if (!TCPSockets[found].Connected)
				{
					TCPSockets[found].TcpClient = client;
					TCPSockets[found].Address = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
					TCPSockets[found].Port = ((IPEndPoint)client.Client.RemoteEndPoint).Port;
					TCPSockets[found].StartListenThread();
					return true;
				}
				else
				{
					Owner.Owner.PrintError("TCPCSharp Error: attaching an connection which have a name conflict with ab existing connected one");
					return false;
				}
			}
			else
			{
				TCPSocket aConnSocket = new TCPSocket(this, name);
				aConnSocket.TcpClient = client;
				aConnSocket.Address = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
				aConnSocket.Port = ((IPEndPoint)client.Client.RemoteEndPoint).Port;
				aConnSocket.StartListenThread();
				TCPSockets.Add(aConnSocket);
				return true;
			}
		}
		public bool RenameConn(string oldname, string newname)
		{
			TCPSocket tcpsocket = GetTCPSocket(oldname);
			if (tcpsocket != null)
			{
				TCPSocket targetsocket = GetTCPSocket(newname);
				// if there are no current socket with a name conflict with newname
				if (targetsocket == null)
				{
					tcpsocket.ConnName = newname;
					return true;
				}
				else
				{
					// if there is a socket with a name the same with newname but is not connected, replace it
					if (!targetsocket.Connected)
					{
						TCPSockets.RemoveAt(GetTCPSocketIndex(newname));
						tcpsocket.ConnName = newname;
						return true;
					}
					else
						return false;
				}
			}
			else
			{
				return false;
			}
		}

		public List<string> GetConnectionList()
		{
			List<string> list = new List<string>();
			foreach (TCPSocket socket in TCPSockets)
			{
				if (socket.Connected)
					list.Add(socket.ConnName);
			}
			return list;
		}
		public string GetRemoteAddress(string name)
		{
			TCPSocket tcpsocket = GetTCPSocket(name);
			if (tcpsocket != null)
			{
				return tcpsocket.Address;
			}
			else
				return null;
		}
		public int GetRemotePort(string name)
		{
			TCPSocket tcpsocket = GetTCPSocket(name);
			if (tcpsocket != null)
			{
				return tcpsocket.Port;
			}
			else
				return 0;
		}
		public MessageGotEvent GetSpeMessageGotEvent(string name)
		{
			TCPSocket tcpsocket = GetTCPSocket(name);
			if (tcpsocket != null)
				return tcpsocket.GetSpeMessageGotEvent();
			else
				return null;
		}
		public bool SetSpeMessageGotEvent(string name, MessageGotEvent mge)
		{
			TCPSocket tcpsocket = GetTCPSocket(name);
			if (tcpsocket != null)
			{
				tcpsocket.SetSpeMessageGotEvent(mge);
				return true;
			}
			else
			{
				AddUnconnectedTCPSocket(name, "", 0);
				tcpsocket = GetTCPSocket(name);
				tcpsocket.SetSpeMessageGotEvent(mge);
				return true;
			}
		}

		public bool IsConnected(string name)
		{
			TCPSocket tcpsocket = GetTCPSocket(name);
			if (tcpsocket != null)
				return tcpsocket.Connected;
			else
				return false;
		}

		public void MessageGot(string srcname, byte[] bytes, int length)
		{
			Owner.MessageGot(srcname, bytes, length);
		}

		public bool Send(string desname, byte[] bytes, int length)
		{
			TCPSocket tcpsocket = GetTCPSocket(desname);
			if (tcpsocket != null)
				return tcpsocket.Send(bytes, length);
			else
				return false;
		}

		public void DisconnectHappened(string srcname)
		{
			int ind = GetTCPSocketIndex(srcname);
			if (ind >= 0)
			{
				//TCPSockets.RemoveAt(ind);
			}
			else
				Owner.Owner.PrintError("TCPCSharp Error: disconnecthappended on an connection no exist in TCPSockets[]");
			Owner.DisconnectHappened(srcname);
		}
	}
}
