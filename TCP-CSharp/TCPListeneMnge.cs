// TCP-CSharp - a C# class library for TCP affairs, including client/socket and server/listener manager. by Nai Weizhi

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TCP_CSharp
{
	internal class TCPListeneMnge
	{
		public TCPMnge Owner;
		public List<TCPListene> TCPListenes = new List<TCPListene>();

		public TCPListeneMnge(TCPMnge owner)
		{
			Owner = owner;
		}

		protected int GetListeneIndex(string name)
		{
			for (int i = 0; i < TCPListenes.Count; i++)
			{
				if (TCPListenes[i].ListenerName == name)
				{
					return i;
				}
			}
			return -1;
		}

		public int AddListen(string name, int port)
		{
			foreach (TCPListene listene in TCPListenes)
			{
				if (listene.Port == port)
					return 10048;
			}
			TCPListene tcplistene = new TCPListene(this, name, port);
			int suc = tcplistene.Open();
			if (suc == 0)
				TCPListenes.Add(tcplistene);
			return suc;
		}

		public bool RemoveListen(string name)
		{
			int ind = GetListeneIndex(name);
			if (ind >= 0)
			{
				TCPListenes[ind].Close();
				TCPListenes.RemoveAt(ind);
				return true;
			}
			else
				return false;
		}
		public bool RemoveListen(int port)
		{
			int found = 0;
			for (int i = 0; i <= TCPListenes.Count - 1; i++)
			{
				if (TCPListenes[i].Port == port)
				{
					TCPListenes[i].Close();
					TCPListenes.RemoveAt(i);
					found++;
				}
			}
			if (found > 0)
				return true;
			else
				return false;
		}

		public List<string> GetListenerList()
		{
			List<string> list = new List<string>();
			foreach (TCPListene listene in TCPListenes)
			{
				if (listene.Listening)
					list.Add(listene.ListenerName);
			}
			return list;
		}

		public void ClientGot(TCPListene tcplistene, TcpClient tcpclient)
		{
			Owner.ClientGot(tcplistene, tcpclient);
		}
	}
}
