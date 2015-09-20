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
namespace SkypeBot2
{
    public partial class Seija
    {
        private bool cmd_db_find(string argh, ChatMessage ms)
        {
            try
            {
                string result = "";
                using (var w = new System.Net.WebClient())
                {
                    String json_data = w.DownloadString(@"https://danbooru.donmai.us/tags.json?search[name_matches]=" + argh);
                    List<DbTag> arr = JsonConvert.DeserializeObject<List<DbTag>>(json_data);
                    if (arr.Count==0)
                        result = "Теги не найдены";
                    else
                    {
                        foreach (DbTag tg in arr)
                        {
                            result += tg.name + "\n";
                        }
                    }
                }
                ms.Chat.SendMessage(result);
                return true;
            }
            catch (Exception ex)
            {
                ms.Chat.SendMessage("ошибка: " + ex.Message);
                return false;
            }
        }
        private bool cmd_db(string argh, ChatMessage ms)
        {
            try
            {
                //if (ms.Sender.Handle=="zoljiin")
                //{
                //    System.Threading.Thread.Sleep(1000);
                //    string result = "";
                //    result = "http://danbooru.donmai.us/posts/1058042";
                //    result += "\nCopyright: Newtonsoft.Json.ArrayException";
                //    result += "\nCharacters: Newtonsoft.Json.ArrayException";
                //    result += "\nTags: Newtonsoft.Json.ArrayException";
                //    result += "\nRating: Newtonsoft.Json.ArrayException";
                //    ms.Chat.SendMessage(result);
                //}
                //else
                //{
                    string [] tags=null;
                    if (argh!="")
                        tags = argh.Split(' ');
                
                    int page_max=1000;
                    string result = "";
                    using (var w = new System.Net.WebClient())
                    {
                        if (argh != "")
                            foreach (string tag in tags)
                            {
                                    if (page_max>SeijaHelper.db_tag_pages(tag))
                                        page_max = SeijaHelper.db_tag_pages(tag);            
                            }
                        int page = SeijaHelper.FixedRandom(1, page_max);
                        String json_data = w.DownloadString(@"https://danbooru.donmai.us/posts.json?tags=" + argh + "&page=" + page.ToString());
                        String json_data2 = "{'results':" + json_data + "}";
                        JObject answer = JObject.Parse(json_data2);
                        if (answer.HasValues)
                        {
                            {
                                List<DbInfo> arr = JsonConvert.DeserializeObject<List<DbInfo>>(json_data);
                                int roll = SeijaHelper.FixedRandom(0, arr.Count);
                                if (ms.Sender.Handle=="zoljiin")
                                    result = "http://danbooru.donmai.us/posts/1058042";
                                else
                                    result = "https://danbooru.donmai.us/posts/" + arr[roll].id;
                                result+="\nCopyright: "+ arr[roll].tag_string_copyright;
                                result+="\nCharacters: "+ arr[roll].tag_string_character;
                                result+="\nTags: "+ arr[roll].tag_string_general;
                                result += "\nRating: " + arr[roll].rating;
                                
                            }
                        }
                        else
                            result = "Комбинация тегов не найдена (✿◕⁀◕)";


                    //}
                    ms.Chat.SendMessage(result);
                }
                return true;
            }
            catch(Exception ex)
            {
                ms.Chat.SendMessage("ошибка: "+ex.Message);
                return false;
            }
        }
        private bool cmd_Off(string argh, ChatMessage ms)
        {
            //if (sender == "fourslash")
            //{
                SeijaHelper.isEn = false;
                SeijaCommander.skype.ChangeUserStatus(TUserStatus.cusAway);
                List<string> messages = new List<string>
            {
                @"/me будет остановлена (✿◕⁀◕)",

            };
                ms.Chat.SendMessage(SeijaHelper.GetRandomMessage(messages));
                return true;

            //}
            //else
            //    return "Ты не можешь этого сделать, раб (✿-̶●̃益●̶̃)";
        }
        private bool cmd_On(string argh, ChatMessage ms)
        {
            //if (sender == "fourslash")
            //{
                SeijaHelper.isEn = true;
                GameChecker.isJustOn = true;
                SeijaCommander.skype.ChangeUserStatus(TUserStatus.cusOnline);
                ms.Chat.SendMessage( "Бот будет включен (✿◕⁀◕)");
                return true;
            //}
            //else
            //    return "Ты не можешь этого сделать, раб (✿-̶●̃益●̶̃)";
        }
        private bool cmd_Stop(string argh, ChatMessage ms)
        {
            //if (sender == "fourslash")
            //{               
            SeijaCommander.skype.ChangeUserStatus(TUserStatus.cusInvisible);
            List<string> messages = new List<string>
            {
                @"/me будет выключена (✿◕⁀◕)",
                @"/me отправляется спать",
                @"/me has ceased to be",
                @"/me is a late bot",
                @"/me is expired and gone to meet its maker",
                @"/me is a stiff",
                @"/me 's metabolic processes are now history",
                @"/me is shuffled off 'is mortal coil, run down the curtain and joined the bleedin' choir invisible",
                @"/me kicked the bucket",
                @"/me is no more",
                @"/me is an ex bot",
                @"http://i.imgur.com/tmtnMCD.gif",
            };
            ms.Chat.SendMessage(SeijaHelper.GetRandomMessage(messages)); ;
                System.Windows.Application.Current.Shutdown();
                return true;
            //}
            //else
            //    return "Ты не можешь этого сделать, раб (✿-̶●̃益●̶̃)";
        }
        private bool cmd_update_all(string argh, ChatMessage ms)
        {
            BotCommand.DeSerialazeSC();
            foreach (BotCommand cmd in commands)
            {
                BotCommand.DeSerialaze(cmd);
            }
            ms.Chat.SendMessage("Обновлено (✿◕⁀◕)");
            return true;
        }

        private bool cmd_leave(string argh, ChatMessage ms)
        {
            SeijaCommander.skype.get_Chat(kantaekonfa).SendMessage(@"/leave");
            SeijaCommander.skype.get_Chat(kantaekonfa).Leave();
            
            return true;
        }

        private bool cmd_Terezi(string argh, ChatMessage ms)
        {
            if (argh.Length == 0)
                ms.Chat.SendMessage("Я не могу послать пустое сообщение!");
            
            try
            {
                ms.Chat.SendMessage("Пишу Терези...");
                TereziCommunicator.SayToTerezi(ms.ChatName+" "+argh);
            }
            catch (Exception ex)
            {
                ms.Chat.SendMessage("Не могу связаться с Терези!");
            }
            return true;
        }

        private bool cmd_TereziUP(string argh, ChatMessage ms)
        {
            if (Process.GetProcessesByName("KanColleBotFinal.exe").Length > 0)
            {
                 ms.Chat.SendMessage("Терези уже запущена!");
                 return true;
            }
            try
            {
                System.Diagnostics.Process.Start(@"KKBOT\KanColleBotFinal.exe");
                ms.Chat.SendMessage("Терези запущена!");
            }
            catch (Exception ex)
            {
                ms.Chat.SendMessage("Не могу связаться с Терези!");
            }
            return true;
        }

        private bool cmd_TereziDown(string argh, ChatMessage ms)
        {
            if (Process.GetProcessesByName("KanColleBotFinal.exe").Length == 0)
            {
                ms.Chat.SendMessage("Терези не включена!");
                return true;
            }
            try
            {
                foreach (var process in Process.GetProcessesByName("KanColleBotFinal.exe"))
                {
                    process.Kill();
                }
                ms.Chat.SendMessage("Выключена!");
            }
            catch (Exception ex)
            {
                ms.Chat.SendMessage("Не могу связаться с Терези!");
            }
            return true;
        }


        private bool cmd_Help(string argh, ChatMessage ms)
        {
            string help_string="";
            if (argh=="")
            {
                //var keys = new List<string>(_commands.Keys);
                //foreach (string key in keys)
                //    help_string += key + " | ";
                foreach (BotCommand bt in commands)
                    help_string += bt.CommandName + " | ";
                help_string += "\r\n";
                help_string += @"а также https://drive.google.com/file/d/0B8yGXlXkNqn-QVUtalVDa2k4cGs/view?usp=sharing";
            }
            else
            {
                //if (_commands.ContainsKey(argh))
                //{
                //    help_string = argh + " " + _help[_commands[argh]];
                //}
                help_string = commands.Find(x => x.CommandName == argh).HelpString;
                if (help_string == null)
                {
                    ms.Chat.SendMessage("Команда не найдена!");
                    return false;
                }
            }
            ms.Chat.SendMessage(help_string);
            return true;
        }

        private bool cmd_Help_add(string argh, ChatMessage ms)
        {
            string command, helpstring;
            int point;
            point = argh.IndexOf(" ");
            command = argh.Substring(0, point);
            helpstring = argh.Substring(point + 1);
            //Random rnd = new Random();
            BotCommand temp = commands.Find(x => x.CommandName == command);
            if (temp==null)
            {
                ms.Chat.SendMessage("Команда не найдена!");
                return false;
            }
            temp.HelpString=helpstring;
            temp.Serialaze();
            ms.Chat.SendMessage("Описание команды успешно добавлено");             
            return true;
        }

        private bool cmd_set_enabled(string argh, ChatMessage ms)
        {
            string command, state;
            int point;
            point = argh.IndexOf(" ");
            command = argh.Substring(0, point);
            state = argh.Substring(point + 1);
            //Random rnd = new Random();
            BotCommand temp = commands.Find(x => x.CommandName == command);
            if (temp==null)
            {
                ms.Chat.SendMessage("Команда не найдена!");
                return false;
            }
            temp.IsEnabled=Convert.ToBoolean(state);
            temp.Serialaze();
            ms.Chat.SendMessage("Команда изменена");             
            return true;
        }
        
        private bool cmd_set_master_only(string argh, ChatMessage ms)
        {
            string command, state;
            int point;
            point = argh.IndexOf(" ");
            command = argh.Substring(0, point);
            state = argh.Substring(point + 1);
            //Random rnd = new Random();
            BotCommand temp = commands.Find(x => x.CommandName == command);
            if (temp==null)
            {
                ms.Chat.SendMessage("Команда не найдена!");
                return false;
            }
            temp.IsMasterOnly=Convert.ToBoolean(state);
            temp.Serialaze();
            ms.Chat.SendMessage("Команда изменена");             
            return true;
        }
        private bool cmd_battle(string argh,ChatMessage ms)
        {
            try
            {
                SeijaHelper.isEn = false;
                if (argh != "")
                    SeijaCommander.DongerBTL.InitBatle(ms.Sender.FullName, ms.Chat, argh);
                else
                    SeijaCommander.DongerBTL.InitBatle(ms.Sender.FullName, ms.Chat);
                      
                //dong.DoShit(ms.Sender.FullName, ms.Chat);

                SeijaHelper.isEn = true;
            }
            catch (Exception ex)
            {
                ms.Chat.SendMessage(ex.Message);
                SeijaHelper.isEn = true;
            }
            return true;
        }

        private bool cmd_Tobi(string argh, ChatMessage ms)
        {
            SeijaHelper.isEn = false;

                ms.Chat.SendMessage("NO ONE HAS EVER DONE THAT!");
                System.Threading.Thread.Sleep(1500);
                ms.Chat.SendMessage("no one");
                System.Threading.Thread.Sleep(500);
                ms.Chat.SendMessage("has");
                System.Threading.Thread.Sleep(350);
                ms.Chat.SendMessage("EVER");
                System.Threading.Thread.Sleep(500);
                ms.Chat.SendMessage("done");
                System.Threading.Thread.Sleep(400);
                ms.Chat.SendMessage("that");
                System.Threading.Thread.Sleep(350);
                ms.Chat.SendMessage("in the history");
                System.Threading.Thread.Sleep(650);
                ms.Chat.SendMessage("of");
                System.Threading.Thread.Sleep(150);
                ms.Chat.SendMessage("DOTA");
               
            SeijaHelper.isEn = true;
            return true;

        }
        private bool cmd_Jaraxxus(string argh,ChatMessage ms)
        {
            SeijaHelper.isEn = false;
            
            //skype.CurrentUser.
            //skype.Client.OpenFileTransferDialog("TargetUserHandle", "sourcePath");
            //skype. = "Lord Jaraxxus";
            //Random rnd = new Random();
            string name=ms.Chat.Members[SeijaHelper.FixedRandom(0,ms.Chat.Members.Count)].Handle;
            if (name == "splasher-_-" || name == "seijabot" || name == "xaos42")
            {
                ms.Chat.SendMessage("TRIFLING GNOME!"); 
                System.Threading.Thread.Sleep(1000);
                ms.Chat.SendMessage("YOU FACE JARA..");
                System.Threading.Thread.Sleep(1300);
                ms.Chat.SendMessage("JUSTICE DEMANDS RETRIBUTION");
                
            }
            else
            {
                ms.Chat.SendMessage("TRIFLING GNOME!");
                System.Threading.Thread.Sleep(1000);
                ms.Chat.SendMessage("YOUR ARROGANCE WILL BE YOUR UNDOING!!");
                System.Threading.Thread.Sleep(1500);
                ms.Chat.SendMessage("/kick "+name);
                System.Threading.Thread.Sleep(500);
                ms.Chat.SendMessage("     U");
                System.Threading.Thread.Sleep(300);
                ms.Chat.SendMessage("   FACE");
                System.Threading.Thread.Sleep(300);
                ms.Chat.SendMessage("JARAXXUS");
                System.Threading.Thread.Sleep(1500);
                ms.Chat.SendMessage("     U");
                System.Threading.Thread.Sleep(300);
                ms.Chat.SendMessage("   FACE");
                System.Threading.Thread.Sleep(300);
                ms.Chat.SendMessage("JARAXXUS");
                System.Threading.Thread.Sleep(1500);
                ms.Chat.SendMessage("EREDAR LORD OF THE BURNING LEGION");
                System.Threading.Thread.Sleep(500);
                ms.Chat.SendMessage("/add " + name);
       
            }
            SeijaHelper.isEn = true;
           return true;
            
        }
        private bool cmd_UpdateKicklist(string argh,ChatMessage ms)
        {
            //if (sender == "fourslash")
            //{
                //SeijaHelper.loadTXT(, @"Settings\kicklist.txt");
                SeijaCommander.KickList.Read();
                ms.Chat.SendMessage("словарь обновлен (✿◕⁀◕)");
                return true;
            //}
            //ms.Chat.SendMessage("Ты не можешь этого сделать, раб (✿-̶●̃益●̶̃)");
            //return false;
        }
        private bool cmd_Hug(string argh,ChatMessage ms)
        {
            if (ms.Sender.Handle == "fourslash")
            {
                ms.Chat.SendMessage("/(✿◕⁀◕)\\");
                return true;
            }
            ms.Chat.SendMessage("( ° ͜ʖ͡°)╭∩╮ Hey "+ms.Sender.FullName+", here's a \"hug\" for you. ( ° ͜ʖ͡°)╭∩╮");
            return false;
        }

        private bool cmd_UpdateAll(string argh, ChatMessage ms)
        {
            SeijaCommander.REInit();
            ms.Chat.SendMessage("Все файлы настроек были обновлены(✿◕⁀◕)");
            return true;
        }

        private bool cmd_GetChatname(string argh, ChatMessage ms)
        {
            ms.Chat.SendMessage(ms.ChatName);
            return false;

        }
        private bool cmd_Add_donger(string argh,ChatMessage ms)
        {
            //using (System.IO.StreamWriter file = File.AppendText(@"Settings\dongerlist.txt"))
            //    {
            //        file.WriteLine(argh);
            //    }
            //SeijaHelper.loadTXT(dongers, @"Settings\dongerlist.txt");
            SeijaCommander.DongerList.AddString(argh);
                ms.Chat.SendMessage("Ваш донгер был добавлен (✿◕⁀◕)");
            return true;
        }
        private bool cmd_Add_team(string argh, ChatMessage ms)
        {
            if (ms.ChatName != dotakonfa && ms.ChatName != homeconfa)
            {
                ms.Chat.SendMessage("Нельзя использовать тут (✿◕⁀◕)");
                return false;
            }
            bool result = GameChecker.AddTeam(argh);
            if (result==true) 
                ms.Chat.SendMessage("Команда "+argh+" была добавлена в список (✿◕⁀◕)");
            else
                ms.Chat.SendMessage("Произошла ошибка. Возможно команда уже есть в списке или случилась какая-нибудь хуйня (✿◕⁀◕)");
            return result;
        }

        private bool cmd_Delete_team(string argh, ChatMessage ms)
        {
            if (ms.ChatName != dotakonfa && ms.ChatName != homeconfa)
            {
                ms.Chat.SendMessage("Нельзя использовать тут (✿◕⁀◕)");
                return false;
            }
            bool result = GameChecker.DeleteTeam(argh);
            if (result == true)
                ms.Chat.SendMessage("Команда " + argh + " была удалена из списка (✿◕⁀◕)");
            else
                ms.Chat.SendMessage("Произошла ошибка. Возможно команда отсутствует в списке или случилась какая-нибудь хуйня (✿◕⁀◕)");
            return result;
        }
        private bool cmd_Show_teams(string argh, ChatMessage ms)
        {
            if (ms.ChatName != dotakonfa && ms.ChatName != homeconfa)
            {
                ms.Chat.SendMessage("Нельзя использовать тут (✿◕⁀◕)");
                return false;
            }
            string result ="Список команд, матчи которых отслеживаются в настоящее время: \r\n";
            foreach (string team in GameChecker.teams)
            {
                result += " * " + team + "\r\n";
            }
            result += "\r\n Для добавления команды введите dota_team_add %team%";
            result += "\r\n Для удаления команды введите dota_team_delete %team%";
            result += "\r\n Пожалуйста, будьте внимательны при вводе названия команды, а лучше копируйте с доталаунжа. При несоответсвии регистра/ наличии лишних символов все взорвется нахуй (✿◕⁀◕)";
            ms.Chat.SendMessage(result);
            return true;
        }

        private bool cmd_Add_pasta(string argh,ChatMessage ms)
        {

                //string pastastr = str.Remove(0, 10);
                //using (System.IO.StreamWriter file = File.AppendText(@"D:\SHARE\pastalist.txt"))
                //{
                //    file.WriteLine(pastastr);
                //}
                //loadTXT(pasta, @"D:\SHARE\pastalist.txt");
                //return "Ваша паста была добавлена (✿◕⁀◕)";
            //using (System.IO.StreamWriter file = File.AppendText(@"Settings\pastalist.txt"))
            //    {
            //        file.WriteLine(argh);
            //    }
            //SeijaHelper.loadTXT(pasta, @"Settings\pastalist.txt");
            SeijaCommander.PastaList.AddString(argh);
                ms.Chat.SendMessage("Ваша паста была добавлена (✿◕⁀◕)");
            return true;
            
        }
        private bool cmd_Kick(string argh,ChatMessage ms)
        {
            //if (sender == "fourslash")
            //{
            //   return ("/" + str);
            //}
            //else
            //   return "Ты не можешь этого сделать, раб (✿-̶●̃益●̶̃)";
            //if (sender == "fourslash")
            //{
               ms.Chat.SendMessage("/" + argh);
               return true;
            //}
            //ms.Chat.SendMessage("Ты не можешь этого сделать, раб (✿-̶●̃益●̶̃)");
            //return false;
        }


        private bool cmd_AddConf(string argh, ChatMessage ms)
        {
            try
            {
                
                SeijaCommander.skype.CreateChatMultiple(ms.Chat.Members);
            }
            catch (Exception ex)
            {
                ms.Chat.SendMessage(ex.Message);
                return false;
            }
            return false;
        }



        private bool cmd_Dice(string argh, ChatMessage ms)
        {
            
            try
            {
                if (argh == string.Empty ||
                    ((!argh.ToLower().Contains("d")) && (!argh.ToLower().Contains("д")))
                    )
                {
                    ms.Chat.SendMessage("Формат броска: NdM+K где N - количество дайсов, M - формат дайса, K - модификатор");
                    return false;
                }
                int dNum=argh.ToLower().IndexOf("d");
                if (dNum==-1)
                    dNum=argh.ToLower().IndexOf("д");
                int diceCount = Convert.ToInt32(argh.Substring(0, dNum));

                int modifer = 0;
                if (argh.Contains("+"))
                {
                    int modNum = argh.IndexOf("+");
                    modifer = Convert.ToInt32(argh.Substring(modNum + 1));
                    argh=argh.Remove(modNum);
                }
                else if (argh.Contains("-"))
                {
                    int modNum = argh.IndexOf("-");
                    modifer = Convert.ToInt32(argh.Substring(modNum + 1))*-1;
                    argh = argh.Remove(modNum);
                }

                int diceType = Convert.ToInt32(argh.Substring(dNum + 1));
                List<int> results= new List<int>();
                string result = "Результат: ";
                for (int i =0;i<diceCount;i++)
                {
                    if (i != 0 && i != diceCount)
                        result += ", ";
                    int t=SeijaHelper.RandomProvider.GetThreadRandom().Next(1,diceType+1);
                    results.Add(1*t);
                    result += t;
                }
                if (modifer != 0)
                {
                    if (modifer>0)
                        result += " +" + modifer;
                    else
                        result += " " + modifer;
                }
                int total=0;
                //ms.Chat.SendMessage("СОДЕРЖИМОЕ results:");
                foreach (int num in results)
                {
                    //ms.Chat.SendMessage(num.ToString());
                    total += num;
                }
                total+=modifer;
                result += "\n" + "Итого: " + total;
                ms.Chat.SendMessage(result);
                return true;
            }
            catch(Exception ex)
            {
                ms.Chat.SendMessage("Не могу совершить данный бросок (✿◕⁀◕)");
                return false;
            }
        }
        private bool cmd_Rng(string argh,ChatMessage ms)
        {
            int num1, num2, point;
            string numbers = argh;
            point = numbers.IndexOf(" ");
            num1 = Convert.ToInt32(numbers.Substring(0, point));
            num2 = Convert.ToInt32(numbers.Substring(point + 1));
            //Random rnd = new Random();
            ms.Chat.SendMessage(SeijaHelper.FixedRandom(num1,num2).ToString());
            return true;
        }

        private bool cmd_Restart(string argh, ChatMessage ms)
        {

            SeijaCommander.skype.ChangeUserStatus(TUserStatus.cusInvisible);
            ms.Chat.SendMessage("Произвожу перезапуск сервера");
            SeijaCommander.skypeConnector.RestartClient();
            return false;
        }

        private bool cmd_SHIP(string argh,ChatMessage ms)
        {
            try
            {
                int num, point;
                string name;
                point = argh.IndexOf(" ");
                name = argh.Substring(0, point);
                num = Convert.ToInt32(argh.Substring(point + 1));
                num = num > 100 ? 100 : num;
                ms.Chat.SendMessage("Флот отправляется");
                for (int i=0;i<num;i++)
                {
                    SeijaCommander.skype.SendMessage(name, "ВСЁ КОРАБЛЬ, ВСЁ В АРТСТАЙЛА , МИНУС АРТСТАЙЛ , КУРОКИ УБИРАЕТ АРТСТАЙЛА, ГОБЛИН УЛЬТУЕТ, ЗАБРАТЬ НАДО ХОТЬ КОГО-ТО,СТЕНКУ СТАВИТ, ОЙОЙОЙ КАКАЯ ХОРОШАЯ СТЕНА У ТОЛСТОЙ СКАТИНЫ, КОПАЕТ СЕЙЧАС ОНИ КУРОКИ, ПЫТАЕТСЯ ЧТО-ТО СДЕЛАТЬ НЕУБИВАЕТ НИКОГО, ФОБОС УЛЬТУЕТ НИКОГО НЕ ЗАБИРАЕТ ЗДЕСЬ КРИПЫ ЛОСТА ПЫТАЮТСЯ ЧТО-ТО СДЕЛАТЬ ИХ ТУТ ЖЕ УБИВАЕТ КУНКА! НА ТОРРЕНТ ОПЯТЬ ВСЕ, ВСЕ ЧЕТВЕРО ПОПАДАЮТ НА ТОРРЕНТ, ДЕНДИ УХОДИТ ПРОСТО С ТП, ЗАБИРАЮТ ЛОСТА, ЗАБИРАЮТ ФОБОСА, ЗАБИРАЮТ ГОБЛАКА, МОЖНО ЛИВАТЬ, ЭТО БЛЯТЬ НЕ ИГРА, ЭТО ПРОСТО ПОШЛИ ОНИ НАХУЙ");
                    System.Threading.Thread.Sleep(1500);
                }
                ms.Chat.SendMessage("Флот прибыл");
                return true;
            }
            catch (Exception ex)
            {
                ms.Chat.SendMessage("Флот встретил препятствие на своем пути! Возможно я не имею этого человека у себя в контактах (✿◕⁀◕)");
                return false;
            }
        }
        private bool cmd_Seija(string argh,ChatMessage ms)
        {
            string result, result_reverse; 
            result_reverse=result="";
            foreach (char c in argh)
            {
                if (SeijaCommander.SeijaChars.Pairs.ContainsKey(c.ToString()))
                    result += SeijaCommander.SeijaChars.Pairs[c.ToString()];
                else
                    result+=c.ToString();
            }
            
            for (int i = result.Length - 1; i >= 0;i--)
            {
                result_reverse += result[i];
            }
            result_reverse = "(✿◕⁀◕) " + result_reverse;
            ms.Chat.SendMessage(result_reverse);
            return true;
        }
        private bool cmd_Add(string argh,ChatMessage ms)
        {
            //if (sender == "fourslash")
            //{
                int point;
                string k, v;
                point = argh.IndexOf(" ");
                k = argh.Substring(0, point);
                v = argh.Substring(point + 1);
                //string result = k + ":" + v;
                //using (System.IO.StreamWriter file = File.AppendText(@"Settings\simpleComands.txt"))
                //{
                //    file.WriteLine(result);
                //}
                //SeijaHelper.loadSimpleCommands(simpleCommands);
                SeijaCommander.SimpleCommands.AddPair(k, v);
                ms.Chat.SendMessage("Команда успешно добавлена (✿◕⁀◕)");
                return true;
            //}
            //ms.Chat.SendMessage("Ты не можешь этого сделать, раб (✿-̶●̃益●̶̃)");
            //return false;
        }
        private bool cmd_Streams(string argh,ChatMessage ms)
        {
            //string res="Сейчас онлайн:\n";
            //var keys = new List<string>(SeijaCommander.TwitchChecker.streams.Keys);
            //foreach (string key in keys)
            //{
            //    if (SeijaCommander.TwitchChecker.streams[key] == true)
            //    {
            //         res += ("http://www.twitch.tv/" + key + " is streaming now | "+key+"\n");
            //    }
            //}
            //string res="Сейчас онлайн:
            //foreach (Translation tr in SeijaCommander.TwitchChecker.se)
            //{

            //}
            string res = SeijaCommander.TwitchChecker.NowOnline();
            ms.Chat.SendMessage(res);
            return true;
        }


        private bool cmd_ServerStatus(string argh, ChatMessage ms)
        {
            var cpuCounter = new PerformanceCounter();
            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";
            var memCounter = new PerformanceCounter("Memory", "Available MBytes");
            string res = "Загруженность процессора: " + cpuCounter.NextValue() + "%";
            res += "\nПамяти свободно: " + memCounter.NextValue() + "mb";
            ms.Chat.SendMessage(res);
            return true;
        }
        

        private bool cmd_Uptime(string argh, ChatMessage ms)
        {

            TimeSpan ts = new TimeSpan(0,0,SeijaHelper.ticks);
            //int hr, min, sec;
            //string time = "";
            //hr = SeijaHelper.ticks / 3600;
            //min = (SeijaHelper.ticks - (hr * 3600)) / 60;
            //sec = SeijaHelper.ticks - (hr * 3600 + min * 60);
            //time = hr.ToString() + ":" + min.ToString() + ":" + sec.ToString();
            int days = ts.Days;
            int hours = ts.Hours;
            int minutes = ts.Minutes;
            int seconds = ts.Seconds;
            string time = string.Empty;
            //
            if (days>0)
            {
                string pdj=string.Empty;
                int tmp = days;
                while (tmp >= 100)
                    tmp = tmp / 10 + tmp % 10;
                if (tmp>=10 && tmp<=19 )
                {
                    pdj="дней";
                }
                else
                {
                    if(tmp>9)
                        tmp=tmp%10;
                    if (tmp >= 2 && tmp <= 4)
                        pdj = "дня";
                    else if (tmp == 1)
                        pdj = "день";
                    else
                        pdj = "дней";
                }
                time += (" " + days + " " + pdj);
            }

            //
            if (hours > 0)
            {
                string pdj = string.Empty;
                int tmp = hours;
                while (tmp >= 100)
                    tmp = tmp / 10 + tmp % 10;
                if (tmp >= 10 && tmp <= 19)
                {
                    pdj = "часов";
                }
                else
                {
                    if (tmp > 9)
                        tmp = tmp % 10;
                    if (tmp >= 2 && tmp <= 4)
                        pdj = "часа";
                    else if (tmp == 1)
                        pdj = "час";
                    else
                        pdj = "часов";
                }
                time += (" " + hours + " " + pdj);
            }

            if (minutes > 0)
            {
                string pdj = string.Empty;
                int tmp = minutes;
                while (tmp >= 100)
                    tmp = tmp / 10 + tmp % 10;
                if (tmp >= 10 && tmp <= 19)
                {
                    pdj = "минут";
                }
                else
                {
                    if (tmp > 9)
                        tmp = tmp % 10;
                    if (tmp >= 2 && tmp <= 4)
                        pdj = "минуты";
                    else if (tmp == 1)
                        pdj = "минуту";
                    else
                        pdj = "минут";
                }
                time += (" " + minutes + " " + pdj);
            }
            if (seconds > 0)
            {
                string pdj = string.Empty;
                int tmp = seconds;
                while (tmp >= 100)
                    tmp = tmp / 10 + tmp % 10;
                if (tmp >= 10 && tmp <= 19)
                {
                    pdj = "секунд";
                }
                else
                {
                    if (tmp > 9)
                        tmp = tmp % 10;
                    if (tmp >= 2 && tmp <= 4)
                        pdj = "секунды";
                    else if (tmp == 1)
                        pdj = "секунду";
                    else
                        pdj = "секунд";
                }
                time += (" " + seconds + " " + pdj);
            }

            


            //return "Я работаю уже " + ticks.ToString() + " секунд (✿◕⁀◕)";
            ms.Chat.SendMessage("Я работаю уже:" + time + " (✿◕⁀◕)");
            return true;
        }
        private bool cmd_BND(string argh, ChatMessage ms)
        {
            //SeijaHelper.loadTXT(nextdoor, @"Settings\bnd.txt");
            SeijaCommander.BND.Read();
            //Random rnd = new Random();
            ms.Chat.SendMessage(SeijaCommander.BND.GetRandomString());
            return true;
        }
        private bool cmd_Donger(string argh, ChatMessage ms)
        {
            //dongers.Clear();
            //using (System.IO.StreamReader file = File.OpenText(@"D:\SHARE\dongerlist.txt"))
            //     {
            //         string line="";
            //         while ((line = file.ReadLine()) != null)
            //             dongers.Add(line);
            //     }
            //loadTXT(dongers, @"D:\SHARE\dongerlist.txt");

            // Random rnd = new Random();
            ms.Chat.SendMessage(SeijaCommander.DongerList.GetRandomString());
            return true;
        }

        //public string donger() //заменить
        //{
        //    return dongers[SeijaHelper.FixedRandom(0, dongers.Count);
        //}
        private bool cmd_Pasta(string argh, ChatMessage ms)
        {
            //dongers.Clear();
            //using (System.IO.StreamReader file = File.OpenText(@"D:\SHARE\dongerlist.txt"))
            //     {
            //         string line="";
            //         while ((line = file.ReadLine()) != null)
            //             dongers.Add(line);
            //     }
            //loadTXT(pasta, @"D:\SHARE\pastalist.txt");
            //string res;

            ms.Chat.SendMessage(SeijaCommander.PastaList.GetRandomString());
            return true;
        }
        private bool cmd_PastaFind(string argh, ChatMessage ms)
        {
            if (argh == null || argh == string.Empty)
            {
                ms.Chat.SendMessage("argh=string.empty");
                return true;
            }
            string result = SeijaCommander.PastaList.Strings.Find(x => x.ToLower().Contains(argh));
            if (result == null || result == string.Empty)
            {
                ms.Chat.SendMessage("Ничего не нашла.");
                return true;
            }
            ms.Chat.SendMessage(result);
            return true;
        }
        private string generate_ah(int st, int end)
        {
            // Random rnd = new Random();
            int r = SeijaHelper.FixedRandom(st, end);
            string ah = "";
            for (int i = 0; i < r; i++)
                ah += "A";
            ah += "HHHHHHHHHHHHH";
            return ah;
        }
        private bool cmd_Goku(string argh, ChatMessage ms)
        {
            //Random rnd = new Random();
            SeijaHelper.isEn = false;
            ms.Chat.SendMessage("and this");
            System.Threading.Thread.Sleep(1000);
            ms.Chat.SendMessage("Whats he doing BabyRage");
            System.Threading.Thread.Sleep(2000);
            ms.Chat.SendMessage("IS TO GO EVEN FURTHER BEYOND");
            System.Threading.Thread.Sleep(3000);
            ms.Chat.SendMessage(generate_ah(15, 20));
            System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(500, 2500));
            ms.Chat.SendMessage(generate_ah(20, 25));
            System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(500, 2500));
            ms.Chat.SendMessage(generate_ah(25, 30));
            System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(500, 2500));
            ms.Chat.SendMessage(generate_ah(30, 35));
            System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(500, 2500));
            ms.Chat.SendMessage("no stop it goku BibleThump");
            System.Threading.Thread.Sleep(2000);
            ms.Chat.SendMessage(generate_ah(50, 100) + " SwiftRage");
            System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(500, 2500));
            ms.Chat.SendMessage(generate_ah(50, 100) + " SwiftRage");
            System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(500, 2500));
            ms.Chat.SendMessage(generate_ah(50, 100) + " SwiftRage");
            System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(500, 2500));
            ms.Chat.SendMessage(generate_ah(50, 100) + " SwiftRage");
            System.Threading.Thread.Sleep(SeijaHelper.FixedRandom(500, 2500));
            ms.Chat.SendMessage("its unreal Kreygasm");
            System.Threading.Thread.Sleep(1000);
            ms.Chat.SendMessage("how is he generates this much power PogChamp");
            System.Threading.Thread.Sleep(2500);
            ms.Chat.SendMessage("do it dad BabyRage");
            System.Threading.Thread.Sleep(1500);
            SeijaHelper.isEn = true;
            return true;
        }
    }
}
