using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace SkypeBot2
{

    class Translation
    {
        public Translation(string n)
        {
            Name = n;
            last_online = new DateTime(0);
        }
        public string Name{get; set;}
        public string DisplayName { get; set; }
        bool is_online;
        public bool Is_Online
        {
            get { return is_online; }
            set 
            {
                if (value==true)
                {
                 
                    if (isRecentlyOnline() == false)
                        announce();
                    last_online = DateTime.Now;
                }
                is_online = value;
            }
        }
        string title;
        public string Title
        {
            get { return title; }
            set
            {
                //if (value != title && title != string.Empty && isRecentlyOnline() == true)
                //    SeijaCommander.skype.get_Chat("#splasher-_-/$omgwtfgglol;7fa80f21182dcf70").SendMessage(String.Format("{0} changed stream title to \"{1}\"",DisplayName,value));
                title = value;
            }

        }

        string game;
        string Game
        {
            get { return game; }
            set
            {
                if (value != game && game != string.Empty && isRecentlyOnline() == true)
                    SeijaCommander.skype.get_Chat("#splasher-_-/$omgwtfgglol;7fa80f21182dcf70").SendMessage(String.Format("{0} is now playing \"{1}\"", DisplayName, value));
                game = value;
            }
        }

        DateTime last_online;
        public void UpdateIndo(JObject channel)
        {
            DisplayName = channel["display_name"].ToString();
            Title = channel["status"].ToString();
            Game = channel["game"].ToString();
            
        }
        
        bool isRecentlyOnline()
        {
            if (last_online == new DateTime(0))
                return false;

            TimeSpan ts = DateTime.Now - last_online;
            if (ts.TotalMinutes < 20)
                return true;
            else return false;

        }
         
        void announce()
        {
           // SeijaCommander.skype.get_Chat("#splasher-_-/$omgwtfgglol;7fa80f21182dcf70").SendMessage(String.Format("\tt༼ຈل͜ຈ༽ﾉ·︻̷̿┻̿═━一I'VE GOT THE STREAM IN MY SIGHTS!\n{0}\n{1} playing in {2}\nhttp://www.twitch.tv/{3}",Title,DisplayName,Game,Name));
            SeijaCommander.skype.get_Chat("#splasher-_-/$omgwtfgglol;7fa80f21182dcf70").SendMessage(String.Format("http://www.twitch.tv/{0} is playing {1} | {2}", Name, Game, Title));
        }

        public string Message
        {
            get { return String.Format("http://www.twitch.tv/{0} is playing {1}",Name,Game); }
        }
    }


    class TwitchChecker
    {
        System.Windows.Threading.DispatcherTimer twitchTimer = new System.Windows.Threading.DispatcherTimer();
        //public Dictionary<string, bool> streams =
        //          new Dictionary<string, bool>();
        List<Translation> streams2 = new List<Translation>();

        public string NowOnline()
        {

            if (streams2.Count(x=>x.Is_Online==true)==0)
            {
                List<string> results = new List<string>
                {
                    "Никто сейчас не стримит ;_;",
                    "Не вижу никого онлайн (✿◕⁀◕)",
                    "Стримов не обнаружено, капитан (>‿ <)7",

                };
                return SeijaHelper.GetRandomMessage(results);
            }
            else
            {
                string result = string.Empty;
                foreach (Translation tr in streams2)
                {
                    if (tr.Is_Online == true)
                        result += tr.Message + "\n";
                }
                return result;
            }
        }

        public TwitchChecker()
        {
            UpdateStreamList();
            twitchTimer.Tick += new EventHandler(twitch_Tick);
            twitchTimer.Interval = new TimeSpan(0, 1, 0);
            twitchTimer.Start();
            
        }
        public void UpdateStreamList()
        {
            //streams = new Dictionary<string, bool>();
            //foreach (string str in SeijaCommander.Settings.Values.Streams)
            //{
            //    streams.Add(str, false);
            //}
            streams2 = new List<Translation>();
            foreach (string str in SeijaCommander.Settings.Values.Streams)
            {
                streams2.Add(new Translation(str));
            }
        }

        public void UpdateStreamListSaveValues()
        {
            //Dictionary<string, bool> newStreams = new Dictionary<string, bool>();
            //foreach (string str in SeijaCommander.Settings.Values.Streams)
            //{
            //    newStreams.Add(str, false);
            //}

            //List<string> streamKeys = new List<string>( streams.Keys.ToArray());
            //foreach (string str in streamKeys)
            //{
            //    int t= newStreams.Keys.ToList().FindIndex(x=>x==str);
            //    if (t!=-1)
            //        newStreams[str] = streams[str];
            //}
            //streams = newStreams;
            UpdateStreamList();
  
        }

        private void twitch_Tick(object sender, EventArgs e)
        {
            if (SeijaHelper.isEn == true)
                check_streams();
            //  
        }
        private void check_streams()
        {
            try
            {
                //var keys = new List<string>(streams.Keys);
                using (var w = new System.Net.WebClient())
                {
                    w.Encoding = Encoding.UTF8;
                    foreach (Translation tr in streams2)
                    {
                        string key = tr.Name;
                        String json_data = w.DownloadString("https://api.twitch.tv/kraken/streams/" + key);
                        String json_data2 = w.DownloadString("https://api.twitch.tv/kraken/channels/" + key);
                        JObject stream = JObject.Parse(json_data);
                        JObject channel = JObject.Parse(json_data2);

                        #region old
                        //if (streams[key] == false )
                        //{
                        //    if (stream["stream"].HasValues)
                        //    {
                        //        streams[key] = true;
                        //        SeijaCommander.skype.get_Chat("#splasher-_-/$omgwtfgglol;7fa80f21182dcf70").SendMessage("t༼ຈل͜ຈ༽ﾉ·︻̷̿┻̿═━一I'VE GOT THE STREAM IN MY SIGHTS\nhttp://www.twitch.tv/" + key + " is streaming now | " + channel["status"].ToString());

                        //    }
                        //}
                        //else
                        //{
                        //    if (!stream["stream"].HasValues)
                        //    {
                        //        streams[key] = false;
                        //    }
                        //}
                        #endregion

                        if (stream["stream"].HasValues)
                        {
                            
                            tr.UpdateIndo(channel);
                            tr.Is_Online = true;
                        }
                        else
                            tr.Is_Online = false;
                       
                    }
                }
            }
            catch (Exception ex)
            {
                SeijaHelper.write_log(ex.Message);
            }

        }
    }
}
