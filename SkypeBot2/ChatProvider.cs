using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeBot2
{
    public abstract class ChatProvider
    {
        public string chatName;
        public string senderName;
        public string inMessageText;
        public DateTime sentTime;
        public virtual void SendMessage(string message, string target = "nll") { throw new Exception("???"); }
        //public static virtual void SendMessage(string str);
    }

    public class SkypeProvider:ChatProvider
    {
        public SkypeProvider(SKYPE4COMLib.ChatMessage msg)
        {
            chatName=msg.ChatName;
            senderName=msg.Sender.Handle;
            inMessageText=msg.Body;
            sentTime=msg.Timestamp;
            
        }
        public override void SendMessage(string message, string target="nll")
        {
            if (target == "nll")
                target = chatName;
            SeijaCommander.skype.get_Chat(target).SendMessage(message);
        }
    }

    public class IRCProvider : ChatProvider
    {
        public IRCProvider(string msg, string sender)
        {
            try
            {
                chatName = "TWITCH CHANNEL";
                senderName = sender;
                inMessageText = msg;
            }
            catch (Exception ex)
            {
                SeijaHelper.SendToMaster("Cant init IRC provider: " + ex.Message);
            }
        }


        public override void SendMessage(string message, string target = "nll")
        {
            try
            {
                string pureMessage = message.Replace("\n", "| ");
                IRCListener.SendMessage(pureMessage);
            }
            catch (Exception ex)
            {
                SeijaHelper.SendToMaster("Cant send message from IRC provider: " + ex.Message);
            }
        }
    }

}
