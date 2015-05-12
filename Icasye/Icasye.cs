// Icasye - a C# class library for oversimplified TCP communication by Nai Weizhi

using System;
using System.Collections.Generic;
using System.Text;
using TCP_CSharp;


namespace Icasye
{
	public delegate void MessageGotEvent(string srcname, IcasyeMsg message);

	/// <summary>
	/// Icasye client manager
	/// </summary>
	public class IcasyeClient
	{
		internal Mnge Mnge;
		internal bool ToPrintLog = false;
		public bool ToPrintError = true;

		/// <summary>
		/// an event triggered when getting TCP data from any client
		/// </summary>
		public event MessageGotEvent OnMessageGot;
		/// <summary>
		/// a setting of whether to broadcast information of myself to all other clients in the lacal network to let them know my existence
		/// </summary>
		public bool ToSendCard = true;
		/// <summary>
		/// a setting of whether to receive broadcasted information from other clients to know there existence
		/// </summary>
		public bool ToReceiveCard = true;

		public IcasyeClient()
		{
			Mnge = new Mnge(this);
		}

		/// <summary>
		/// call this function to enable output of log info of Icasye, TCP-CSharp, UDP-CSharp
		/// </summary>
		public void SetToPrintLog()
		{
			ToPrintLog = true;
			Mnge.SetToPrintLog();
		}
		/// <summary>
		/// set the name of self Icasye client. be careful not to conflict with other clients
		/// </summary>
		/// <param name="name">my name</param>
		/// <returns></returns>
		public bool SetMyName(string name)
		{
			return Mnge.SetMyName(name);
		}
		/// <summary>
		/// get online into Icasye network
		/// </summary>
		/// <returns></returns>
		public bool GoOnline()
		{
			return Mnge.GoOnline();
		}
		/// <summary>
		/// get offline from Icasye network
		/// </summary>
		/// <returns></returns>
		public bool GoOffline()
		{
			return Mnge.GoOffline();
		}
		/// <summary>
		/// send TCP data to another client
		/// </summary>
		/// <param name="name">destination client name</param>
		/// <param name="msg">the TCP data to send</param>
		/// <returns></returns>
		public bool Send(string name, IcasyeMsg msg)
		{
			return Mnge.Send(name, msg);
		}
		/// <summary>
		/// get names of all current online clients
		/// </summary>
		/// <returns></returns>
		public List<string> GetClientList()
		{
			return Mnge.GetClientList();
		}
		/// <summary>
		/// check if a client with the specific name is online
		/// </summary>
		/// <param name="name">name of client to check</param>
		/// <returns></returns>
		public bool CheckClientOnline(string name)
		{
			return Mnge.CheckClientOnline(name);
		}
		/// <summary>
		/// get the ip address of a client
		/// </summary>
		/// <param name="name">name of client to get address of</param>
		/// <returns></returns>
		public string GetClientAddress(string name)
		{
			return Mnge.GetClientAddress(name);
		}
		/// <summary>
		/// add an event which will be triggered when getting TCP data from a specific client 
		/// </summary>
		/// <param name="name">name of the specific client</param>
		/// <param name="mge">the function to handle the event</param>
		/// <returns></returns>
		public bool SetSpecificMessageGotEvent(string name, MessageGotEvent mge)
		{
			return Mnge.SetSpeMessageGotEvent(name, mge);
		}
		/// <summary>
		/// add a TCP connection to a host that is not an Icasye client
		/// </summary>
		/// <param name="name">user defined name of the connection</param>
		/// <param name="address">address if the host</param>
		/// <param name="port">port to connect</param>
		/// <returns></returns>
		public bool AddGeneralConnectionManually(string name, string address, int port)
		{
			return Mnge.AddGeneralConnection(name, address, port);
		}
		/// <summary>
		/// connect to a remote Icasye client that is not in the same local network
		/// </summary>
		/// <param name="address">address of the client. will use the default port 5301</param>
		/// <returns></returns>
		public bool AddIcasyeClientManually(string address)
		{
			return Mnge.AddIcasyeClientManually(address);
		}
		/// <summary>
		/// block a Icasye client with the specific name. this will only hide it from you, not affecting any TCP behavior in backgruond
		/// </summary>
		/// <param name="name">name of the client to block</param>
		/// <returns></returns>
		public bool Block(string name)
		{
			Mnge.BlockClient(name);
			return true;
		}
		/// <summary>
		/// unblock an Icasye client
		/// </summary>
		/// <param name="name">name of the client to unblock</param>
		/// <returns></returns>
		public bool UnBlock(string name)
		{
			Mnge.UnBlockClient(name);
			return true;
		}
		/// <summary>
		/// delete a manually added client or connection.
		/// you are not able by now to delete/disconnect a client which is not manually added by you.
		/// if you need, try to use BlockClient to hide some client from you, 
		/// when it does not matter that the TCP connection to the bloced one remains live
		/// </summary>
		/// <param name="name">name of the manually added client to delete</param>
		/// <returns></returns>
		public bool DeleteManuallyAdded(string name)
		{
			bool suc = Mnge.DeleteAdded(name);
			return suc;
		}

		/// <summary>
		/// NOTE: this is an unaccomplished, unreliable and buggy function.
		/// get local ip address towards the spacific client. works only on clients which are in the same local network
		/// </summary>
		/// <param name="name">name of the client </param>
		/// <returns></returns>
		public string GetMyAddressFacingClient(string name)
		{
			return Mnge.GetMyAddressFacingClient(name);
		}

		internal void MessageGot(string srcname, IcasyeMsg msg)
		{
			if (OnMessageGot != null)
				OnMessageGot(srcname, msg);
			if (Mnge.GetSpeMessageGotEvent(srcname) != null)
				Mnge.GetSpeMessageGotEvent(srcname)(srcname, msg);
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
