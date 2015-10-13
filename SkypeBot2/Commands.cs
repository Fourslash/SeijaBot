using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using SKYPE4COMLib;
using System.IO;
using Twitch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using HtmlAgilityPack;
using System.Net.Http;
using System.Xml.XPath;
using System.Xml;
using System.Diagnostics;

namespace SkypeBot2
{
   
    public class BotCommand
    {
        string helpString = "helpstring";
        public bool isMoraleCommand = false;
        bool isEnabled = true;
        bool isMasterOnly = false;
        protected Func<string, ChatProvider, bool> commandDelegate;
        static List<string> disabledMessages = new List<string>
        {
            "Команда выключена",
            "Команда отключена",
        };
        static List<string> rejectedMessages = new List<string>
        {
            "У вас недостаточно прав",
            "Вы не мой мастер",
            "Ты не можешь этого сделать, раб (✿-̶●̃益●̶̃)",
        };
        public static List<string> DisabledMessages
        {
            get
            {
                return disabledMessages;
            }
            set
            {
                disabledMessages = value;
            }
        }
        public static List<string> RejectedMessages
        {
            get
            {
                return rejectedMessages;
            }
            set
            {
                rejectedMessages = value;
            }
        }

        string commandName;
      
        public string CommandName
        {
            get
            {
                return commandName;
            }
             set
            {
                commandName=value;
            }
        }       
        public string HelpString
        {
            get
            {
                return helpString;
            }
            set 
            {
                helpString = value;
            }
        }
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
            }
        }
        public bool IsMasterOnly
        {
            get
            {
                return isMasterOnly;
            }
            set
            {
                isMasterOnly = value;
            }
        }

        public static void SerialazeSC()
        {
            string path = @"Settings\Seija_commands\A_disabled_msgs.ext";
            if (!File.Exists(path))
                using (File.Create(path)) ;
            var serializer = new XmlSerializer(BotCommand.disabledMessages.GetType());
            var sw = new StreamWriter(path);
            serializer.Serialize(sw, BotCommand.disabledMessages);
            sw.Close();

            path = @"Settings\Seija_commands\A_rejected_msgs.ext";
            if (!File.Exists(path))
                using (File.Create(path)) ;
            serializer = new XmlSerializer(BotCommand.rejectedMessages.GetType());
            sw = new StreamWriter(path);
            serializer.Serialize(sw, BotCommand.rejectedMessages);
            sw.Close();
        }
        public void Serialaze()
        {
            string path = @"Settings\Seija_commands\c_" + CommandName + @".ext";
            if (!File.Exists(path))
                using (File.Create(path)) ;
            var serializer = new XmlSerializer(this.GetType());
            var sw = new StreamWriter(path);
            serializer.Serialize(sw, this);
            sw.Close();
        }

        public void update(BotCommand temp)
        {
            helpString = temp.helpString;
            isEnabled = temp.isEnabled;
            isMasterOnly = temp.isMasterOnly;
        }

        
        public static void DeSerialaze(BotCommand cmd)
        {
            BotCommand temp = null;
            try
            {

                string path = @"Settings\Seija_commands\c_" + cmd.CommandName + @".ext";
                if (File.Exists(path))
                {
                   
                    var stream = new StreamReader(path);
                    if (stream.BaseStream.Length != 0)
                    {
                        var ser = new XmlSerializer(cmd.GetType());
                        if (cmd.GetType() == typeof(BotCommand))
                        {
                            temp = (BotCommand)ser.Deserialize(stream);
                            cmd.update(temp);
                        }
                        else if (cmd.GetType() == typeof(MoraleCommand))
                        {
                            temp = (MoraleCommand)ser.Deserialize(stream);
                            ((MoraleCommand)cmd).updateM((MoraleCommand)temp);
                        }
                        

                    }
                    stream.Close();
                }
                
            }
                
            catch (Exception ex)
            {
                string e = ex.Message;
            }
        }
        public static void DeSerialazeSC()
        {
            string path = @"Settings\Seija_commands\A_disabled_msgs.ext";
            var stream = new StreamReader(path);
            if (stream.BaseStream.Length != 0)
            {
                var ser = new XmlSerializer(BotCommand.disabledMessages.GetType());
                BotCommand.disabledMessages = (List<string>)ser.Deserialize(stream);
            }
            stream.Close();

            path = @"Settings\Seija_commands\A_rejected_msgs.ext";
            stream = new StreamReader(path);
            if (stream.BaseStream.Length != 0)
            {
                var ser = new XmlSerializer(BotCommand.rejectedMessages.GetType());
                BotCommand.rejectedMessages = (List<string>)ser.Deserialize(stream);
            }
            stream.Close();
        }

       
        public bool TryExecute(string ar1, ChatProvider provider)
        {
            if (isEnabled == true)
            {
                if (isMasterOnly == true && provider.senderName.ToLower() != SeijaCommander.Settings.Values.masterName.ToLower())
                {
                    provider.SendMessage(SeijaHelper.GetRandomMessage(rejectedMessages));
                    return false;
                }
                else
                    return commandDelegate(ar1, provider);
            }
            else
            {
                provider.SendMessage(SeijaHelper.GetRandomMessage(disabledMessages));
                return false;
            }
           
        }
        public BotCommand(string name, Func<string, ChatProvider, bool> del)
        {
            commandDelegate=del;
            commandName=name;
        }
        public BotCommand()
        {
            
        }
    }

    public class MoraleCommand : BotCommand 
    {
        System.Windows.Threading.DispatcherTimer restTimer;
        List<string> fatigueMessages = new List<string>
        {
            "Я слишком устала",
            "Не хочу",
        };
        public List<string> FatigueMessages
        {
            get
            {
                return fatigueMessages;
            }
            set
            {
                fatigueMessages = value;
            }
        }
        Int64 restMS = 10000000 * 10;
        public Int64 RestMM
        {
            get
            {
                return restMS;
            }
            set
            {
                restMS = value;
            }
        }
        int morale = 7;
        public void updateM(MoraleCommand temp)
        {
            base.update(temp);
            restMS = temp.restMS;
            fatigueMessages = temp.fatigueMessages;
            morale = temp.morale;
        }
        public int Morale
        {
            get
            {
                return morale;
            }
            set
            {
                morale = value;
            }
        }
        public MoraleCommand(string name, Func<string, ChatProvider, bool> del)
            : base(name, del)
        {
            restTimer = new System.Windows.Threading.DispatcherTimer();
            restTimer.Interval = new TimeSpan(restMS);
            restTimer.Tick += RestTick;
            isMoraleCommand = true;
        }
        public MoraleCommand()
        {
            restTimer = new System.Windows.Threading.DispatcherTimer();
            restTimer.Interval = new TimeSpan(restMS);
            restTimer.Tick += RestTick;
            isMoraleCommand = true;
        }
        public bool TryExecute(string ar1, ChatProvider provider)
        {
            if (morale > 0)
            {
                morale--;
                restTimer.Start();
                return base.TryExecute(ar1, provider);
                //commandDelegate(ar1, msg);
            }
            else
            {
                provider.SendMessage(SeijaHelper.GetRandomMessage(fatigueMessages));
                return false;
            }
        }
        void RestTick(object sender, EventArgs e)
        {
            morale++;
            if (morale >= 10)
                restTimer.Stop();
        }
    }

}