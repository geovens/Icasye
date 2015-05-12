// TCP-CSharp - a C# class library for TCP affairs, including client/socket and server/listener manager. by Nai Weizhi

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Net;
using System.Net.Sockets; 
using System.Threading;  
using System.IO;

namespace TCP_CSharp
{
	internal class TCPListene
	{
		private TCPListeneMnge Owner;

		private TcpListener TcpListener;
		private Thread ListenThread;

		public string ListenerName;
		public int Port;
		public bool Listening;

		public TCPListene(TCPListeneMnge owner, string listenername, int port)
		{
			Owner = owner;
			ListenerName = listenername;
			Port = port;
		}

	
		public void Close()
		{
			Listening = false;
			TcpListener.Stop();
		}

		public int Open()
		{
			this.TcpListener = new TcpListener(IPAddress.Any, Port);
			try
			{
				this.TcpListener.Start();
			}
			catch (System.Exception e)
			{
				if (e.GetType().ToString() == "System.Net.Sockets.SocketException")
				{
					System.Net.Sockets.SocketException es = (System.Net.Sockets.SocketException)e;
					return es.ErrorCode;
				}
				else
					Owner.Owner.Owner.PrintError("TCPCSharp Error: while open new listen port. " + e);
			}

			Listening = true;
			this.ListenThread = new Thread(new ThreadStart(ListenForClients));
			this.ListenThread.Start();

			return 0;
		}

		private void ListenForClients()
		{
			TcpClient client = null;
			while (Listening)
			{
				//blocks until a client has connected to the server
				try
				{
					client = this.TcpListener.AcceptTcpClient();
				}
				catch
				{
					
				}

				if (client != null)
				{
					Owner.ClientGot(this, client);
					client = null;
				}
			}
		}

		public void Dispose()
		{

		}
	}
}
