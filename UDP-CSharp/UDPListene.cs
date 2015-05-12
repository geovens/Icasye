// UDP-CSharp - a C# class library for UDP affairs by Nai Weizhi

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UDP_CSharp
{
	internal class UDPListene
	{
		public UDPMnge Owner;

		public string Name;
		public int Port;
		public MessageGotEvent OnOwnMessageGot;
		public bool Listening;

		Socket ListenSocket;
		Thread ListenThread;

		public UDPListene(UDPMnge owner, string name, int port)
		{
			Owner = owner;
			Name = name;
			Port = port;
		}

		public bool StartListen()
		{
			try
			{
				ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				IPEndPoint udplocalpoint = new IPEndPoint(IPAddress.Any, Port);
				ListenSocket.Bind((EndPoint)udplocalpoint);
				ListenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 100);
			}
			catch (Exception e)
			{
				if (e.GetType().ToString() == "System.Net.Sockets.SocketException")
				{
					System.Net.Sockets.SocketException es = (System.Net.Sockets.SocketException)e;
					Owner.Owner.PrintError("UDPListene.StartListen: start listen exception, error code " + es.ErrorCode);
					return false;
				}
				else
				{
					Owner.Owner.PrintError("UDPListene.StartListen: start listen unknown exception"); 
					return false;
				}
			}
			Listening = true;
			ListenThread = new Thread(ReceiveThread);
			ListenThread.Start();
			return true;
		}

		public void StopListen()
		{
			Listening = false;
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

		private void ReceiveThread()
		{
			int l;
			byte[] r = new byte[50000];
			while (Listening)
			{
				try
				{
					l = ListenSocket.Receive(r);
					if (l != 0)
					{
						Owner.MessageGot(Name, r, l);
					}
				}
				catch (Exception e)
				{
					if (e.GetType().ToString() == "System.Net.Sockets.SocketException")
					{
						System.Net.Sockets.SocketException es = (System.Net.Sockets.SocketException)e;
						if (es.ErrorCode != 10060)
							Owner.Owner.PrintError("UDPListene.ReceiveThread: receive exception, error code " + es.ErrorCode.ToString());
					}
					else
						Owner.Owner.PrintError("UDPListene.ReceiveThread: unknown exception");
				}
			}
			ListenSocket.Close();
		}

	}
}
