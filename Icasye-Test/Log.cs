using System;
using System.Collections.Generic;
using System.Text;
using Icasye;

namespace Icasye_Test
{
	public class Log
	{
		public string ClientName;
		public string CurrentContain = "";
		public List<LogMessage> Messages = new List<LogMessage>();

		public void AddMessage(string type, IcasyeMsg msg)
		{
			LogMessage aMsg = new LogMessage();
			msg.Data.CopyTo(aMsg.Data, 0);
			aMsg.Length = msg.Length;
			aMsg.Type = type;
			aMsg.CalculateText();
			Messages.Add(aMsg);
		}
	
		//public void RenewTextMessages()
		//{
		//   Messages = "";
		//   for (int i = 0; i < IcasyeMsgs.Count; i++)
		//      if (IcasyeMsgs[i].ToString().Contains(CurrentContain))
		//         AddTextMessage(IcasyeMsgs[i]);
		//}
		public string GetTextMessages(int count)
		{
			//if (contain == CurrentContain)
			//   return Messages;
			//else
			//{
			//   CurrentContain = contain;
			//   RenewTextMessages();
			//   return Messages;
			//}

			string messages = "";
			int index = 0;
			for (int i = 0; i < Messages.Count; i++)
				if (Messages[i].Text.Contains(CurrentContain))
				{
					messages += Messages[i].Text + "\r\n";
					Messages[i].TextIndex = index;
					index++;
				}
				else
				{
					Messages[i].TextIndex = -1;
				}
			return messages;
		}


	}
}
