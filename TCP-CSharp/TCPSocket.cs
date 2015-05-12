﻿// TCP-CSharp - a C# class library for TCP affairs, including client/socket and server/listener manager. by Nai Weizhi

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Net.Sockets; 
using System.Threading;  
using System.IO;
using System.Collections.Generic;

namespace TCP_CSharp
{
	internal class TCPSocket
	{
		private TCPSocketsMnge Owner;

		public TcpClient TcpClient;
		private Thread RThread;

		public string ConnName;
		public string Address;
		public int Port;
		public bool Connected;

		public List<byte[]> Msgs = new List<byte[]>();

		public event MessageGotEvent OnOwnMessageGot;

		public TCPSocket(TCPSocketsMnge owner, string connname, string hostname, int port)
		{
			Owner = owner;
			ConnName = connname;
			Address = hostname;
			Port = port;
		}
		public TCPSocket(TCPSocketsMnge owner, string connname)
		{
			Owner = owner;
			ConnName = connname;
		}

		private void ReceiveThread()
		{
			NetworkStream ns;
			try
			{
				ns = TcpClient.GetStream();
			}
			catch
			{
				return;
			}
			byte[] bytes = new byte[50000];
			int length;
			while (Connected)
			{
				try
				{
					length = ns.Read(bytes, 0, bytes.Length);
					if (length == 0)   //连接已断开
					{
						Close();
						Owner.DisconnectHappened(this.ConnName);
						break;
					}
					else if (length < 0)
					{
						Owner.Owner.Owner.PrintError("ERRORd: bytesread = " + length.ToString());
					}
				}
				catch (System.Exception e)
				{
					if (e.GetType().ToString() == "System.IO.IOException")
					{
						Close();
						Owner.DisconnectHappened(this.ConnName);
					}
					else
						Owner.Owner.Owner.PrintError("TCPChsarp Error: " + e);
					break;
				}
				Owner.MessageGot(this.ConnName, bytes, length);
				//ns.Flush();
			}
		}

		public void Close()
		{
			Connected = false;
			TcpClient.Close();
		}

		public int Connect()
		{
			try
			{
				TcpClient = new TcpClient(Address, Port);
				TcpClient.NoDelay = true;
			}
			catch (System.Exception e)
			{
				if (e.GetType().ToString() == "System.Net.Sockets.SocketException")
				{
					System.Net.Sockets.SocketException es = (System.Net.Sockets.SocketException)e;
					return es.ErrorCode;
				}
				else if (e.GetType().ToString() == "System.ArgumentOutOfRangeException")
				{
					Owner.Owner.Owner.PrintError("ee2: " + e);
					return -9;
				}
				else
					Owner.Owner.Owner.PrintError("TCPCSharp Error: while connect. " + e);
			}
			Connected = true;
			RThread = new Thread(ReceiveThread);
			RThread.Start();
			return 0;
		}
		// as some objects of this class is generated by Lisners but not by users, just start the thread but not to connect
		public void StartListenThread()
		{
			Connected = true;
			RThread = new Thread(ReceiveThread);
			RThread.Start();
		}

		//public bool Send(string SendString)
		//{
		//   if (TcpClient == null || Connected == false)
		//   {
		//      Owner.PrintError("Note: Already Disconnected. Do not call me.");
		//      return false;
		//   }

		//   NetworkStream sendStream = TcpClient.GetStream();
		//   Byte[] sendBytes = Encoding.Default.GetBytes(SendString);
		//   try
		//   {
		//      sendStream.Write(sendBytes, 0, sendBytes.Length);
		//      sendStream.Flush();
		//   }
		//   catch (System.Exception e)
		//   {
		//      if (e.GetType().ToString() == "System.IO.IOException")
		//      {
		//         Owner.PrintError("ERRORc: " + e);
		//         Close();
		//      }
		//      else
		//      {
		//         Owner.PrintError("ERRORc: " + e);
		//      }
		//      return false;
		//   }
		//   return true;
		//}
		public bool Send(byte[] SendBytes, int length)
		{
			if (TcpClient == null || Connected == false)
			{
				//Owner.PrintError("Note: Already Disconnected. Do not call me.");
				return false;
			}
			
			try
			{
				NetworkStream sendStream = TcpClient.GetStream();
				sendStream.Write(SendBytes, 0, length);
				sendStream.Flush();
			}
			catch (System.Exception e)
			{
				if (e.GetType().ToString() == "System.IO.IOException")
				{
					Owner.Owner.Owner.PrintError("ERRORc1: " + e);
					//Close();
				}
				else if (e.GetType().ToString() == "System.ObjectDisposedException")
				{
					Owner.Owner.Owner.PrintError("ERRORc2: " + e);
				}
				else
				{
					Owner.Owner.Owner.PrintError("ERRORc3: " + e);
				}
				return false;
			}
			return true;
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

	}
}