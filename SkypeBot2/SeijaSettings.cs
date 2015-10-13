using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;





namespace SkypeBot2
{

   [Serializable]
       public class SettingsRecord
    {
       public string botName;
       public string masterName;
       public string commandSymvol;
       public string homeconfa;
       public string IRCkey;
       public DateTime lastRestart;
       public List<String> botNames;
       public List<String> Streams;
    }
       class SeijaSettings
       {
           public SeijaSettings()
           {
               Read();
           }
           string path = @"Settings/MainSettings.ext";
           public SettingsRecord Values = new SettingsRecord();
           public void Write()
           {
               if (!Directory.Exists(@"Settings"))
                   Directory.CreateDirectory(@"Settings");
               if (!File.Exists(path))
                   using (File.Create(path)) ;
               var serializer = new XmlSerializer(typeof(SettingsRecord));
               var sw = new StreamWriter(path);
               serializer.Serialize(sw, Values);
               sw.Close();
           }
           public void Read()
           {
               if (!Directory.Exists(@"Settings"))
                   Directory.CreateDirectory(@"Settings");
               if (!File.Exists(path))
                   using (File.Create(path)) ;
               var stream = new StreamReader(path);
               if (stream.BaseStream.Length != 0)
               {
                   var ser = new XmlSerializer(typeof(SettingsRecord));
                   Values = (SettingsRecord)ser.Deserialize(stream);
               }
               stream.Close();
           }
           void LoadDeafaultSettings()
           {
                 Values.botName = "seijabot";
                 Values.masterName = "fourslash";
                 Values.commandSymvol = "!";
                 Values.homeconfa = "#splasher-_-/$omgwtfgglol;7fa80f21182dcf70";
                 Values.lastRestart = DateTime.Now;
                 Values.IRCkey = "";
                 Values.botNames = new List<string>                               
            {
                "Сейджа",
                "Сейдзя",
                "сейджа",
                "сейдзя"
            };

                 Values.Streams = new List<string>
               {
                  "forsenlol",
                   "dreadztv",
                    "amazhs",
                    "demolition_d",
                    "sing_sing",
                     "helixsnake",
                     "kolento",
                     "cirno_tv"

               };
           }
       }

    }

   

