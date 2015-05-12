### Icasye

Icasye is a .NET library for LAN network communication. The purpose of this library is to greatly simplify the implementation of LAN network communication in .NET projects for those who know nothing about and do not want to learn TCP/IP.

Here is an example of how to send a string "Hello!" from David's computer to Tom's computer.

On David's computer, the following code sends the string:

    IcasyeClient IcasyeClient = new IcasyeClient();
	IcasyeClient.SetMyName("David");
	IcasyeClient.GoOnline();
	
	IcasyeMsg msg = new IcasyeMsg();
	msg.SetString("Hello!");
	IcasyeClient.Send("Tom", msg);
	
and on Tom's computer, the following code receives the string:

    IcasyeClient IcasyeClient = new IcasyeClient();
	IcasyeClient.SetMyName("Tom");
	IcasyeClient.OnMessageGot += OnDataGot;
	IcasyeClient.GoOnline();
	
	private void OnDataGot(string srcname, IcasyeMsg msg)
	{
		Console.WriteLine("I got a message from " + srcname + ": " + msg.ToString());
	}

The message transmission should be successful as long as David's computer and Tom's computer are in the same local network. No need to know what is IP address, TCP protocal, Socket, etc. The Icasye library handles all the low-level common works, including port opening, client discovering, socket establishing, data transmitting, broken packet reforming, automatic reconnecting after accidental disconnections, etc.
	
Here are also a few side projects, including a library for general-purpose TCP communication, a library for general-purpose UDP communication, and tools to debug network communications. The side projects are used by Icasye, however they can also be used solo for general network communication tasks.

I use these libraries in a few of my other small but serious projects to manage network communications, and they are working very well by now.
