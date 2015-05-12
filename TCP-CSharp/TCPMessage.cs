// TCP-CSharp - a C# class library for TCP affairs, including client/socket and server/listener manager. by Nai Weizhi

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace TCP_CSharp
{
	/// <summary>
	/// one object of this class presents one stream of TCP data
	/// </summary>
	public class TCPMessage
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
		/// prefix bytes at the beginning
		/// </summary>
		/// <param name="bytes">bytes</param>
		public void PrefixBytes(byte[] bytes)
		{
			for (int i = Length - 1; i >= 0; i--)
			{
				Data[i + bytes.Length] = Data[i];
			}
			bytes.CopyTo(Data, 0);
			Length += bytes.Length;
		}
		/// <summary>
		/// append bytes at the end
		/// </summary>
		/// <param name="bytes">bytes</param>
		public void AppendBytes(byte[] bytes)
		{
			bytes.CopyTo(Data, Length);
			Length += bytes.Length;
		}

		/// <summary>
		/// append another TCPMessage
		/// </summary>
		/// <param name="msg">message to append</param>
		public void Append(TCPMessage msg)
		{
			for (int i = 0; i < msg.Length; i++)
				Data[Length + i] = msg.Data[i];
			Length += msg.Length;
		}

		/// <summary>
		/// decode Data using UTF8 to get the string. please only use this when you know the Data are all text and are in UTF8 encoding
		/// </summary>
		/// <returns></returns>
		public string GetString()
		{
			string str;
			try
			{
				str = Encoding.UTF8.GetString(Data, 0, Length);
			}
			catch
			{
				return "";
			}
			return str;
		}
		/// <summary>
		/// decode Data using UTF8 to get the string. please only use this when you know the Data are all text and are in UTF8 encoding
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return GetString();
		}
	}
}

