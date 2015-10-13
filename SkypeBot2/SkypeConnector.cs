using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;
using System.Threading;

namespace SkypeBot2
{
    class SkypeConnector
    {
        delegate void del(ChatMessage msg);
        Skype skype;
        public SkypeConnector(Skype sk)
        {
            skype = sk;
        }
        public TAttachmentStatus AttachStatus
        {
            get {return ((SKYPE4COMLib.ISkype)skype).AttachmentStatus; }
        }
        public void Connect()
        {
            bool isAttached=false;
            do
                isAttached = Attach();
            while (!isAttached);

        }
        bool Attach()
        {
            try
            {
               // Skype skype = new Skype();
                if (!skype.Client.IsRunning)
                {
                    skype.Client.Start(true, true);
                }
                skype.Attach(7, true);
                skype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);
                if (((SKYPE4COMLib.ISkype)skype).AttachmentStatus !=TAttachmentStatus.apiAttachPendingAuthorization)
                {
                    while (((SKYPE4COMLib.ISkype)skype).AttachmentStatus ==TAttachmentStatus.apiAttachPendingAuthorization)
                    {
                        System.Threading.Thread.Sleep(5000);
                    }
                }
                if (((SKYPE4COMLib.ISkype)skype).AttachmentStatus != TAttachmentStatus.apiAttachSuccess)
                    return false;
                else
                {
                    if (((SKYPE4COMLib.ISkype)skype).ConnectionStatus != TConnectionStatus.conOnline)
                        SuspendInit();

                   // SeijaCommander.Init(skype, this);
                    return true;
                }
            }
            catch (Exception ex)
            {
                SeijaHelper.write_log(ex.Message);
                return false;
            }
        }
        void SuspendInit()
        {          
            do
            {
                try
                {
                    System.Threading.Thread.Sleep(5000);
                    TimeSpan ts = DateTime.Now - SeijaCommander.Settings.Values.lastRestart;
                    if (ts.TotalHours>=5)
                        RestartClient();
                }
                catch (Exception ex)
                {

                }
            }
            while (((SKYPE4COMLib.ISkype)skype).ConnectionStatus != TConnectionStatus.conOnline);
        }

        public void CheckConnection()
        {
            if (((SKYPE4COMLib.ISkype)skype).ConnectionStatus == TConnectionStatus.conOnline)
                return;
            skype.get_Chat(SeijaCommander.Settings.Values.homeconfa).SendMessage("Произошел разрыв соеденения. Произвожу перезагрузку сервера (✿◕⁀◕)");
            RestartClient();
        }
        public void RestartClient()
        {
            SeijaCommander.Settings.Values.lastRestart = DateTime.Now;
            SeijaCommander.Settings.Write();
            System.Diagnostics.Process.Start("shutdown.exe", "-r -f -t 0");
        }


        private void skype_MessageStatus(ChatMessage msg, TChatMessageStatus status)
        {
            try
            {
                if ((SeijaHelper.isEn == true || msg.Body.Contains("on"))/*&& msg.Sender.Handle=="fourslash"*/)
                {
                    //del dlg= SeijaCommander.Seija.ProcessNewMessage;
                    //dlg(msg);
                    //Thread T= new Thread(SeijaCommander.Seija.ProcessNewMessage);
                    //T.Start(msg);

                    SeijaCommander.Seija.ProcessMSG(new SkypeProvider(msg));
                   // SeijaHelper.SendToMaster("начало обработки, запуск процесса");
                      // SeijaCommander.Seija.ProcessNewMessage(msg);
                }
            }
            catch (Exception e)
            {
                SeijaHelper.write_log("\r\n" + DateTime.Now.ToString("u") + "\r\n" + msg.ChatName + "\r\n" + e.Message + "\r\n");
            }
        }
    }
}
