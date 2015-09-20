using System;
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

public enum statement { will, live, ended, postponed };
public struct matchmain
{
    public matchmain(string t1, string t2, string t1P, string t2P, statement st, string win, string e, string lk)
    {
        team1 = t1;
        team2 = t2;
        team1P = t1P;
        team2P = t2P;
        // isLive = live;
        // ended = endd;
        state = st;
        winner = win;
        events = e;
        link = lk;

        toRemove = false;
    }
    public string team1;
    public string team2;
    public string team1P;
    public string team2P;
    public statement state;
    //    public bool isLive;
    //      public bool ended;
    public string winner;
    public string events;
    public string link;
    public bool toRemove;
}

namespace SkypeBot2
{
    public class GameChecker
    {
        //System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
        public static string error;
        public static bool isJustOn = true;
        private static List<matchmain> matches = new List<matchmain>();
        public static List<string> teams= new List<string>();
        public static List<string> results = new List<string>();



        public static bool AddTeam (string team)
        {
            if (teams.Contains(team))
                return false;
            teams.Add(team);
            if (RevriteTeams()==false)
            {
                teams.Remove(team);
                return false;
            }
            return true;
        }

        public static bool DeleteTeam (string team)
        {
            if (!teams.Contains(team))
                return false;
            teams.Remove(team);
            if (RevriteTeams()==false)
            {
                teams.Add(team);
                return false;
            }
            return true;
        }

        private static bool RevriteTeams()
        {
            try
            {
                var serializer = new XmlSerializer(teams.GetType());
                var sw = new StreamWriter(@"Settings\dota_teams.ext");
                serializer.Serialize(sw, teams);
                sw.Close();
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool ReReadTeams()
        {
            try
            {
                var stream = new StreamReader(@"Settings\dota_teams.ext");
                if (stream.BaseStream.Length != 0)
                {
                    var ser = new XmlSerializer(teams.GetType());
                    teams = (List<string>)ser.Deserialize(stream);
                }
                stream.Close();
            }
            catch (System.Exception e)
            {
                SeijaHelper.write_log(e.Message);
                error = e.Message;
                return false;
            }
            return true;
        }

        public static void write(List<matchmain> m)
        {
            //  error = m.GetType().ToString();
            var serializer = new XmlSerializer(m.GetType());
            var sw = new StreamWriter(@"Settings\matches.ext");
            serializer.Serialize(sw, m);
            sw.Close();
        }
        private static void read()
        {
            try
            {
                var stream = new StreamReader(@"Settings\matches.ext");

                if (stream.BaseStream.Length != 0)
                {

                    var ser = new XmlSerializer(matches.GetType());
                    matches = (List<matchmain>)ser.Deserialize(stream);

                }
                stream.Close();
            }
            catch (System.Exception e)
            {
                error = e.Message;
            }

        }

        public static void check()
        {
            List<matchmain> matches_temp = new List<matchmain>();
            string url = "http://dota2lounge.com/";
            var Webget = new HtmlWeb();
            var doc = Webget.Load(url);
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//div[@class='matchmain']"))
            {
                string team1;
                string team2;
                string team1P;
                string team2P;
                // bool isLive;
                //bool ended;
                statement st;
                string winner = "";
                string link;

                //             
                string link_string = node.SelectSingleNode(".//a[@href]").Attributes["href"].Value;
                link = "http://dota2lounge.com/" + link_string;
                //
                if (link_string != "match?m=6450")
                {

                    string events = node.SelectSingleNode(".//div[@class='eventm']").InnerText;
                    //
                    string time_string = node.SelectSingleNode(".//div[@class='whenm']").InnerText;
                    if (time_string.Contains("Postponed"))
                        st = statement.postponed;
                    else if (time_string.Contains("LIVE"))
                        st = statement.live;
                    else if (time_string.Contains("ago"))
                        st = statement.ended;
                    else
                        st = statement.will;

                    //
                    string team1_string = node.SelectNodes(".//div[@class='teamtext']")[0].InnerHtml;
                    team1 = team1_string.Substring((team1_string.IndexOf("<b>") + 3), (team1_string.IndexOf("</b>") - 3));
                    team1P = team1_string.Substring((team1_string.IndexOf("<i>") + 3), team1_string.IndexOf("</i>") - (team1_string.IndexOf("<i>") + 3));
                    string team2_string = node.SelectNodes(".//div[@class='teamtext']")[1].InnerHtml;
                    team2 = team2_string.Substring((team2_string.IndexOf("<b>") + 3), (team2_string.IndexOf("</b>") - 3));
                    team2P = team2_string.Substring((team2_string.IndexOf("<i>") + 3), team2_string.IndexOf("</i>") - (team2_string.IndexOf("<i>") + 3));
                    if (st == statement.ended)
                    {
                        if (node.SelectNodes(".//div[@class='team']")[0].InnerHtml.Contains("won.png"))
                            winner = team1;
                        else if (node.SelectNodes(".//div[@class='team']")[1].InnerHtml.Contains("won.png"))
                            winner = team2;
                    }

                    if (teams.Contains(team1)||teams.Contains(team2))
                        matches_temp.Add(new matchmain(team1, team2, team1P, team2P, st, winner, events, link));
                }
            }
            if (isJustOn == true)
            {
                write(matches_temp);
                isJustOn = false;
            }
            else
            {
                read();
                compare_matches(matches_temp);
            }
        }

        public static void compare_matches(List<matchmain> match_t)
        {
            foreach (matchmain mT in match_t)
            {
                int num = matches.FindIndex(x => x.link == mT.link);
                if (num == -1)
                {
                    matches.Add(mT);
                    announce(mT);
                }
                else
                {
                    matchmain temp = matches[num];
                    temp.team1P = mT.team1P;
                    temp.team2P = mT.team2P;
                    if (temp.state != mT.state)
                    {
                        if (mT.state != statement.will)
                        {
                            temp = mT;
                            announce(temp);
                        }
                    }
                    matches[num] = temp;
                }
            }
            for (int i = 0; i < matches.Count; i++)
            {
                if (match_t.FindIndex(x => x.link == matches[i].link) == -1)
                {
                    matchmain temp = matches[i];
                    temp.toRemove = true;
                    matches[i] = temp;
                }
                matches.RemoveAll(x => x.toRemove == true);
            }
            write(matches);
        }
        private static void announce(matchmain mt)
        {
            if (mt.state == statement.live)
            {
                results.Add(mt.team1 + "(" + mt.team1P + ") VS " + mt.team2 + "(" + mt.team2P + ") went live on " + "[" + mt.events + "] " + mt.link);
            }
            else if (mt.state == statement.postponed)
            {
                results.Add(mt.team1 + "(" + mt.team1P + ") VS " + mt.team2 + "(" + mt.team2P + ") on " + "[" + mt.events + "] was postponed " + mt.link);
            }
            else if (mt.state == statement.ended)
            {
                string res;
                if (mt.winner == "")
                    res = "DRAW";
                else
                    res = "Winner is " + mt.winner;
                results.Add(mt.team1 + "(" + mt.team1P + ") VS " + mt.team2 + "(" + mt.team2P + ") on " + "[" + mt.events + "] is over. " + res + " " + mt.link);
            }
        }
    }
}
