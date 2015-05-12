// UDP-CSharp - a C# class library for UDP affairs by Nai Weizhi

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace UDP_CSharp
{
	public delegate void MessageGotEvent(string srcname, UDPMessage message);

	/// <summary>
	/// the main class to manage all UDP affairs
	/// </summary>
	public class UDPCSharp
	{
		internal UDPMnge Mnge;

		/// <summary>
		/// whether to print log
		/// </summary>
		public bool ToPrintLog= false;
		/// <summary>
		/// whether to print error info
		/// </summary>
		public bool ToPrintError = true;
		/// <summary>
		/// an event  triggered when UDP data is received by any current connection
		/// </summary>
		public event MessageGotEvent OnMessageGot;

		public UDPCSharp()
		{
			Mnge = new UDPMnge(this);
		}

		/// <summary>
		/// add a remote host as a named destination
		/// </summary>
		/// <param name="name">user defined name of the destination</param>
		/// <param name="address">address</param>
		/// <param name="port">port</param>
		/// <returns></returns>
		public int AddDestination(string name, string address, int port)
		{
			if (name == null || name == "" || address == null || address == "")
				return -9;
			int suc = Mnge.AddDestination(name, address, port);
			return suc;
		}
		/// <summary>
		/// add a named broadcast destination
		/// </summary>
		/// <param name="name">user defined name of the destination</param>
		/// <param name="ip">ip</param>
		/// <param name="bitlength">bit-length of the network prefix</param>
		/// <param name="port">port</param>
		/// <returns></returns>
		public int AddBroadcastDestination(string name, string ip, int bitlength, int port)
		{
			if (name == null || name == "" || ip == null || ip == "")
				return -9;
			int suc = Mnge.AddBroadcastDestination(name, ip, bitlength, port);
			return suc;
		}
		/// <summary>
		/// add a named broadcast destination
		/// </summary>
		/// <param name="name">user defined name of the destination</param>
		/// <param name="ip">ip</param>
		/// <param name="mask">subnet mask</param>
		/// <param name="port">port</param>
		/// <returns></returns>
		public int AddBroadcastDestination(string name, string ip, string mask, int port)
		{
			if (name == null || name == "" || ip == null || ip == "")
				return -9;
			int suc = Mnge.AddBroadcastDestination(name, ip, mask, port);
			return suc;
		}
		/// <summary>
		/// add a listener on a specified local port
		/// </summary>
		/// <param name="name">user defined name of the listener</param>
		/// <param name="port">port to listen</param>
		/// <returns>0=success</returns>
		public int AddListen(string name, int port)
		{
			if (name == null || name == "")
				return -9;
			bool suc = Mnge.AddListen(name, port);
			if (suc)
				return 0;
			else
				return -1;
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
		/// raname a listener
		/// </summary>
		/// <param name="oldname">old name</param>
		/// <param name="newname">new user defined name</param>
		/// <returns></returns>
		public bool RenameListener(string oldname, string newname)
		{
			if (oldname == null || oldname == "" || newname == null || newname == "")
				return false;
			return Mnge.RenameListene(oldname, newname);
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
		/// send UDP data to a remote host
		/// </summary>
		/// <param name="destname">name of the connection to send data to</param>
		/// <param name="message">data to send, in form of class UDPMessage</param>
		/// <returns></returns>
		public bool Send(string destname, UDPMessage message)
		{
			if (destname == null || destname == "")
				return false;
			return Mnge.Send(destname, message.Data, message.Length);
		}

		/// <summary>
		/// register a new event of message got for a specified listener 
		/// </summary>
		/// <param name="name">name of the connection to set</param>
		/// <param name="mge">the function to handle the event</param>
		/// <returns></returns>
		public bool SetSpeMessageGotEvent(string name, MessageGotEvent mge)
		{
			return Mnge.SetSpeMessageGotEvent(name, mge);
		}

	
		internal void MessageGot(string srcname, byte[] bytes, int length)
		{
			// firstly, trigger an event for any message got by any connection
			UDPMessage message = new UDPMessage();
			message.Length = length;
			bytes.CopyTo(message.Data, 0);
			if (OnMessageGot != null)
				OnMessageGot(srcname, message);

			// secondly, trigger an event only for the specified connection
			MessageGotEvent mge = Mnge.GetSpeMessageGotEvent(srcname);
			if (mge != null)
				mge(srcname, message);
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

