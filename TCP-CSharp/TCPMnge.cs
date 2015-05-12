// TCP-CSharp - a C# class library for TCP affairs, including client/socket and server/listener manager. by Nai Weizhi

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TCP_CSharp
{
	internal class TCPMnge
	{
		public TCPCSharp Owner;
		
		public TCPListeneMnge ListeneMnge;
		public TCPSocketsMnge SocketsMnge;

		public TCPMnge(TCPCSharp owner)
		{
			Owner = owner;
			ListeneMnge = new TCPListeneMnge(this);
			SocketsMnge = new TCPSocketsMnge(this);
		}

		public int Connect(string name, string host, int port)
		{
			return SocketsMnge.Connect(name, host, port);
		}
		public bool Disconnect(string name)
		{
			return SocketsMnge.Disconnect(name);
		}
		public bool RenameConnection(string oldname, string newname)
		{
			return SocketsMnge.RenameConn(oldname, newname);
		}
		public List<string> GetConnectionList()
		{
			return SocketsMnge.GetConnectionList();
		}
		public string GetRemoteAddress(string name)
		{
			return SocketsMnge.GetRemoteAddress(name);
		}
		public int GetRemotePort(string name)
		{
			return SocketsMnge.GetRemotePort(name);
		}

		public int AddListen(string name, int port)
		{
			return ListeneMnge.AddListen(name, port);
		}
		public bool RemoveListen(string name)
		{
			return ListeneMnge.RemoveListen(name);
		}
		public bool RemoveListen(int port)
		{
			return ListeneMnge.RemoveListen(port);
		}
		public List<string> GetListenerList()
		{
			return ListeneMnge.GetListenerList();
		}

		public bool Send(string destname, byte[] bytes, int length)
		{
			return SocketsMnge.Send(destname, bytes, length);
		}

		public MessageGotEvent GetSpeMessageGotEvent(string name)
		{
			return SocketsMnge.GetSpeMessageGotEvent(name);
		}
		public bool SetSpeMessageGotEvent(string name, MessageGotEvent mge)
		{
			return SocketsMnge.SetSpeMessageGotEvent(name, mge);
		}

		public bool ClientGot(TCPListene listenefrom, TcpClient client)
		{
			string name = listenefrom.ListenerName;
			int i = 1;
			while (SocketsMnge.IsConnected(name))
			{
				i++;
				name = listenefrom.ListenerName + i.ToString();
			}

			SocketsMnge.AttachConn(name, client);
			client.NoDelay = true;
			Owner.ClientGot(name, listenefrom.ListenerName);
			return true;
		}
		public void MessageGot(string srcname, byte[] bytes, int length)
		{
			Owner.MessageGot(srcname, bytes, length);
		}
		public void DisconnectHappened(string srcname)
		{
			Owner.DisconnectHappened(srcname);
		}
	}
}

