using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
using System.Threading;

namespace SkypeBot2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //public class DbInfo
    //{
    //    public DbInfo()
    //    { }
    //    public string id { get; set; }
    //    public string created_at { get; set; }
    //    public string uploader_id { get; set; }
    //    public string score { get; set; }
    //    public string source { get; set; }
    //    public string md5 { get; set; }
    //    public string last_comment_bumped_at { get; set; }
    //    public string rating { get; set; }
    //    public string image_width { get; set; }
    //    public string image_height { get; set; }
    //    public string tag_string { get; set; }
    //    public string is_note_locked { get; set; }
    //    public string fav_count { get; set; }
    //    public string file_ext { get; set; }
    //    public string parent_id { get; set; }
    //    public string has_children { get; set; }
    //    public string approver_id { get; set; }
    //    public string tag_count_general { get; set; }
    //    public string tag_count_artist { get; set; }
    //    public string tag_count_character { get; set; }
    //    public string is_status_locked { get; set; }
    //    public string file_size { get; set; }
    //    public string fav_string { get; set; }
    //    public string pool_string { get; set; }
    //    public string up_score { get; set; }
    //    public string down_score { get; set; }
    //    public string is_pending { get; set; }
    //    public string is_flagged { get; set; }
    //    public string is_deleted { get; set; }
    //    public string tag_count { get; set; }
    //    public string updated_at { get; set; }
    //    public string is_banned { get; set; }
    //    public string pixiv_id { get; set; }
    //    public string last_commented_at { get; set; }
    //    public string has_active_children { get; set; }
    //    public string bit_flags { get; set; }

    //    public string uploader_name { get; set; }
    //    public string has_large { get; set; }
    //    public string tag_string_artist { get; set; }
    //    public string tag_string_character { get; set; }
    //    public string tag_string_copyright { get; set; }
    //    public string tag_string_general { get; set; }
    //    public string has_visible_children { get; set; }
    //    public string file_url { get; set; }
    //    public string large_file_url { get; set; }
    //    public string preview_file_url { get; set; }
      
    //}
    //public class DbTag
    //{
    //    public DbTag()
    //    { }
    //    public string id { get; set; }
    //    public string name { get; set; }
    //    public string post_count { get; set; }
    //    public string related_tags { get; set; }
    //    public string related_tags_updated_at { get; set; }
    //    public string category { get; set; }
    //    public string created_at { get; set; }
    //    public string updated_at { get; set; }
    //    public string is_locked { get{}; set; }
       
    //}
    //public enum statement { will, live, ended, postponed };
   //public struct matchmain
   //     {
   //         public matchmain(string t1, string t2, string t1P, string t2P, statement st, string win, string e, string lk)
   //         {
   //             team1 = t1;
   //             team2 = t2;
   //             team1P = t1P;
   //             team2P = t2P;
   //            // isLive = live;
   //            // ended = endd;
   //             state = st;
   //             winner = win;
   //             events = e;
   //             link = lk;

   //             toRemove = false;
   //         }
   //         public string team1;
   //         public string team2;
   //         public string team1P;
   //         public string team2P;
   //         public statement state;
   //     //    public bool isLive;
   //   //      public bool ended;
   //         public string winner;
   //         public string events;
   //         public string link;
   //         public bool toRemove;
   //     }
    //public static class Seija
    //{
    //    static const string skypeName = "seijabot";
    //    static const string masterName = "fourslash";
    //    static const string dotakonfa = "#splasher-_-/$e36c265204653a65";
    //    static const string homeconfa = "#splasher-_-/$omgwtfgglol;7fa80f21182dcf70";
    //    public static int FixedRandom(int start, int end)
    //    {
    //        Random rnd = new Random(Convert.ToInt32(DateTime.Now));
    //        return rnd.Next(start, end);
    //    }

    //}
   //public class GameChecker
   // {
   //     public static string error;
   //     public static bool isJustOn = true;
   //     private static List<matchmain> matches = new List<matchmain>();
   //     public static List<string> results = new List<string>();
   //     public static void write(List<matchmain> m)
   //     {
   //       //  error = m.GetType().ToString();
   //         var serializer = new XmlSerializer(m.GetType());
   //         var sw= new StreamWriter(@"D:\SHARE\matches.ext");
   //         serializer.Serialize(sw, m);
   //         sw.Close();
   //     }
   //     private static void read()
   //     {
            
   //         try
   //         {
   //             var stream = new StreamReader(@"D:\SHARE\matches.ext");
                
   //             if (stream.BaseStream.Length != 0)
   //             {
                    
   //                 var ser = new XmlSerializer(matches.GetType());
   //                 matches = (List<matchmain>)ser.Deserialize(stream);
                    
   //             }
   //             stream.Close();
   //         }
   //         catch ( Exception e)
   //         {
   //             error = e.Message;
   //         }

   //      }

   //     public static void check()
   //     {
   //         List<matchmain> matches_temp = new List<matchmain>();
   //         string url = "http://dota2lounge.com/";
   //         var Webget = new HtmlWeb();
   //         var doc = Webget.Load(url);
   //         foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//div[@class='matchmain']"))
   //         {
   //             string team1;
   //             string team2;
   //             string team1P;
   //             string team2P;
   //            // bool isLive;
   //             //bool ended;
   //             statement st;
   //             string winner="";
   //             string link;

   //             string events = node.SelectSingleNode(".//div[@class='eventm']").InnerText;
   //             //
   //             string time_string = node.SelectSingleNode(".//div[@class='whenm']").InnerText;
   //             if (time_string.Contains("Postponed"))
   //                 st = statement.postponed;
   //             else if (time_string.Contains("LIVE"))
   //                 st = statement.live;
   //             else if (time_string.Contains("ago"))
   //                 st = statement.ended;
   //             else
   //                 st = statement.will;
   //             //             
   //             string link_string = node.SelectSingleNode(".//a[@href]").Attributes["href"].Value;
   //             link = "http://dota2lounge.com/" + link_string;
   //             //
   //             string team1_string = node.SelectNodes(".//div[@class='teamtext']")[0].InnerHtml;
   //             team1=team1_string.Substring((team1_string.IndexOf("<b>")+3),(team1_string.IndexOf("</b>")-3));
   //             team1P = team1_string.Substring((team1_string.IndexOf("<i>")+3), team1_string.IndexOf("</i>") - (team1_string.IndexOf("<i>")+3));
   //             string team2_string = node.SelectNodes(".//div[@class='teamtext']")[1].InnerHtml;
   //             team2 = team2_string.Substring((team2_string.IndexOf("<b>") + 3), (team2_string.IndexOf("</b>") - 3));
   //             team2P = team2_string.Substring((team2_string.IndexOf("<i>") + 3), team2_string.IndexOf("</i>") - (team2_string.IndexOf("<i>") + 3));
   //             if (st==statement.ended)
   //             {
   //                 if (node.SelectNodes(".//div[@class='team']")[0].InnerHtml.Contains("won.png"))
   //                     winner = team1;
   //                 else if (node.SelectNodes(".//div[@class='team']")[1].InnerHtml.Contains("won.png"))
   //                     winner = team2;
   //             }
   //             matches_temp.Add(new matchmain(team1, team2, team1P, team2P, st, winner, events, link));
                
   //     }
   //         if (isJustOn == true)
   //         {
   //             write(matches_temp);
   //             isJustOn = false;
   //         }
   //         else
   //         {
   //             read();
   //             compare_matches(matches_temp);
   //         }
   // }

   //     public static void compare_matches(List<matchmain> match_t)
   //     {
   //        foreach (matchmain mT in match_t)
   //        {
   //            int num=matches.FindIndex(x=>x.link==mT.link);
   //            if (num==-1)
   //            {
   //                 matches.Add(mT);
   //                 announce(mT);
   //            }
   //            else
   //            {
   //                matchmain temp = matches[num];
   //                temp.team1P = mT.team1P;
   //                temp.team2P = mT.team2P;
   //                if (temp.state!=mT.state)
   //                {
   //                    if(mT.state!=statement.will)
   //                    {
   //                        temp = mT;
   //                        announce(temp);
   //                    }
   //                }
   //                matches[num] = temp; 
   //            }
   //        }
   //         for (int i=0;i<matches.Count;i++)
   //         {
   //             if(match_t.FindIndex(x=>x.link==matches[i].link)==-1)
   //             {
   //                 matchmain temp = matches[i];
   //                 temp.toRemove = true;
   //                 matches[i] = temp;
   //             }
   //             matches.RemoveAll(x => x.toRemove == true);
   //         }
   //         write(matches);
   //     }
   //     private static void announce(matchmain mt)
   //      {
   //          if (mt.state==statement.live)
   //          {
   //              results.Add(mt.team1 + "(" + mt.team1P + ") VS " + mt.team2 + "(" + mt.team2P + ") went live on "+"[" + mt.events + "] "+mt.link);
   //          }
   //          else if (mt.state == statement.postponed)
   //          {
   //              results.Add(mt.team1 + "(" + mt.team1P + ") VS " + mt.team2 + "(" + mt.team2P + ") on " + "[" + mt.events + "] was postponed " + mt.link);
   //          }
   //          else if (mt.state == statement.ended)
   //          {
   //              string res;
   //              if (mt.winner == "")
   //                  res = "DRAW";
   //              else
   //                  res = "Winner is " + mt.winner; 
   //              results.Add(mt.team1 + "(" + mt.team1P + ") VS " + mt.team2 + "(" + mt.team2P + ") on " + "[" + mt.events + "] is over. "+res+" " + mt.link);
   //          }
   //      }
   //     }


   

    //public partial class Seija
    //{
    //    //const string botName = "seijabot";
    //    const string dotakonfa = "#splasher-_-/$e36c265204653a65";
    //    const string homeconfa = "#splasher-_-/$omgwtfgglol;7fa80f21182dcf70";
    //    const string kantaekonfa = "#nekonyak/$88001bf7f531a99e";
    //    //const string masterName = "fourslash";
    //    //const string commandSymvol = "!";
        
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    //DongerBattler dong;
    //    Skype skype;
    //    ChatMessage currentMessage;
    //    string askedName;
    //    /// <summary>
    //    /// 
    //    /// </summary> 
    //    System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
    //   // System.Windows.Threading.DispatcherTimer twitchTimer = new System.Windows.Threading.DispatcherTimer();
    //   // System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    //Dictionary<string, bool> streams =
    //              //new Dictionary<string, bool>();
    //   // Dictionary<string, string> simpleCommands =
    //     //         new Dictionary<string, string>();
    //    //Dictionary<string, string> chars =
    //    //  new Dictionary<string, string>();
    //    //public static List<string> dongers = new List<string>();
    //    //List<string> pasta = new List<string>();
    //    //List<string> kicklist = new List<string>();
    //    //List<string> nextdoor = new List<string>();
    //    List<BotCommand> commands;
    //    //List<string> botNames;
    //    public Seija(Skype sk)
    //    {
    //        skype = sk;
    //        //dong=new DongerBattler();
    //        try
    //        {
    //            Initial();
    
    //        }
    //        catch (Exception ex)
    //        {
    //            skype.SendMessage(SeijaCommander.Settings.Values.masterName, ex.Message);
    //        }
    //    }
    //    private void Initial()
    //    {

            
    //        //streams.Add("forsenlol", false);
    //        //streams.Add("dreadztv", false);
    //        //streams.Add("amazhs", false);
    //        //streams.Add("demolition_d", false);
    //        //streams.Add("sing_sing", false);
    //        //streams.Add("helixsnake", false);
    //        //streams.Add("kolento", false);
    //        //
    //        //SeijaHelper.loadTXT(kicklist, @"Settings\kicklist.txt");
    //       // SeijaHelper.loadTXT(dongers, @"Settings\dongerlist.txt");
    //       // SeijaHelper.loadTXT(pasta, @"Settings\pastalist.txt");
    //        //SeijaHelper.loadTXT(GameChecker.teams, @"D:\SHARE\dota_teams.txt");
    //        //if( GameChecker.ReReadTeams()!=true)
    //        //    skype.SendMessage(SeijaCommander.Settings.Values.masterName, "Внимание! Список команд не был загружен!");
    //        //SeijaHelper.loadSimpleCommands(simpleCommands);
    //        //SeijaHelper.loadChars(chars);
    //        //
    //        commands = new List<BotCommand>
    //        {
    //            new BotCommand("donger_add",cmd_Add_donger),
    //            new BotCommand("pasta_add",cmd_Add_pasta),
    //            new BotCommand("kick",cmd_Kick),
    //            new MoraleCommand("seija",cmd_Seija),
    //            new BotCommand("command_add",cmd_Add),
    //            new MoraleCommand("goku",cmd_Goku),
    //            new BotCommand("uptime",cmd_Uptime),
    //            new MoraleCommand("jaraxxus",cmd_Jaraxxus),
    //            new BotCommand("rng",cmd_Rng),
    //            new BotCommand("update_kicklist",cmd_UpdateKicklist),
    //            new BotCommand("streams",cmd_Streams),
    //            new MoraleCommand("кораблей",cmd_SHIP),
    //            new BotCommand("hug",cmd_Hug),
    //            new MoraleCommand("bnd",cmd_BND),
    //            new MoraleCommand("donger",cmd_Donger),
    //            new MoraleCommand("pasta",cmd_Pasta),
    //            new BotCommand("help_please",cmd_Help),
    //            new MoraleCommand("db",cmd_db),
    //            new MoraleCommand("db_find",cmd_db_find),
    //            new BotCommand("on",cmd_On),
    //            new BotCommand("off",cmd_Off),
    //            new BotCommand("stop",cmd_Stop),
    //            new BotCommand("help_add",cmd_Help_add),
    //            new BotCommand("set_enabled",cmd_set_enabled),
    //            new BotCommand("set_master_only",cmd_set_master_only),
    //            new BotCommand ("update_all",cmd_update_all),
    //            new BotCommand ("battle",cmd_battle),
    //            new BotCommand ("get_chatname",cmd_GetChatname),
    //            new BotCommand ("leave",cmd_leave),
    //            new BotCommand ("dota_team_add",cmd_Add_team),
    //            new BotCommand ("dota_team_delete",cmd_Delete_team),
    //            new BotCommand ("dota_show_teams",cmd_Show_teams),

    //        };
    //        BotCommand.DeSerialazeSC();
    //        foreach (BotCommand cmd in commands)
    //        {
    //            BotCommand.DeSerialaze(cmd);
    //        }
    //        //botNames = new List<string>
    //        //{
    //        //    "Сейджа",
    //        //    "Сейдзя",
    //        //    "сейджа",
    //        //    "сейдзя"
    //        //};

    //        dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
    //        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
    //        dispatcherTimer.Start();
    //        //twitchTimer.Tick += new EventHandler(twitch_Tick);
    //        //twitchTimer.Interval = new TimeSpan(0, 1, 0);
    //        //twitchTimer.Start();
    //        //gameTimer.Tick += new EventHandler(game_Tick);
    //        //gameTimer.Interval = new TimeSpan(0, 3, 0);
    //        //gameTimer.Start();
    //        //DongerBattler.Init();
    //        //
    //    }
    //    public void ProcessMSG(ChatMessage msg)
    //    {
    //        currentMessage = msg;
    //        if (currentMessage.Sender.Handle!=SeijaCommander.Settings.Values.botName)
    //        {
    //            try
    //            {
    //                //Thread myThread = new Thread(checkForTenshi);
    //                //myThread.Start(msg.Body);
    //                if (ProcessCommand() == true)
    //                    return;
    //                else if (no_copypasterino() == true)
    //                    return;
    //                else if (isAsked() == true)
    //                {
    //                    SeijaAsk();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                //msg.Chat.SendMessage("Error: "+ex.Message);
    //                skype.SendMessage(SeijaCommander.Settings.Values.masterName, msg.ChatName + "\n" + ex.Message);
    //                SeijaHelper.write_log( ex.Message);
    //            }
    //        }
    //    }


    //    private void checkForTenshi(object str_obj)
    //    {
    //        try
    //        {
    //            string str = (string)str_obj;
    //            List<string> words = new List<string>(str.Split(' ').ToArray());
    //            foreach (string word in words)
    //            {
    //                if (word.Contains("http"))
    //                {
    //                    TenshiChecker check = new TenshiChecker(word, skype);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //msg.Chat.SendMessage("Error: "+ex.Message);
    //            //skype.SendMessage(masterName, msg.ChatName + "\n" + ex.Message);
    //            SeijaHelper.write_log(ex.Message);
    //        }

    //    }

    //    private bool no_copypasterino() //мне очень стыдно за это функцию но мне слишком лень писать нормальный код
    //    {
    //        if (currentMessage.Body.Contains("no copy"))
    //        {
    //            string[] cancer = { "(no space)", "(no spaceerino)", "Kappa", "4Head" };
    //            string temp = currentMessage.Body;
    //            int t = SeijaHelper.FixedRandom(1, 3);
    //            if (t == 1)
    //                temp += " " + cancer[SeijaHelper.FixedRandom(0, cancer.Length)];
    //            System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(1500, 3500));
    //            currentMessage.Chat.SendMessage(temp);
    //            return true;
    //        }
    //        else if (currentMessage.Body.Contains("но копи"))
    //        {
    //            string[] cancer = { "(но спейс)", "(но спейсерино)", "Kappa", "4Head" };
    //            string temp = currentMessage.Body;
    //            int t = SeijaHelper.FixedRandom(1, 3);
    //            if (t == 1)
    //                temp += " " + cancer[SeijaHelper.FixedRandom(0, cancer.Length)];
    //            System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(1500, 3500));
    //            currentMessage.Chat.SendMessage(temp);
    //            return true;
    //        }
    //        return false;
    //    }

    //    private bool isAsked()
    //    {
    //        askedName = string.Empty;
    //        bool isRight = false;
    //        foreach (string name in SeijaCommander.Settings.Values.botNames)
    //            if (currentMessage.Body.Contains(name))
    //            {
    //                askedName = name;
    //                isRight = true;
    //            }
    //        return isRight;
    //    }

    //    private bool SeijaAsk()
    //    {
    //        if (currentMessage.Body.IndexOf(",")==(currentMessage.Body.IndexOf(askedName)+askedName.Length) && currentMessage.Body.Contains("?"))
    //        {
    //            if (currentMessage.Sender.Handle == "zoljiin")
    //            {
    //                currentMessage.Chat.SendMessage(@"http://imgur.com/ygVipUl.jpg");
    //            }
    //            else
    //            {
    //                int num = SeijaHelper.FixedRandom(0, 10000);
    //                if (num % 2 == 0)
    //                    currentMessage.Chat.SendMessage(@"http://imgur.com/F6hu49A.jpg");
    //                else
    //                    currentMessage.Chat.SendMessage(@"http://imgur.com/Ct47ALh.jpg");
    //            }
    //            return true;
    //        }
    //        return false;
    //    }
    //    //private void check_streams()
    //    //{
    //    //    try
    //    //    {
    //    //        var keys = new List<string>(streams.Keys);
    //    //        using (var w = new System.Net.WebClient())
    //    //        {
    //    //            foreach (string key in keys)
    //    //            {
    //    //                String json_data = w.DownloadString("https://api.twitch.tv/kraken/streams/" + key);
    //    //                String json_data2 = w.DownloadString("https://api.twitch.tv/kraken/channels/" + key);
    //    //                JObject stream = JObject.Parse(json_data);
    //    //                JObject channel = JObject.Parse(json_data2);
    //    //                if (streams[key] == false)
    //    //                {
    //    //                    if (stream["stream"].HasValues)
    //    //                    {
    //    //                        streams[key] = true;
    //    //                        skype.get_Chat(homeconfa).SendMessage("t༼ຈل͜ຈ༽ﾉ·︻̷̿┻̿═━一I'VE GOT THE STREAM IN MY SIGHTS\nhttp://www.twitch.tv/" + key + " is streaming now | " + channel["status"].ToString());
    //    //                    }
    //    //                }
    //    //                else
    //    //                {
    //    //                    if (!stream["stream"].HasValues)
    //    //                    {
    //    //                        streams[key] = false;
    //    //                    }
    //    //                }
    //    //            }
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        SeijaHelper.write_log(ex.Message);
    //    //    }

    //    //}
    //    private void dispatcherTimer_Tick(object sender, EventArgs e)
    //    {
    //        SeijaHelper.ticks++;

    //    }

    //    //private void game_Tick(object sender, EventArgs e)
    //    //{
    //    //    string chatname = dotakonfa;
    //    //    if (SeijaHelper.isEn == true)
    //    //    {
    //    //        try
    //    //        {
    //    //            GameChecker.check();
    //    //        }
    //    //        catch (Exception ex)
    //    //        {
    //    //            skype.SendMessage(SeijaCommander.Settings.Values.masterName, chatname + "\n" + ex.Message);
    //    //            SeijaHelper.write_log("\r\n" + DateTime.Now.ToString("u") + "\r\n" + chatname + "\r\n" + ex.Message + "\r\n");
    //    //        }
    //    //        finally
    //    //        {
    //    //            foreach (string ms in GameChecker.results)
    //    //            {
    //    //                skype.get_Chat(chatname).SendMessage(ms);
    //    //            }
    //    //            //
    //    //            GameChecker.results.Clear();
    //    //        }
    //    //    }
    //    //}
    //    private bool ProcessCommand()
    //    {
    //        string message = currentMessage.Body;
    //        if (message.IndexOf(SeijaCommander.Settings.Values.commandSymvol) == 0)
    //        {
    //            message = message.Remove(0, SeijaCommander.Settings.Values.commandSymvol.Length);
    //            string command, arg;
    //            if (message.IndexOf(" ") != -1)
    //            {
    //                command = message.Remove(message.IndexOf(" "));
    //                arg = message.Remove(0, message.IndexOf(" ") + 1);
    //            }
    //            else
    //            {
    //                command = message;
    //                arg = "";
    //            }
    //            BotCommand cmd = commands.Find(x => x.CommandName == command);
    //            if (cmd == null)
    //                return ProcessSimpleCommand(message, currentMessage);
    //            else
    //            {
    //                if (cmd.isMoraleCommand==true)
    //                    return ((MoraleCommand)cmd).TryExecute(arg, currentMessage);
    //                else
    //                    return cmd.TryExecute(arg, currentMessage);
    //            }
                    
    //            //if (!_commands.ContainsKey(command))
    //            //    return ProcessSimpleCommand(str, currentMessage);
    //            //else
    //            //    return _commands[command](arg, currentMessage);
    //        }
    //        return false;
    //    }
    //    private bool ProcessSimpleCommand(string command, ChatMessage ms)
    //    {
    //        string res = "";
    //        var keys = new List<string>(SeijaCommander.SimpleCommands.Pairs.Keys);
    //        foreach (string k in keys)
    //        {
    //            if (command == k)
    //                res = SeijaCommander.SimpleCommands.Pairs[k].Clone().ToString();
    //        }
    //        //res = res.Replace(@"\\", @"\");
    //        //res = res.Replace("\\n", "\n");
    //        if (res == "")
    //            return false;
    //        ms.Chat.SendMessage(res);
    //        return true;
    //    }

      
    //}

    //public class SeijaHelper
    //{
    //    public static class RandomProvider
    //    {
    //        private static int seed = Environment.TickCount;

    //        private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(() =>
    //            new Random(Interlocked.Increment(ref seed))
    //        );

    //        public static Random GetThreadRandom()
    //        {
    //            return randomWrapper.Value;
    //        }
    //    } 

    //    //public static Random rnd = new Random();
    //    public static int ticks=0;
    //    public static bool isEn = false;
    //    public static bool isStop = false;
    //    //public const string masterName = "fourslash";
    //    //public const string homeconfa = "#splasher-_-/$omgwtfgglol;7fa80f21182dcf70";
    //    public static string GetRandomMessage (List<string> messages)
    //    {
    //        Random r = new Random(Convert.ToInt32(DateTime.Now.Millisecond * DateTime.Now.Second));
    //        return messages[r.Next(0,messages.Count())]; 
    //    }
    //    //public static void Serialaze(BotCommand ccc)
    //    //{
    //    //    string path=@"D:\SHARE\Seija_commands\c_"+ccc.CommandName+@".ext";
    //    //    if (!File.Exists(path))
    //    //        using (File.Create(path));
    //    //    var serializer = new XmlSerializer(ccc.GetType());
    //    //    var sw = new StreamWriter(path);
    //    //    serializer.Serialize(sw, ccc);
    //    //    sw.Close();
    //    //}
    //    public static int db_tag_pages(string tag)
    //    {
    //        try
    //        {
    //            using (var w = new System.Net.WebClient())
    //            {
    //                String json_data = w.DownloadString(@"https://danbooru.donmai.us/tags.json?search[name_matches]=" + tag);
    //                json_data = json_data.Remove(0, 1);
    //                json_data = json_data.Remove(json_data.Length-1, 1);
    //                DbTag jTag = JsonConvert.DeserializeObject<DbTag>(json_data);
    //                return (Convert.ToInt32(jTag.post_count) / 20)+1;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return 1;
    //        }

    //    }
    //    public static int FixedRandom(int start, int end)
    //    {
    //        //Random rnd = new Random(Convert.ToInt32(DateTime.Now.Millisecond*DateTime.Now.Second));
    //        //return rnd.Next(start, end);
    //        Random rnd = new Random();
    //        List<int> lst = new List<int>();
    //        for (int i = 0; i < 5; i++)
    //            lst.Add(rnd.Next(start, end));
    //        return lst[rnd.Next(0, 5)];
    //    }
    //    public static void loadTXT(List<String> lst, string path)
    //    {
    //        lst.Clear();
    //        using (System.IO.StreamReader file = File.OpenText(path))
    //        {
    //            string line = "";
    //            while ((line = file.ReadLine()) != null)
    //                lst.Add(line);
    //        }
    //    }


    //    //public static void loadSimpleCommands(Dictionary<string, string> simpleCommands)
    //    //{
    //    //    simpleCommands.Clear();
    //    //    using (System.IO.StreamReader file = File.OpenText(@"Settings\simpleComands.txt"))
    //    //    {
    //    //        string line = "";
    //    //        while ((line = file.ReadLine()) != null)
    //    //        {
    //    //            int point = line.IndexOf(":");
    //    //            string key, value;
    //    //            key = line.Substring(0, point);
    //    //            value = line.Substring(point + 1);
    //    //            simpleCommands.Add(key, value);
    //    //        }
    //    //    }
    //    //}
    //    public static void write_log(string msg)
    //    {
    //        using (System.IO.StreamWriter file = File.AppendText(@"Settings\log.txt"))
    //        {
    //            file.WriteLine(DateTime.Now.ToString("u") + " " + msg);
    //        }
    //    }
    //    //public static void loadChars(Dictionary<string, string> chars)
    //    //{
    //    //    chars.Clear();
    //    //    using (System.IO.StreamReader file = File.OpenText(@"Settings\seija.txt"))
    //    //    {
    //    //        string temp1, temp2;
    //    //        int point1, point2;
    //    //        string line1 = file.ReadLine();
    //    //        string line2 = file.ReadLine();
    //    //        while (!(line1 == "" || line2 == ""))
    //    //        {
    //    //            point1 = line1.IndexOf(" ");
    //    //            point2 = line2.IndexOf(" ");
    //    //            temp1 = line1.Substring(0, point1);
    //    //            temp2 = line2.Substring(0, point2);
    //    //            line1 = line1.Remove(0, point1 + 1);
    //    //            line2 = line2.Remove(0, point2 + 1);
    //    //            chars.Add(temp1, temp2);
    //    //        }
    //    //    }
    //    //}
    //}

    public partial class MainWindow : Window
    {
        //static const string skypeName = "seijabot";
        //static const string masterName = "fourslash";
        //static const string dotakonfa = "#splasher-_-/$e36c265204653a65";
        //static const string homeconfa = "#splasher-_-/$omgwtfgglol;7fa80f21182dcf70";
        //const string dotakonfa = "#splasher-_-/$e36c265204653a65";
        //const string homeconfa = "#splasher-_-/$omgwtfgglol;7fa80f21182dcf70";
        
       // Random rnd = new Random();
        

        //Skype skype;
        //Seija SeijaBot;
        /// <summary>
        /// Считывание из текстовиков
        /// </summary>
        //Dictionary<string, bool> streams =
        //          new Dictionary<string, bool>();
        //Dictionary<string, string> simpleCommands =
        //          new Dictionary<string, string>();
        //Dictionary<string, string> chars =
        //          new Dictionary<string, string>();
        //List<string> dongers = new List<string>();
        //List<string> pasta = new List<string>();
        //List<string> kicklist = new List<string>();
        //List<string> nextdoor = new List<string>();
        
        //private Dictionary<string, Func<string, ChatMessage, bool>> _commands;
        //private Dictionary<Func<string, ChatMessage, bool>, string> _help;
        //List<BotCommand> cmd;
        /// <summary>
        /// 
        /// </summary>
        //int ticks=0;
        //string sender;
        
        
        /// <summary>
        /// таймеры
        /// </summary>
        //System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        //System.Windows.Threading.DispatcherTimer twitchTimer = new System.Windows.Threading.DispatcherTimer();
        //System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            try
            {

                ///
                InitializeComponent();
                //SkypeConnector cnn = new SkypeConnector();
                //cnn.Connect();
                SeijaCommander.Init();
                //setSkype();
                //setTimers();
                //loadSimpleCommands();
                //
                //loadTXT(kicklist, @"D:\SHARE\kicklist.txt");
                //loadTXT(dongers, @"D:\SHARE\dongerlist.txt");
                //loadTXT(pasta, @"D:\SHARE\pastalist.txt");
                //loadChars();
            }
            catch (Exception ex)
            {
                SeijaHelper.write_log(ex.Message);
                this.Close();
            }

           
        }

        
        //public void setSkype()
        //{
        //    try
        //    {
        //       Skype skype = new Skype();
        //       if (!skype.Client.IsRunning)
        //       {
        //           skype.Client.Start(true, true);
        //       }
        //        skype.Attach(7, false);
        //        skype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);
        //        //SeijaBot = new Seija(skype);

        //        SeijaCommander.Init(skype);
        //    }
        //    catch (Exception ex)
        //    {
        //        SeijaHelper.write_log(ex.Message);
        //    }
        //}
        ////public void setTimers()
        ////{
            
        ////    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        ////    dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        ////    dispatcherTimer.Start();
        ////    twitchTimer.Tick += new EventHandler(twitch_Tick);
        ////    twitchTimer.Interval = new TimeSpan(0, 1, 0);
        ////    twitchTimer.Start();
        ////    gameTimer.Tick += new EventHandler(game_Tick);
        ////    gameTimer.Interval = new TimeSpan(0, 3 , 0);
        ////    gameTimer.Start();
        ////}
        ////private void dispatcherTimer_Tick(object sender, EventArgs e)
        ////{
        ////    SeijaHelper.ticks++;

        ////}
        ////private void twitch_Tick(object sender, EventArgs e)
        ////{
        ////    if (isEn == true)
        ////        check_streams();
        ////    //  
        ////}
        ////private void game_Tick(object sender, EventArgs e)
        ////{
        ////    string chatname = dotakonfa;
        ////    if (isEn == true)
        ////    {
        ////        try
        ////        {

        ////            GameChecker.check();
        ////            foreach (string ms in GameChecker.results)
        ////            {
        ////                skype.get_Chat(chatname).SendMessage(ms);
        ////            }
        ////            //
        ////            GameChecker.results.Clear();
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            skype.SendMessage("fourslash", chatname + "\n" + ex.Message);
        ////            write_log("\r\n" + DateTime.Now.ToString("u") + "\r\n" + chatname + "\r\n" + ex.Message + "\r\n");
        ////        }
        ////    }
        ////}
        ////private void write_log(string currentMessage)
        ////{
        ////    using (System.IO.StreamWriter file = File.AppendText(@"D:\SHARE\log.txt"))
        ////    {
        ////        file.WriteLine(DateTime.Now.ToString("u") + " " + currentMessage);
        ////    }
        ////}
        ////private void check_streams()
        ////{
        ////    try
        ////    {
        ////        var keys = new List<string>(streams.Keys);
        ////        using (var w = new System.Net.WebClient())
        ////        {
        ////            foreach (string key in keys)
        ////            {
        ////                String json_data = w.DownloadString("https://api.twitch.tv/kraken/streams/" + key);
        ////                String json_data2 = w.DownloadString("https://api.twitch.tv/kraken/channels/" + key);
        ////                JObject stream = JObject.Parse(json_data);
        ////                JObject channel = JObject.Parse(json_data2);
        ////             if (streams[key] == false)
        ////                {
        ////                    if (stream["stream"].HasValues)
        ////                    {
        ////                        streams[key] = true;
        ////                        skype.get_Chat("#splasher-_-/$omgwtfgglol;7fa80f21182dcf70").SendMessage("t༼ຈل͜ຈ༽ﾉ·︻̷̿┻̿═━一I'VE GOT THE STREAM IN MY SIGHTS\nhttp://www.twitch.tv/" + key + " is streaming now | " + channel["status"].ToString());
        ////                    }
        ////                }
        ////                else
        ////                {
        ////                    if (!stream["stream"].HasValues)
        ////                    {
        ////                        streams[key] = false;
        ////                    }
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        write_log(ex.Message);
        ////    }

        ////}


        ////private void loadSimpleCommands(Dictionary<string, string> simpleCommands)
        ////{
        ////    simpleCommands.Clear();
        ////    using (System.IO.StreamReader file = File.OpenText(@"D:\GoogleDrive\simpleComands.txt"))
        ////    {
        ////        string line = "";
        ////        while ((line = file.ReadLine()) != null)
        ////        {
        ////            int point=line.IndexOf(":");
        ////            string key, value;
        ////            key = line.Substring(0, point);
        ////            value = line.Substring(point + 1);
        ////            simpleCommands.Add(key, value);
        ////        }
        ////    }
        ////}
        ////private void loadChars(Dictionary<string, string> chars)
        ////{
        ////    chars.Clear();
        ////    using (System.IO.StreamReader file = File.OpenText(@"D:\SHARE\seija.txt"))
        ////    {
        ////       string temp1,temp2;
        ////       int point1, point2;
        ////       string line1=file.ReadLine();
        ////       string line2 = file.ReadLine(); 
        ////       while (!(line1=="" || line2==""))
        ////       {
        ////           point1 = line1.IndexOf(" ");
        ////           point2 = line2.IndexOf(" ");
        ////           temp1 = line1.Substring(0, point1);
        ////           temp2 = line2.Substring(0, point2);
        ////           line1=line1.Remove(0, point1 + 1);
        ////           line2=line2.Remove(0, point2 + 1);
        ////           chars.Add(temp1, temp2);
        ////       }
        ////    }
        ////}
        ////private void no_copypasterino(ChatMessage currentMessage) //мне очень стыдно за это функцию но мне слишком лень писать нормальный код
        ////{
        ////    if (currentMessage.Body.Contains("no copy"))
        ////    {
        ////        string[] cancer = { "(no space)", "(no spaceerino)", "Kappa", "4Head"};
        ////        string temp = currentMessage.Body;
        ////        int t = SeijaHelper.FixedRandom(1, 3);
        ////        if (t==1)
        ////            temp += " " + cancer[SeijaHelper.FixedRandom(0, cancer.Length)];
        ////        System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(1500, 3500));
        ////        currentMessage.Chat.SendMessage(temp);
        ////    }
        ////    else if(currentMessage.Body.Contains("но копи"))
        ////    {
        ////        string[] cancer = { "(но спейс)", "(но спейсерино)", "Kappa", "4Head" };
        ////        string temp = currentMessage.Body;
        ////        int t = SeijaHelper.FixedRandom(1, 3);
        ////        if (t == 1)
        ////            temp +=" "+cancer[SeijaHelper.FixedRandom(0, cancer.Length)];
        ////        System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(1500, 3500));
        ////        currentMessage.Chat.SendMessage(temp);
        ////    }
        ////}
        ////private bool SeijaAsk(ChatMessage currentMessage)
        ////{
        ////    if ((currentMessage.Body.IndexOf("Сейджа,")==0 || currentMessage.Body.IndexOf("Сейдзя,")==0)&&currentMessage.Body.Contains("?"))
        ////    {
        ////        int num = SeijaHelper.FixedRandom(0,10000);
        ////        if (num%2==0)
        ////            currentMessage.Chat.SendMessage(@"http://imgur.com/F6hu49A");
        ////        else
        ////            currentMessage.Chat.SendMessage(@"http://imgur.com/Ct47ALh");
        ////        return true;
        ////    }
        ////    return false;
        ////}
        //private void skype_MessageStatus(ChatMessage msg, TChatMessageStatus status)
        //{
        //    try
        //    {
                

        //        //string ms = msg.Body.ToLower();
        //        //sender = msg.Sender.Handle;
        //        //if (sender != "seijabot")
        //        //{
        //        //    //foreach (string s in kicklist)
        //        //    //{
        //        //    //    if (ms.Contains(s))
        //        //    //    {
        //        //    //        msg.Chat.SendMessage("/kick " + msg.Sender.Handle);
        //        //    //    }
        //        //    //}
        //        //    ProcessImportantCommand(msg);
        //        //    if (isEn == true)
        //        //    {
        //        //        if (SeijaAsk(msg) == false)
        //        //        {
        //        //            if (ProcessCommand(msg) == false)
        //        //            {
        //        //                no_copypasterino(msg);
        //        //            }
        //        //        }
        //        //    }
        //        //}

        //        if ((SeijaHelper.isEn == true || msg.Body.Contains("on"))/*&& msg.Sender.Handle=="fourslash"*/)
        //        {
        //            SeijaCommander.Seija.ProcessMSG(msg);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ///msg.Chat.SendMessage("Error:" +e.Message);
        //       // skype.SendMessage("fourslash", msg.ChatName + "\n" + e.Message);
        //        SeijaHelper.write_log("\r\n"+DateTime.Now.ToString("u")+"\r\n"+msg.ChatName + "\r\n" + e.Message+"\r\n");
        //    }
        
       /*private string SimpleCommand(string str)
        {
            string res="";
            switch (str)
            {
                case "ameno":
                    res = "༼ つ ◕_◕ ༽つ AMENO ༼ つ ◕_◕ ༽つ";
                    break;
                case "protect":
                    res = "123";
                    break;
                case "rekt":
                    res = "☑ rekt ☐ not rekt";
                    break;
                case "help":
                    res = "( ° ͜ʖ͡°)╭∩╮Here's a \"help\" for you. ( ° ͜ʖ͡°)╭∩╮";
                    break;
                case "9/11":
                    res = "( ͡° ͜ʖ ͡°)╯╲____✈ ▌▌ Oh, don't mind me just taking my 9/11 for a walk";
                    break;
                case "copy":
                    res = " ╰( ͡° ͜ʖ ͡° )つ──☆*:・ﾟCopius Pasterinus!";
                    break;
                case "donger":
                    res = generateDonger();
                    break;
                case "forsen":
                    res = "http://www.twitch.tv/forsenlol";
                    break;
                case "eject":
                    res = "http://hydra-media.cursecdn.com/dota2.gamepedia.com/c/c2/Vipe_death_16.mp3";
                    break;
                case "-25":
                    res = "(▀Ĺ̯▀ ̿) This is the -25 Police, You're coming with us (▀Ĺ̯▀ ̿)";
                    break;
                case "rip":
                    res = "BibleThump REMI IS DEAD BibleThump";
                    break;
                case "arena":
                    res = "ez 1-3";
                    break;
                case "bnd":
                    res = generateBND();
                    break;
                case "мамин член":
                    res = "http://rghost.ru/8nbvgWqFj";
                    break;
                case "udonger":
                    res = "Reisen Udonger Inaba\n   (\\    /)\nヽ༼, ͡ຈ ͜ʖ ͡ຈ,༽ﾉ";
                    break;
                case "5к":
                    break;
                case "uptime":
                    res = "Я работаю уже " + ticks.ToString() + " секунд (✿◕⁀◕)";
                    break;
                case "rmm":
                    res = "Meet John, an above average player who wins most of his games and often contributes the most to the team’s victory. Now, meet Ricardo, who only picks hard carries, feeds and communicates with his team through sentences containing “fuck” and “your mom”; and Shlomo, who has zero skill but likes buying and trading shiny hats. Ricardo and Shlomo are tired of losing all their games and might quit. In order to prevent Ricardo and Shlomo from quitting, John is forced to try and carry them. John, with great distress and disgust, manages to carry Ricardo and Shlomo so that they would stick around and give Valve their shekels. That’s the secret algorithm, folks.";
                    break;
                case "ag":
                    res = "http://i.gyazo.com/7adfa70d84e35482ca7279c2e7eddb37.png";
                    break;
                case "honk":
                    res = "http://www.youtube.com/watch?v=7jbZ8RdBzwY";
                    break;
                case "roadto7k":
                    res = "http://rghost.ru/8LgXCvlNH.view";
                    break;
                case "notrekt":
                    res = "☐ rekt ☑ not rekt";
                    break;
                case "dread":
                    res = "GO STREAM ZAEBAL";
                    break;
                case "haha":
                    res = "4Head";
                    break;
                case "вис":
                    res = "хуис";
                    break;
                case "нож":
                    res = "http://www.youtube.com/watch?v=DiYwPkatK54";
                    break;
                case "home":
                    res = "skype:?chat&blob=6RlmCrC02yscpS8MO95iyuq0fb900lwDZ15Oi1hAanwfNatsagoSYUgbyT9b0_k5yVXwrfHpO96EY3pr-VA3avznaw1an7Ma1-tdNCetUyW7KkIkUjuu9VJx8CedS_Ny2Q";
                    break;
                case "high":
                    res = "AHAHAHAHAHA JUST *HOW* *_HIGH_* DO YOU EVEN/nHAVE TO *BE* JUST TO *DO* SOMETHING LIKE/nTHAT.......";
                    break;
                case "taxi":
                    res = "http://higgs.rghost.ru/77xTlcWQC/image.png";
                    break;
                case "сосок":
                    res = "http://higgs.rghost.ru/private/6MF5CpWWq/f84e20dad7f34ba0d4514b28d7da8953/image.png";
                    break;
                case "ufoporno":
                    res = "http://www.youtube.com/watch?v=MHYEWa8JjSg";
                    break;
                case "мед":
                    res = "http://tau.rghost.ru/6H6F4vYj7/image.png";
                    break;
                case "kk_tea":
                    res = "http://www51.atpages.jp/kancollev/kcplayer.php?c=78v11&vol=1";
                    break;
                case "kk_cuppa":
                    res = "http://www51.atpages.jp/kancollev/kcplayer.php?c=78v3&vol=1";
                    break;
                case "kk_idle":
                    res = "http://www51.atpages.jp/kancollev/kcplayer.php?c=186v29&vol=1";
                    break;
                case "greetings":
                    res = "WELL MET";
                    break;
                case "faith":
                    res = "PUT YOUR FAITH IN THE LIGHT";
                    break;
                case "banana":
                    res = "http://www.youtube.com/watch?v=vT3uuZZnaxw";
                    break;
                case "нет":
                    res = "пидора ответ (✿◕⁀◕)";
                    break;
                case "дцп":
                    res = "ヽ(◉◡◔)ﾉ";
                    break;
                case "":
                    res = "";
                    break;
                case "":
                    res = "";
                    break;
                case "":
                    res = "";
                    break;
                case "":
                    res = "";
                    break;


            }
            return res;

        }*/
        //private string commandOff()
        //{
        //    if (sender == "fourslash")
        //    {
        //        isEn = false;
        //        skype.ChangeUserStatus(TUserStatus.cusAway);
        //        return "Бот будет остановлен (✿◕⁀◕)";
        //    }
        //    else
        //        return "Ты не можешь этого сделать, раб (✿-̶●̃益●̶̃)";
        //}
        //private string commandOn()
        //{
        //    if (sender == "fourslash")
        //    {
        //        isEn = true;
        //        GameChecker.isJustOn = true;
        //        skype.ChangeUserStatus(TUserStatus.cusOnline);
        //        return "Бот будет включен (✿◕⁀◕)";
        //    }
        //    else
        //        return "Ты не можешь этого сделать, раб (✿-̶●̃益●̶̃)";
        //}
        //private string commandStop()
        //{
        //    if (sender == "fourslash")
        //    {
        //        isStop = true;
        //        skype.ChangeUserStatus(TUserStatus.cusInvisible);
        //        return "Бот будет выключен (✿◕⁀◕)";
        //    }
        //    else
        //        return "Ты не можешь этого сделать, раб (✿-̶●̃益●̶̃)";
        //}
        
       

        //private void ProcessImportantCommand(ChatMessage currentMessage)
        //{
        //    string str = currentMessage.Body;
        //    string res = "";
        //    switch (str)
        //    {
        //        case "!off":
        //            res=commandOff();
        //            break;
        //        case "!on":
        //            res=commandOn();
        //            break;
        //        case "!stop":
        //            res=commandStop();
        //            break;
        //    }
        //    if (res != "")
        //        currentMessage.Chat.SendMessage(res);
        //    if (isStop == true)
        //        this.Close();
        //}      
        //private bool ProcessCommand(ChatMessage currentMessage)
        //{
        //    string str = currentMessage.Body;
        //    if (str.IndexOf("!") == 0)
        //    {
        //        str = str.Remove(0, 1);
        //        string command,arg;
        //        if (str.IndexOf(" ") != -1)
        //        {
        //            command = str.Remove(str.IndexOf(" "));
        //            arg = str.Remove(0, str.IndexOf(" ") + 1);
        //        }
        //        else
        //        {
        //            command = str;
        //            arg = "";
        //        }


        //            if (!_commands.ContainsKey(command))
        //                return ProcessSimpleCommand(str, currentMessage);
        //            else
        //                return _commands[command](arg, currentMessage);
        //    }
        //     return false;
        //}
        //private bool ProcessSimpleCommand(string command, ChatMessage ms)
        //{
        //    string res = "";
        //    var keys = new List<string>(simpleCommands.Keys);
        //    foreach (string k in keys)
        //    {
        //        if (command == k)
        //            res = simpleCommands[k].Clone().ToString();
        //    }
        //    res=res.Replace(@"\\",@"\");
        //    res = res.Replace("\\n", "\n");
        //    if (res=="")
        //        return false;
        //    ms.Chat.SendMessage(res);
        //    return true;
        //}



       
    }
}
