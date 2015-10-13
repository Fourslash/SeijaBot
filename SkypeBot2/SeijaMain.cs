using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Codeplex.Data;

namespace SkypeBot2
{
    public partial class Seija
    {
        List<BotCommand> commands;
        const string dotakonfa = "#splasher-_-/$e36c265204653a65";
        const string homeconfa = "#splasher-_-/$omgwtfgglol;7fa80f21182dcf70";
        const string kantaekonfa = "#nekonyak/$88001bf7f531a99e";
        //Skype skype;
        //ChatMessage currentMessage;
        string askedName;

        bool IsYoutubeSilent = false;
        int shushTicks = 0;

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer shushTimer = new System.Windows.Threading.DispatcherTimer();

        List<Conf> Confs = new List<Conf>();
        public Seija(/*Skype sk*/)
        {
            //skype = sk;
            try
            {
                
                Initial();

            }
            catch (Exception ex)
            {
                SeijaCommander.skype.SendMessage(SeijaCommander.Settings.Values.masterName, ex.Message);
            }
        }
        private void Initial()
        {
            commands = new List<BotCommand>
            {
                new BotCommand("donger_add",cmd_Add_donger),
                new BotCommand("pasta_add",cmd_Add_pasta),
                new BotCommand("kick",cmd_Kick),
                new MoraleCommand("seija",cmd_Seija),
                new BotCommand("command_add",cmd_Add),
                new MoraleCommand("goku",cmd_Goku),
                new BotCommand("uptime",cmd_Uptime),
                new MoraleCommand("jaraxxus",cmd_Jaraxxus),
                new BotCommand("rng",cmd_Rng),
                new BotCommand("update_kicklist",cmd_UpdateKicklist),
                new BotCommand("streams",cmd_Streams),
                new MoraleCommand("кораблей",cmd_SHIP),
                new BotCommand("hug",cmd_Hug),
                new MoraleCommand("bnd",cmd_BND),
                new MoraleCommand("donger",cmd_Donger),
                new MoraleCommand("pasta",cmd_Pasta),
                new BotCommand("help_please",cmd_Help),
                new MoraleCommand("db",cmd_db),
                new MoraleCommand("db_find",cmd_db_find),
                new BotCommand("on",cmd_On),
                new BotCommand("off",cmd_Off),
                new BotCommand("stop",cmd_Stop),
                new BotCommand("help_add",cmd_Help_add),
                new BotCommand("set_enabled",cmd_set_enabled),
                new BotCommand("set_master_only",cmd_set_master_only),
                //new BotCommand ("update_all",cmd_update_all),
                new BotCommand ("battle",cmd_battle),
                new BotCommand ("get_chatname",cmd_GetChatname),
                new BotCommand ("leave",cmd_leave),
                new BotCommand ("dota_team_add",cmd_Add_team),
                new BotCommand ("dota_team_delete",cmd_Delete_team),
                new BotCommand ("dota_show_teams",cmd_Show_teams),
                new BotCommand ("Tobi",cmd_Tobi),
                new BotCommand ("server_status",cmd_ServerStatus),
                new BotCommand ("update_all",cmd_UpdateAll),
                new BotCommand ("dice", cmd_Dice),
                new BotCommand ("restart", cmd_Restart),
               // new BotCommand("NewChat",cmd_AddConf),
               new BotCommand ("Terezi", cmd_Terezi),
               new BotCommand ("TereziStart", cmd_TereziUP),
               new BotCommand ("TereziDown", cmd_TereziDown),
               new BotCommand ("pasta_find", cmd_PastaFind),

            };
            BotCommand.DeSerialazeSC();
            foreach (BotCommand cmd in commands)
            {
                BotCommand.DeSerialaze(cmd);
            }
            
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            shushTimer.Tick += shushTimer_Tick;
            shushTimer.Interval = new TimeSpan(0, 0, 1);
            
        }
        void startShushTimer()
        {
            shushTicks = 0;
            shushTimer.Start();
        }

        void shushTimer_Tick(object sender, EventArgs e)
        {
            shushTicks += 1;
            if (shushTicks>=60*5)
            {
                IsYoutubeSilent = false;
                shushTimer.Stop();
            }
        }
        public void sendMOTD()
        {
            List<string> motd = new List<string>
            {
                @"/me версии ""TWITCH PogChamp"" запущена",
                @"/me запущена и готова спамить (✿◕⁀◕)",
                @"Опять перезапуск? http://d.d.doushio.com/nagashi/",
                @"From the frozen pool, I rise ಠ▃ಠ",
                @"Its time! https://youtu.be/anwy2MPT5RE?t=11s",



            };

            SeijaCommander.skype.get_Chat(homeconfa).SendMessage(SeijaHelper.GetRandomMessage(motd));
        }


        public void ProcessMSG(ChatProvider provider)
        {

            if (provider is SkypeProvider)
            {
               // currentMessage = msg;
                TimeSpan ts = provider.sentTime - DateTime.Now;
                if (ts.TotalMinutes > 5)
                {
                    return;
                }
            }
            if (provider.senderName.ToLower() != SeijaCommander.Settings.Values.botName.ToLower())
            {
                try
                {
                    if (ProcessCommand(provider) == true)
                    {
                        //cnf.processNext();
                        return;
                    }
                    //else if (no_copypasterino() == true)
                    //    return;
                    else if (isAsked(provider) == true)
                    {
                        if (SeijaAsk(provider) == true)
                            return;
                        if (SHUSH(provider) == true)
                            return;


                    }
                    else if (provider.inMessageText.ToLower().Contains("youtu") && provider.chatName != kantaekonfa)
                    {
                        /*string answ=*/
                        checkYoutube(provider);
                       //if (answ != string.Empty)
                       //    msg.Chat.SendMessage(answ);
                    }
                }
                catch (Exception ex)
                {
                    //msg.Chat.SendMessage("Error: "+ex.Message);
                    SeijaCommander.skype.SendMessage(SeijaCommander.Settings.Values.masterName, provider.chatName + "\n" + ex.Message);
                    SeijaHelper.write_log(ex.Message);
                }
                finally
                {
                    //cnf.processNext();
                }
            }
        }
        private void checkYoutube(ChatProvider provider)
        {

            if (provider.senderName.ToLower() == SeijaCommander.Settings.Values.masterName.ToLower() && IsYoutubeSilent == true)
            {
                IsYoutubeSilent = false;
                shushTimer.Stop();
                return;
            }


            string v=getVideoId(provider.inMessageText);
            if (v == string.Empty)
                return;
            string answ= getVideoName(v);
            if (answ != string.Empty)
                provider.SendMessage(answ,provider.chatName); 
            return;

        }

        private bool SHUSH(ChatProvider provider)
        {
            if (provider.senderName.ToLower() != SeijaCommander.Settings.Values.masterName.ToLower())
                return false;

            List<string> SHUSHes = new List<string> 
            {
                "щщ",
                "не пали",
                "не говори",
                "секрет",
                "тайн",
                "цыц",
                "молч",
                "любопыт",
                "посмотр",
                "шуш",
                "щущ",
            };
            foreach (string str in SHUSHes)
            {
                if (provider.inMessageText.ToLower().Contains(str))
                {
                    IsYoutubeSilent = true;
                    startShushTimer();
                    return true;
                }
            }
            return false;


        }

        private string getVideoName(string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string key = "AIzaSyAE7ny5P9R0RPoY2DaqzLKedmPmcLz9kdk";
                    string url = string.Format("https://www.googleapis.com/youtube/v3/videos?id={0}&key={1}&part=snippet,contentDetails", id, key);
                    client.BaseAddress = new Uri(url);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "");
                    var result = client.SendAsync(request).Result;
                    string resultContent = result.Content.ReadAsStringAsync().Result;
                    var jsn = DynamicJson.Parse(resultContent);
                    //return jsn.items[0].snippet.title;
                    string channelTitle = jsn.items[0].snippet.channelTitle;
                    string duration = getDuration(jsn.items[0].contentDetails.duration);
                    string title = jsn.items[0].snippet.title;
                    string res = string.Format("\"{0}\" by {1} {2}", title, channelTitle, duration);
                    return res;

                }
            }
            catch (Exception ex)
            {
               // SeijaCommander.skype.SendMessage(SeijaCommander.Settings.Values.masterName,  ex.Message);
                return string.Empty;
            }
        }
        string getDuration(string str)
        {
            try
            {
                //"duration": "PT1H38M25S"
                string temp = str.Remove(0, 2);

                int hIndex = temp.IndexOf("H");
                int hours = 0;
                if (hIndex != -1)
                {
                    string h = temp.Substring(0, temp.IndexOf("H"));
                    temp = temp.Remove(0, temp.IndexOf("H") + 1);
                    hours = Convert.ToInt32(h);
                }

                int mIndex = temp.IndexOf("M");
                int minutes = 0;
                if (mIndex != -1)
                {
                    string m = temp.Substring(0, temp.IndexOf("M"));
                    temp = temp.Remove(0, temp.IndexOf("M") + 1);
                    minutes = Convert.ToInt32(m);
                }


                int sIndex = temp.IndexOf("S");
                int seconds = 0;
                if (sIndex != -1)
                {
                    string s = temp.Substring(0, temp.IndexOf("S"));
                    // temp.Remove(0, temp.IndexOf("H") + 1);
                    seconds = Convert.ToInt32(s);
                }


                TimeSpan ts = new TimeSpan(hours, minutes, seconds);
                return ts.ToString(@"c");
            }
            catch (Exception ex)
            {
                return "???";
            }
        }
        private string getVideoId(string link)
        {
            try
            {
                string vidID = string.Empty;
                string testString = link;
                if (testString.Contains("www.youtube.com/watch"))
                {
                    if (testString.IndexOf("?v=") == -1)
                        return string.Empty;
                    string cutString = testString.Substring(testString.IndexOf("?v="));
                    while (cutString.Contains("&") ||
      cutString.Contains(" ") ||
      cutString.Contains("\r") ||
      cutString.Contains("\n"))
                    {
                        int end = cutString.IndexOf("&");
                        if (end == -1)
                        {
                            end = cutString.IndexOf(" ");
                            if (end == -1)
                            {
                                end = cutString.IndexOf("\r");
                                if (end == -1)
                                {
                                    end = cutString.IndexOf("\n");
                                }
                            }
                        }

                        if (end != -1)
                            cutString = cutString.Remove(end);

                    }
                    vidID = cutString.Substring(("?v=").Length, cutString.Length - ("?v=").Length);
                }
                else if (testString.Contains("youtu.be"))
                {
                    string cutString = testString.Remove(0, testString.IndexOf("youtu.be") + ("youtu.be").Length + 1);
                    while (cutString.Contains("?") ||
cutString.Contains(" ") ||
cutString.Contains("\r") ||
cutString.Contains("\n"))
                    {
                        if (cutString.Contains("?"))
                        {
                            cutString = cutString.Remove(cutString.IndexOf("?"));
                        }
                        if (cutString.Contains(" "))
                        {
                            cutString = cutString.Remove(cutString.IndexOf(" "));
                        }
                        if (cutString.Contains("\r"))
                        {
                            cutString = cutString.Remove(cutString.IndexOf("\r"));
                        }
                        if (cutString.Contains("\n"))
                        {
                            cutString = cutString.Remove(cutString.IndexOf("\n"));
                        }
                    }


                    vidID = cutString;
                }
                return vidID;
            }
            catch (Exception ex)
            {

                return string.Empty;
            }
            
        }
        //private void checkForTenshi(object str_obj)
        //{
        //    try
        //    {
        //        string str = (string)str_obj;
        //        List<string> words = new List<string>(str.Split(' ').ToArray());
        //        foreach (string word in words)
        //        {
        //            if (word.Contains("http"))
        //            {
        //                TenshiChecker check = new TenshiChecker(word, SeijaCommander.skype);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SeijaHelper.write_log(ex.Message);
        //    }
        //}

        private bool no_copypasterino(ChatProvider provider) //мне очень стыдно за это функцию но мне слишком лень писать нормальный код
        {
            if (provider.inMessageText.Contains("no copy"))
            {
                string[] cancer = { "(no space)", "(no spaceerino)", "Kappa", "4Head" };
                string temp = provider.inMessageText;
                int t = SeijaHelper.FixedRandom(1, 3);
                if (t == 1)
                    temp += " " + cancer[SeijaHelper.FixedRandom(0, cancer.Length)];
                System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(1500, 3500));
                provider.SendMessage(temp,provider.chatName);
                return true;
            }
            else if (provider.inMessageText.Contains("но копи"))
            {
                string[] cancer = { "(но спейс)", "(но спейсерино)", "Kappa", "4Head" };
                string temp = provider.inMessageText;
                int t = SeijaHelper.FixedRandom(1, 3);
                if (t == 1)
                    temp += " " + cancer[SeijaHelper.FixedRandom(0, cancer.Length)];
                System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(1500, 3500));
                provider.SendMessage(temp, provider.chatName);
                return true;
            }
            return false;
        }


        private bool isAsked(ChatProvider provider)
        {
            askedName = string.Empty;
            bool isRight = false;
            foreach (string name in SeijaCommander.Settings.Values.botNames)
                if (provider.inMessageText.Contains(name))
                {
                    askedName = name;
                    isRight = true;
                }
            return isRight;
        }


        private bool SeijaAsk(ChatProvider provider)
        {
            if (provider.inMessageText.IndexOf(",") == (provider.inMessageText.IndexOf(askedName) + askedName.Length) && provider.inMessageText.Contains("?"))
            {
                //if (currentMessage.Sender.Handle == "zoljiin")
                //{
                //    currentMessage.Chat.SendMessage(@"http://imgur.com/ygVipUl.jpg");
                //}
                //else
                //{

                    //int num = SeijaHelper.FixedRandom(0, 2);
                    //if (num==0)
                    //    currentMessage.Chat.SendMessage(@"http://imgur.com/F6hu49A.jpg");
                    //else if (num==1)
                    //    currentMessage.Chat.SendMessage(@"http://imgur.com/Ct47ALh.jpg");
                    //else
                    //    currentMessage.Chat.SendMessage(@"http://imgur.com/Ct47ALh.jpg");
                string link,resultString=string.Empty;
                int num = SeijaHelper.FixedRandom(0, 3);
                if (num == 0)
                    link=@"http://imgur.com/F6hu49A.jpg";
                else if (num == 1)
                    link=@"http://imgur.com/Ct47ALh.jpg";
                else
                    link = @"http://i.imgur.com/it2cI1A.jpg";
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://www.googleapis.com/urlshortener/v1/url?key=AIzaSyAE7ny5P9R0RPoY2DaqzLKedmPmcLz9kdk ");

                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "");
                        request.Content = new StringContent("{\"longUrl\": \"" + link + "&" + SeijaHelper.FixedRandom(1, 10000) + "\"}",
                                                            Encoding.UTF8,
                                                            "application/json");
                        var result = client.SendAsync(request).Result;
                        string resultContent = result.Content.ReadAsStringAsync().Result;
                        //Console.WriteLine(resultContent);
                        JObject res = JObject.Parse(resultContent);
                        resultString = res["id"].ToString();
                    }
                }
                    catch (Exception ex)
                {
                    resultString = link;
                }
                finally
                {
                    provider.SendMessage(resultString);
                }
                return true;
            }
            return false;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            SeijaHelper.ticks++;

        }

        private bool ProcessCommand(ChatProvider provider)
        {
            string message = provider.inMessageText;
            if (message.IndexOf(SeijaCommander.Settings.Values.commandSymvol) == 0)
            {
                message = message.Remove(0, SeijaCommander.Settings.Values.commandSymvol.Length);
                string command, arg;
                if (message.IndexOf(" ") != -1)
                {
                    command = message.Remove(message.IndexOf(" "));
                    arg = message.Remove(0, message.IndexOf(" ") + 1);
                }
                else
                {
                    command = message;
                    arg = "";
                }
                BotCommand cmd = commands.Find(x => x.CommandName == command);
                if (cmd == null)
                    return ProcessSimpleCommand(message, provider);
                else
                {
                    if (cmd.isMoraleCommand == true)
                        return ((MoraleCommand)cmd).TryExecute(arg, provider);
                    else
                        return cmd.TryExecute(arg, provider);
                }
            }
            return false;
        }
        private bool ProcessSimpleCommand(string command, ChatProvider provider)
        {
            string res = "";
            var keys = new List<string>(SeijaCommander.SimpleCommands.Pairs.Keys);
            foreach (string k in keys)
            {
                if (command == k)
                    res = SeijaCommander.SimpleCommands.Pairs[k].Clone().ToString();
            }
            //res = res.Replace(@"\\", @"\");
            //res = res.Replace("\\n", "\n");
            if (res == "")
                return false;
            provider.SendMessage(res);
            return true;
        }


    }
}
