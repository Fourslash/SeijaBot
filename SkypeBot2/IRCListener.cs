using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatSharp;
using System.Threading;
using System.IO;

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
        static void writeLog(string msg,string filename)
        {
            try
            {
                using (System.IO.StreamWriter file = File.AppendText(@"Settings\" + filename))
                {
                    file.WriteLine(DateTime.Now.ToString("u") + " " + msg);
                }
            }
            catch (Exception ex)
            {
                SeijaHelper.SendToMaster("Cantwrite irc log: " + ex.Message);
            }
        }
        private static void IRCListenerInit() 
        {
            try
            {
                client = new IrcClient("irc.twitch.tv", new IrcUser("seijabot", "seijabot", SeijaCommander.Settings.Values.IRCkey));
                client.ConnectionComplete += (s, ex) =>
                {                  
                    joinServer();
                };
                client.ChannelMessageRecieved += (s, ex) =>
                {
                    SeijaCommander.Seija.ProcessMSG(new IRCProvider(ex.PrivateMessage.Message, ex.PrivateMessage.User.Nick));
                    writeLog(ex.IrcMessage.RawMessage, "IRCchhannelMsgRaw.txt");
                    writeLog(ex.IrcMessage.Command, "IRCchhannelMsgRawCommands.txt");
                    writeLog(ex.PrivateMessage.Message, "IRCchhannelMsg.txt");

                };
                client.UserJoinedChannel += (s, ex) =>
                {
                    if (ex.User.Nick.ToLower() != "seijabot")
                    {
                        string res = "New user detected: " + ex.User.Nick;
                        SeijaHelper.SendToMaster(res);
                        writeLog(res, "IRCusers.txt");
                    }
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
