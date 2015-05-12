using System;
using System.Collections.Generic;
using System.Text;
using Icasye;

namespace Icasye_Test
{
	public class LogMessage : IcasyeMsg
	{
		public string Text;
		public int TextIndex;
		public string Type;

		public void CalculateText()
		{
			IcasyeMsg textmsg = this.Clone();
			bool istext = true;
			//for (int i = 0; i < textmsg.Length; i++)
			//{
			//   if (textmsg.Data[i] == 0x0A || textmsg.Data[i] == 0x0D)
			//      textmsg.Data[i] = (byte)0x20;
			//   if (textmsg.Data[i] < 0x20 || textmsg.Data[i] > 0x7E)
			//   {
			//      istext = false;
			//      break;
			//   }
			//}
			try
			{
				Encoding.UTF8.GetString(textmsg.Data);
			}
			catch
			{
				istext = false;
			}
			if (istext)
			{
				Text = Type + ": " + textmsg.ToString().TrimEnd(' ');
			}
			else
			{
				Text = Type + ": " + "(" + textmsg.Length + " bytes)";
			}
		}

		//static public implicit operator LogMessage(IcasyeMsg msg)
		//{
		//   LogMessage aMessage = new LogMessage();
		//   aMessage.Length = msg.Length;
		//   msg.Data.CopyTo(aMessage.Data, 0);
		//   return aMessage;
		//}
	}
}
