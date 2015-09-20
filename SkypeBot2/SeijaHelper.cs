using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;


namespace SkypeBot2
{
    public class SeijaHelper
    {
        public static class RandomProvider
        {
            private static int seed = Environment.TickCount;

            private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(() =>
                new Random(Interlocked.Increment(ref seed))
            );

            public static Random GetThreadRandom()
            {
                return randomWrapper.Value;
            }
        }

        public static int ticks = 0;
        public static bool isEn = true;
        public static bool isStop = false;
        public static string GetRandomMessage(List<string> messages)
        {
            //Random r = new Random(Convert.ToInt32(DateTime.Now.Millisecond * DateTime.Now.Second));
            return messages[RandomProvider.GetThreadRandom().Next(0, messages.Count())];
        }
        public static void SendToMaster(string str)
        {
            SeijaCommander.skype.SendMessage(SeijaCommander.Settings.Values.masterName, str);
        }

        public static int db_tag_pages(string tag)
        {
            try
            {
                using (var w = new System.Net.WebClient())
                {
                    String json_data = w.DownloadString(@"https://danbooru.donmai.us/tags.json?search[name_matches]=" + tag);
                    json_data = json_data.Remove(0, 1);
                    json_data = json_data.Remove(json_data.Length - 1, 1);
                    DbTag jTag = JsonConvert.DeserializeObject<DbTag>(json_data);
                    return (Convert.ToInt32(jTag.post_count) / 20) + 1;
                }
            }
            catch (Exception ex)
            {
                return 1;
            }

        }
        public static int FixedRandom(int start, int end)
        {
          return RandomProvider.GetThreadRandom().Next(start,end );
        }
        public static void loadTXT(List<String> lst, string path)
        {
            lst.Clear();
            using (System.IO.StreamReader file = File.OpenText(path))
            {
                string line = "";
                while ((line = file.ReadLine()) != null)
                    lst.Add(line);
            }
        }

        public static void write_log(string msg)
        {
            using (System.IO.StreamWriter file = File.AppendText(@"Settings\log.txt"))
            {
                file.WriteLine(DateTime.Now.ToString("u") + " " + msg);
            }
        }
       
    }
}
