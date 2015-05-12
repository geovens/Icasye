// TCP-CSharp - a C# class library for TCP affairs, including client/socket and server/listener manager. by Nai Weizhi

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TCP_CSharp
{
	public delegate void ClientGotEvent(string name, string listenername);
	public delegate void MessageGotEvent(string srcname, TCPMessage message);
	public delegate void DisconnectEvent(string srcname);

	/// <summary>
	/// the main class to manage all TCP affairs
	/// </summary>
	public class TCPCSharp
	{
		internal TCPMnge Mnge;

		/// <summary>
		/// whether to print log
		/// </summary>
		public bool ToPrintLog = false;
		/// <summary>
		/// whether to print error info
		/// </summary>
		public bool ToPrintError = true;
		/// <summary>
		/// an event triggered when any current listener got a new client connection
		/// </summary>
		public event ClientGotEvent OnClientGot;
		/// <summary>
		/// an event  triggered when TCP data is received by any current connection
		/// </summary>
		public event MessageGotEvent OnMessageGot;
		/// <summary>
		/// an event triggered and only triggered when a connection is forcely closed BY THE OTHER SIDE, whether server or client
		/// </summary>
		public event DisconnectEvent OnForcedDisconnect;

		public TCPCSharp()
		{
			Mnge = new TCPMnge(this);
		}

		/// <summary>
		/// connect to some server.
		/// </summary>
		/// <param name="name">user defined name of the connection</param>
		/// <param name="host">address of the server</param>
		/// <param name="port">port</param>
		/// <returns>0=success, 10060=timeout, 10061=rejected by server, ect. see windows socket error code</returns>
		public int Connect(string name, string host, int port)
		{
			if (name == null || name == "")
				return -9;
			return Mnge.Connect(name, host, port);
		}
		/// <summary>
		/// disconnect a connection
		/// </summary>
		/// <param name="name">name of the connection to disconnect</param>
		/// <returns></returns>
		public bool Disconnect(string name)
		{
			if (name == null || name == "")
				return false;
			return Mnge.Disconnect(name);
		}
		/// <summary>
		/// raname a connection
		/// </summary>
		/// <param name="oldname">old name</param>
		/// <param name="newname">new user defined name</param>
		/// <returns></returns>
		public bool RenameConnection(string oldname, string newname)
		{
			if (oldname == null || oldname == "" || newname == null || newname == "")
				return false;
			return Mnge.RenameConnection(oldname, newname);
		}
		/// <summary>
		/// get a list of names of currently connected connections
		/// </summary>
		/// <returns></returns>
		public List<string> GetConnectionList()
		{
			return Mnge.GetConnectionList();
		}
		/// <summary>
		/// get the remote address of a connection
		/// </summary>
		/// <param name="name">name of the connection to inquire</param>
		/// <returns></returns>
		public string GetRemoteAddress(string name)
		{
			if (name == null || name == "")
				return null;
			return Mnge.GetRemoteAddress(name);
		}
		/// <summary>
		/// get the remote port of a connection
		/// </summary>
		/// <param name="name">name of the connection to inquire</param>
		/// <returns></returns>
		public int GetRemotePort(string name)
		{
			if (name == null || name == "")
				return 0;
			return Mnge.GetRemotePort(name);
		}

		/// <summary>
		/// add a listener on a specified local port
		/// </summary>
		/// <param name="name">user defined name of the listener</param>
		/// <param name="port">port to listen</param>
		/// <returns>0=success, 10048=port occupied, ect. see windows socket error code</returns>
		public int AddListen(string name, int port)
		{
			if (name == null || name == "")
				return -9;
			return Mnge.AddListen(name, port);
		}
		/// <summary>
		/// stop a listener
		/// </summary>
		/// <param name="name">name of the listener to stop</param>
		/// <returns></returns>
		public bool StopListen(string name)
		{
			if (name == null || name == "")
				return false;
			return Mnge.RemoveListen(name);
		}
		/// <summary>
		/// stop a listener
		/// </summary>
		/// <param name="port">port to stop listening on</param>
		/// <returns></returns>
		public bool StopListen(int port)
		{
			return Mnge.RemoveListen(port);
		}
		/// <summary>
		/// get a list of names of current listeners
		/// </summary>
		/// <returns></returns>
		public List<string> GetListenerList()
		{
			return Mnge.GetListenerList(); ;
		}

		/// <summary>
		/// send TCP data to a connection
		/// </summary>
		/// <param name="destname">name of the connection to send data to</param>
		/// <param name="message">data to send, in form of class TCPMessage</param>
		/// <returns></returns>
		public bool Send(string destname, TCPMessage message)
		{
			if (destname == null || destname == "")
				return false;
			return Mnge.Send(destname, message.Data, message.Length);
		}

		/// <summary>
		/// register a new event of message got for a specified connection 
		/// </summary>
		/// <param name="name">name of the connection to set</param>
		/// <param name="mge">the function to handle the event</param>
		/// <returns></returns>
		public bool SetSpeMessageGotEvent(string name, MessageGotEvent mge)
		{
			return Mnge.SetSpeMessageGotEvent(name, mge);
		}

		internal void ClientGot(string name, string listenefrom)
		{
			if (OnClientGot != null)
				OnClientGot(name, listenefrom);
		}
		internal void MessageGot(string srcname, byte[] bytes, int length)
		{
			// firstly, trigger an event for any message got by any connection
			TCPMessage message = new TCPMessage();
			message.Length = length;
			bytes.CopyTo(message.Data, 0);
			if (OnMessageGot != null)
				OnMessageGot(srcname, message);

			// secondly, trigger an event only for the specified connection
			MessageGotEvent mge = Mnge.GetSpeMessageGotEvent(srcname);
			if (mge != null)
				mge(srcname, message);
		}
		internal void DisconnectHappened(string srcname)
		{
			if (OnForcedDisconnect != null)
				OnForcedDisconnect(srcname);
		}
		internal void PrintLog(string log)
		{
			if (ToPrintLog)
				Console.WriteLine(log);
		}
		internal void PrintError(string log)
		{
			if (ToPrintError)
				Console.WriteLine(log);
		}
	}
}

