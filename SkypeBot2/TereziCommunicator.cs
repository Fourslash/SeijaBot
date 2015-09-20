using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourslashNettest;

namespace SkypeBot2
{
    class TereziCommunicator
    {
        static Listener lst;
        static Sender snd;
        public static void Init()
        {
           
            try
            {

                lst = new Listener(11223);

                snd = new Sender(11224, "localhost");

                lst.NewStringCollected += lst_NewStringCollected;

                Task.Factory.StartNew(() =>
               lst.Start()
               );
            }
            catch (Exception ex)
            {
                SeijaCommander.skype.SendMessage(SeijaCommander.Settings.Values.masterName, "Error on initialisation: "+ex.Message);
            }


        }

        static void lst_NewStringCollected(string NewData)
        {
            try
            {
                GetAnswer(NewData);
            }
            catch (Exception ex)
            {
                SeijaCommander.skype.SendMessage(SeijaCommander.Settings.Values.masterName, "Error while getting text from Terezi " + ex.Message);
            }
            
        }
        public static void SayToTerezi(String str)
        {
            try
            {
                snd.SendString(str);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        static void GetAnswer(string str)
        {

            //List<string> strings = new List<string>(str.Split(' '));
            //if (strings.Count < 1)
            //    throw new Exception("error on getting string from Terezi");
            if( str.IndexOf("%")==0)
            {
                string temp=str.Substring(1);
                List<string> strings = new List<string>(temp.Split(' '));
                string konf = strings[0];
                strings.RemoveAt(0);
                string message = string.Join(" ",strings);
                SeijaCommander.skype.get_Chat(konf).SendMessage(ChatFormat(message));
            }
            else
                SeijaCommander.skype.SendMessage(SeijaCommander.Settings.Values.masterName, ChatFormat(str));

                
        }

        static string ChatFormat(string str)
        {
            return string.Format("[{0}] Terezi: {1}",DateTime.Now.ToString("HH:mm:ss"),str);
        }


    }
}
