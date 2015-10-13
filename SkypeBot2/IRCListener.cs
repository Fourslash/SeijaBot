using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatSharp;
using System.Threading;

namespace SkypeBot2
{
    class IRCListener
    {
        const string CHANNELNAME = "#fourslash";
        static IrcClient client;
        static IrcChannel channel
        {
            get
            {
                if (client != null)
                    return client.Channels.FirstOrDefault(x => x.Name == CHANNELNAME);
                else
                    return null;
            }
        }
        //private static IRCListener instance;

        static void joinServer()
        {
            try
            {
                client.JoinChannel("#fourslash");
                SeijaHelper.SendToMaster("Joined IRC server ");
            }
            catch (Exception ex)
            {
                SeijaHelper.SendToMaster("Cant join IRC server " + ex.Message);
            }
        }

        private static void IRCListenerInit() 
        {
            try
            {
                client = new IrcClient("irc.twitch.tv", new IrcUser("seijabot", "seijabot", "oauth:iggb37o4zs4eq2jd5zu5qtsemrjbcb"));
                client.ConnectionComplete += (s, ex) =>
                {                  
                    //SeijaHelper.SendToMaster("IRC INITED");
                    joinServer();
                };
                client.ChannelMessageRecieved += (s, ex) =>
                {
                   // SeijaHelper.SendToMaster(ex.PrivateMessage.Message+":"+ ex.PrivateMessage.User.Nick);
                    SeijaCommander.Seija.ProcessMSG(new IRCProvider(ex.PrivateMessage.Message, ex.PrivateMessage.User.Nick));
                    //SendMessage("test");
                };
                
                client.ConnectAsync();
                 for (int i=0 ;i<10; i++)
                {
                    if (client == null)
                    {
                        if (i >= 5)
                            throw new Exception("client not connected after 5 attempts");
                        SeijaHelper.SendToMaster("client is not inited on "+(i+1)+" try");
                        Thread.Sleep(500);
                    }
                    else
                        break;
                }
                
            }
            catch (Exception ex)
            {
                SeijaHelper.SendToMaster("Cant init IRC listener: " + ex.Message);
            }

        }

        public static void init()
        {
            try
            {
                IRCListenerInit();
            }
            catch (Exception ex)
            {
                SeijaHelper.SendToMaster("wut: " + ex.Message);
            }
        }
        //public static IRCListener Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new IRCListener();
        //        }
        //        return instance;
        //    }
        //    set
        //    {
        //        instance = new IRCListener();
        //    }
        //}
        public static void SendMessage(string message)
        {
            try
            {
                if (client == null)
                    SeijaHelper.SendToMaster("Trying to send message to twitch chat, but irc client is null");
                else if (channel == null)
                    SeijaHelper.SendToMaster("Trying to send message to twitch chat, but irc channel is null");
                else
                    channel.SendMessage(message);
            }
            catch (Exception ex)
            {
                SeijaHelper.SendToMaster("Cant send message to twitch: " + ex.Message);
            }
        }

    }



}
