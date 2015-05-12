// Icasye - a C# class library for oversimplified TCP communication by Nai Weizhi

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using TCP_CSharp;

namespace Icasye
{
	/// <summary>
	/// one object of this class presents one stream of TCP data
	/// </summary>
	public class IcasyeMsg
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
			byte[] bytes = Encoding.UTF8.GetBytes(str);
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
		/// append another IcasyeMsg
		/// </summary>
		/// <param name="msg">message to append</param>
		public void Append(IcasyeMsg msg)
		{
			for (int i = 0; i < msg.Length; i++)
				Data[Length + i] = msg.Data[i];
			Length += msg.Length;
		}
		/// <summary>
		/// get a clone object of this message
		/// </summary>
		/// <returns></returns>
		public IcasyeMsg Clone()
		{
			IcasyeMsg aMsg = new IcasyeMsg();
			aMsg.Length = Length;
			for (int i = 0; i < Length; i++)
				aMsg.Data[i] = Data[i];
			return aMsg;
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

		static public implicit operator TCPMessage(IcasyeMsg msg)
		{
			TCPMessage aMessage = new TCPMessage();
			aMessage.Length = msg.Length;
			msg.Data.CopyTo(aMessage.Data, 0);
			return aMessage;
		}
		static public implicit operator IcasyeMsg(TCPMessage msg)
		{
			IcasyeMsg aMessage = new IcasyeMsg();
			aMessage.Length = msg.Length;
			msg.Data.CopyTo(aMessage.Data, 0);
			return aMessage;
		}
	}
}

