// UDP-CSharp - a C# class library for UDP affairs by Nai Weizhi

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace UDP_CSharp
{
	/// <summary>
	/// one object of this class presents one stream of UDP data
	/// </summary>
	public class UDPMessage
	{
		/// <summary>
		/// the stream data length in bytes
		/// </summary>
		public Int32 Length;
		/// <summary>
		/// the stream data
		/// </summary>
		public byte[] Data = new byte[50000];

		/// <summary>
		/// fill Data with a string encoded using UTF8
		/// </summary>
		/// <param name="str"></param>
		public void SetString(string str)
		{
			byte[] bytes = 	Encoding.UTF8.GetBytes(str);
			bytes.CopyTo(Data, 0);
			Length = bytes.Length;
		}

		/// <summary>
		/// append Data with a string encoded using UTF8
		/// </summary>
		/// <param name="str"></param>
		public void AppendString(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			bytes.CopyTo(Data, Length);
			Length += bytes.Length;
		}

		/// <summary>
		/// decode Data using UTF8 to get the string. please only use this when you know the Data are all text and are in UTF8 encoding
		/// </summary>
		/// <returns></returns>
		public string GetString()
		{
			return Encoding.UTF8.GetString(Data, 0, Length);
		}
	}
}

