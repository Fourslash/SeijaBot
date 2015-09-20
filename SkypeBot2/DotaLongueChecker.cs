using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeBot2
{
    class DotaLongueChecker
    {
        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
        public DotaLongueChecker()
        {
            if (GameChecker.ReReadTeams() != true)
                SeijaCommander.skype.SendMessage(SeijaCommander.Settings.Values.masterName, "Внимание! Список команд не был загружен!");
            gameTimer.Tick += new EventHandler(game_Tick);
            gameTimer.Interval = new TimeSpan(0, 3, 0);
            gameTimer.Start();
        }
        private void game_Tick(object sender, EventArgs e)
        {
            string chatname = "#splasher-_-/$e36c265204653a65";
            if (SeijaHelper.isEn == true)
            {
                try
                {
                    GameChecker.check();
                    foreach (string ms in GameChecker.results)
                    {
                        SeijaCommander.skype.get_Chat(chatname).SendMessage(ms);
                    }
                    //
                    GameChecker.results.Clear();
                }
                catch (Exception ex)
                {
                    SeijaCommander.skype.SendMessage(SeijaCommander.Settings.Values.masterName, chatname + "\n" + ex.Message);
                    SeijaHelper.write_log("\r\n" + DateTime.Now.ToString("u") + "\r\n" + chatname + "\r\n" + ex.Message + "\r\n");
                    SeijaCommander.skypeConnector.CheckConnection();
                    
                }
                finally
                {
                   
                }
            }
        }
    }
}
